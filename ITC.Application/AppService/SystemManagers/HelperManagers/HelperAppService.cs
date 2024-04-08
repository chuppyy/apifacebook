#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Auth.OAuth2;
using ITC.Domain.Commands.GoogleAnalytics.Models;
using ITC.Domain.Commands.SystemManagers.HelperManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.SystemManagers.HelperManagers;
using ITC.Domain.Interfaces.CompanyManagers.StaffManagers;
using ITC.Domain.ResponseDto;
using NCore.Enums;
using NCore.Modals;
using Newtonsoft.Json;
using static System.Double;

#endregion

namespace ITC.Application.AppService.SystemManagers.HelperManagers;

/// <summary>
///     Class service helper
/// </summary>
public class HelperAppService : IHelperAppService
{
    #region Fields

    private readonly IMediatorHandler _bus;
    private readonly IMapper _mapper;
    private readonly IStaffManagerQueries _staffManagerQueries;

    #endregion

    #region Constructors

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    /// <param name="staffManagerQueries"></param>
    public HelperAppService(IMapper          mapper,
                            IMediatorHandler bus, IStaffManagerQueries staffManagerQueries)
    {
        _mapper = mapper;
        _bus    = bus;
        _staffManagerQueries = staffManagerQueries;
    }

#endregion

    /// <inheritdoc cref="GetAttackViewCombobox" />
    public async Task<IEnumerable<ComboboxModalInt>> GetAttackViewCombobox()
    {
        return await Task.Run(() =>
        {
            var lData = (List<NewsAttackEnumeration>)NewsAttackEnumeration.GetList();
            return lData.Select(items => new ComboboxModalInt { Id = items.Id, Name = items.Name }).ToList();
        });
    }

    /// <inheritdoc cref="UpdateStatus" />
    public async Task<bool> UpdateStatus(UpdateStatusHelperModal model)
    {
        var updateCommand = _mapper.Map<UpdateStatusHelperCommand>(model);
        await _bus.SendCommand(updateCommand);
        model.ResultCommand = updateCommand.ResultCommand;
        return model.ResultCommand;
    }

    public async Task<bool> CheckTime(CheckTimeModel model)
    {
        var updateCommand = new CheckTimeHelperCommand(model);
        await _bus.SendCommand(updateCommand);
        return updateCommand.ResultCommand;
    }
    
    public async Task<ReportGoogleAnalyticsDto> GoogleAnalyticsReportAsync(GoogleAnalyticsReport request, CancellationToken cancellationToken)
    {
        var config = await _staffManagerQueries.GetConfigAnalyticsAsync();

        if (config == null)
        {
            return null;
        }

        var serviceAccountEmail = config.Email;
        var privateKey = config.PrivateKey?.Replace("\\n", "\n");

        var credential = new ServiceAccountCredential(
            new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = new[] { "https://www.googleapis.com/auth/analytics.readonly" }
            }.FromPrivateKey(privateKey));

        var accessToken = await credential.GetAccessTokenForRequestAsync(cancellationToken: cancellationToken);

        if (string.IsNullOrEmpty(accessToken))
        {
            return null;
        }

        var domains = await _staffManagerQueries.GetListInfoWebAsync(request.DomainIds);

        if (domains == null || !domains.Any())
        {
            return null;
        }

        using (var client = new HttpClient())
        {
            var convertStartDate = request.StartDate != null ? request.StartDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
            var convertEndDate = request.EndDate != null ? request.EndDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
            var json = $@"{{
                    ""dateRanges"": [
                        {{
                            ""startDate"": ""{convertStartDate}"",
                            ""endDate"": ""{convertEndDate}""
                        }}
                    ],
                    ""dimensions"": [
                        {{
                            ""name"": ""unifiedPagePathScreen""
                        }}
                    ],
                    ""metrics"": [
                        {{
                            ""name"": ""screenPageViews""
                        }}
                    ],
                    ""metricAggregations"": [
                        ""TOTAL""
                    ]
                }}";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var linkViews = new List<UserViewDto>();
            foreach (var domain in domains)
            {
                var url = $"https://analyticsdata.googleapis.com/v1beta/properties/{domain.IdAnalytic}:runReport";
                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"), cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    var data = JsonConvert.DeserializeObject<RootObject>(responseContent);
                    if (data.rows != null && data.rows.Any())
                    {
                        foreach (var row in data.rows)
                        {
                            var link = row.dimensionValues?.FirstOrDefault();
                            var viewString = row.metricValues?.FirstOrDefault();

                            if (link != null && viewString != null)
                            {
                                TryParse(viewString.value, out var view);
                                linkViews.Add(new UserViewDto(link.value, view));
                            }
                        }
                    }
                }
            }

            var users = await _staffManagerQueries.GetUserCodeAsync();
            if (users != null && users.Any())
            {
                var results = new ReportGoogleAnalyticsDto
                {
                    Users = new List<UserReportDto>(),
                    TotalView = 0
                };
                foreach (var user in users)
                {
                    if (string.IsNullOrEmpty(user.UserCode))
                    {
                        results.Users.Add(new UserReportDto(user.Name, user.UserCode, 0));
                        continue;
                    }
                    var byUser = linkViews.Where(x => x.Link.Contains(user.UserCode));
                    if (byUser.Any())
                    {
                        var totalView = byUser.Sum(x => x.View);
                        results.Users.Add(new UserReportDto(user.Name, user.UserCode, totalView));
                    }
                    else
                    {
                        results.Users.Add(new UserReportDto(user.Name, user.UserCode, 0));
                    }
                }

                results.Users = results.Users.OrderByDescending(x => x.TotalView).ToList();
                results.TotalView = linkViews.Sum(x => x.View);
                return results;
            }

            return null;
        }
    }

    public async Task<List<ComboboxIdNameDto>> GetComboboxAsync()
    {
        var result = await _staffManagerQueries.GetComboboxWebAsync();
        return result.ToList();
    }

    public async Task<List<ReportUserGroupResponseDto>> ReportUserGroupNewAsync(ReportUserPostQuery query)
    {
        // Config Date
        var dateNow = DateTime.Now;
        query.StartDate = query.StartDate != null ? new DateTime(query.StartDate.Value.Year, query.StartDate.Value.Month, query.StartDate.Value.Day, 00, 00, 00, 000) : new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 00, 00, 00, 000);
        query.EndDate = query.EndDate != null ? new DateTime(query.EndDate.Value.Year, query.EndDate.Value.Month, query.EndDate.Value.Day, 23, 59, 59, 999) : new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 23, 59, 59, 999);
        
        // Thông tin người dùng và nhóm
        var userGroup = await _staffManagerQueries.ReportUserGroupNewAsync(query.EndDate);
        var listGroupId = userGroup.Select(x => x.GroupId).Distinct();

        var groupAmount = await _staffManagerQueries.ReportPostAsync(listGroupId, query.StartDate, query.EndDate);

        // Thông tin người dùng
        var users = userGroup.GroupBy(x => new { x.UserId, x.Name });
        var totalDaysDifference = Round((query.EndDate.Value - query.StartDate.Value).TotalDays);
       
        var results = new List<ReportUserGroupResponseDto>();
        foreach (var user in users)
        {
            var firstGroup = user.FirstOrDefault();
            if(firstGroup == null) continue;

            var userGroupResponse = new ReportUserGroupResponseDto
            {
                UserId = firstGroup.UserId,
                Name = firstGroup.Name,
                Groups = new List<GroupReportDto>()
            };

            // Lấy thông tin group của người dùng
            foreach (var group in user)
            {
                var kpi = totalDaysDifference * 4;
                if (group.Created > query.StartDate)
                {
                    var createdDate = new DateTime(group.Created.Year, group.Created.Month, group.Created.Day, 00, 00, 00, 000);
                    var difference = query.StartDate.Value - createdDate;
                    var totalDays = Round(difference.TotalDays);
                    if (totalDays <= 0)
                    {
                        kpi = 4;
                    }
                    else
                    {
                        kpi = totalDays * 4;
                    }
                }

                // Tạo đối tượng GroupReportDto và ánh xạ dữ liệu
                var groupReport = new GroupReportDto
                {
                    GroupId = group.GroupId,
                    GroupName = group.GroupName,
                    Created = group.Created,
                    TotalPost = group.TotalPost,
                    Kpi = kpi
                };

                // Tìm số lượng bài đăng cho group này
                var groupPostAmount = groupAmount.FirstOrDefault(x => x.GroupId == group.GroupId);
                if (groupPostAmount != null)
                {
                    // Ánh xạ số lượng bài đăng
                    groupReport.TotalPost = groupPostAmount.Amount;
                }

                // Thêm group vào danh sách của người dùng
                userGroupResponse.Groups.Add(groupReport);
            }

            // Thêm userGroupResponse vào kết quả
            results.Add(userGroupResponse);
        }

        return results.ToList();
    }

    public async Task<List<ReportData>> ApiGetWagesAsync(string tokenAK, int siteId, DateTime? startDate, DateTime? endDate)
    {
        var client = new HttpClient();

        var url = "https://api.adskeeper.co.uk/v1/publishers/712793/widget-custom-report";

        var request = new HttpRequestMessage(HttpMethod.Get, url);

        request.Headers.Add("Authorization", $"Bearer {tokenAK}");
        request.Headers.Add("Accept", "application/json, text/javascript, */*; q=0.01");

        var parameters = new System.Collections.Specialized.NameValueCollection {
            { "dateInterval", "interval" },
            { "startDate", $"{startDate}" },
            { "endDate", $"{endDate}" },
            { "siteId", "936535" },
            { "dimensions", "domain" },
            { "metrics", "wages" }
        };

        var uriBuilder = new UriBuilder(url)
        {
            Query = string.Join("&", Array.ConvertAll(parameters.AllKeys, key => $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(parameters[key])}"))
        };

        request.RequestUri = uriBuilder.Uri;

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            // Sử dụng thư viện Newtonsoft.Json để chuyển đổi từ JSON sang đối tượng
            var reportDataArray = JsonConvert.DeserializeObject<ReportData[]>(json);
            return reportDataArray.ToList();
        }

        return null;
    }
}