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
}