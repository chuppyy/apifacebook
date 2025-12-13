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
using ITC.Domain.Commands.CompanyManagers.StaffManager;
using ITC.Domain.Commands.GoogleAnalytics.Models;
using ITC.Domain.Commands.SystemManagers.HelperManagers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.SystemManagers.HelperManagers;
using ITC.Domain.Interfaces.CompanyManagers.StaffManagers;
using ITC.Domain.ResponseDto;
using NCore.Enums;
using NCore.Modals;
using static System.Double;
using System.Text.Json;
using NCore.Responses;
using Newtonsoft.Json.Linq;
using AutoMapper.Execution;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
    public HelperAppService(IMapper mapper,
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

    //public async Task<ReportGoogleAnalyticsDto> GoogleAnalyticsReportAsync(GoogleAnalyticsReport request, CancellationToken cancellationToken)
    //{


    //    var config = await _staffManagerQueries.GetConfigAnalyticsAsync();

    //    if (config == null)
    //    {
    //        return null;
    //    }

    //    var serviceAccountEmail = config.Email;
    //    var privateKey = config.PrivateKey?.Replace("\\n", "\n");

    //    var credential = new ServiceAccountCredential(
    //        new ServiceAccountCredential.Initializer(serviceAccountEmail)
    //        {
    //            Scopes = new[] { "https://www.googleapis.com/auth/analytics.readonly" }
    //        }.FromPrivateKey(privateKey));

    //    var accessToken = await credential.GetAccessTokenForRequestAsync(cancellationToken: cancellationToken);

    //    if (string.IsNullOrEmpty(accessToken))
    //    {
    //        return null;
    //    }

    //    var domains = await _staffManagerQueries.GetListInfoWebAsync(request.DomainIds);

    //    if (domains == null || !domains.Any())
    //    {
    //        return null;
    //    }

    //    using (var client = new HttpClient())
    //    {
    //        var convertStartDate = request.StartDate != null ? request.StartDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
    //        var convertEndDate = request.EndDate != null ? request.EndDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");
    //        var json = $@"{{
    //                ""dateRanges"": [
    //                    {{
    //                        ""startDate"": ""{convertStartDate}"",
    //                        ""endDate"": ""{convertEndDate}""
    //                    }}
    //                ],
    //                ""dimensions"": [
    //                    {{
    //                        ""name"": ""unifiedPagePathScreen""
    //                    }}
    //                ],
    //                ""metrics"": [
    //                    {{
    //                        ""name"": ""screenPageViews""
    //                    }}
    //                ],
    //                ""metricAggregations"": [
    //                    ""TOTAL""
    //                ]
    //            }}";
    //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

    //        // Dữ liệu báo cáo tổng tiền cho domain
    //        var allReports = new List<ReportData>();

    //        foreach (var domain in domains)
    //        {
    //            var reportData = new ReportData();
    //            #region View
    //            // Dữ liệu báo cáo lượt view
    //            var linkViews = new List<UserViewDto>();
    //            var url = $"https://analyticsdata.googleapis.com/v1beta/properties/{domain.IdAnalytic}:runReport";
    //            var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"), cancellationToken);
    //            if (response.IsSuccessStatusCode)
    //            {
    //                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
    //                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(responseContent);
    //                if (data.rows != null && data.rows.Any())
    //                {
    //                    foreach (var row in data.rows)
    //                    {
    //                        var link = row.dimensionValues?.FirstOrDefault();
    //                        var viewString = row.metricValues?.FirstOrDefault();

    //                        if (link != null && viewString != null)
    //                        {
    //                            TryParse(viewString.value, out var view);
    //                            linkViews.Add(new UserViewDto(link.value, view));
    //                        }
    //                    }
    //                }
    //            }
    //            reportData.UserViews = linkViews;
    //            #endregion

    //            #region Wages

    //            if (!string.IsNullOrEmpty(config.TokenAK))
    //            {
    //                var wages = await ApiGetWagesAsync(config.TokenAK, domain.TokenAK, convertStartDate, convertEndDate);
    //                if (wages != null && wages.Any())
    //                {
    //                    reportData.Wages=wages.FirstOrDefault()?.Wages;
    //                }
    //            }

    //            #endregion
    //            reportData.IdDomain = domain.Id;
    //            allReports.Add(reportData);
    //        }

    //        var users = await _staffManagerQueries.GetUserCodeAsync();
    //        if (users != null && users.Any())
    //        {
    //            var results = new ReportGoogleAnalyticsDto
    //            {
    //                Users = new List<UserReportDto>(),
    //                TotalView = 0
    //            };

    //            foreach (var domainData in allReports)
    //            {
    //                foreach (var user in users)
    //                {
    //                    if (string.IsNullOrEmpty(user.UserCode))
    //                    {
    //                        results.Users.Add(new UserReportDto(user.Name, user.UserCode, 0));
    //                        continue;
    //                    }
    //                    var byUser = domainData.UserViews.Where(x => x.Link.Contains(user.UserCode));
    //                    if (byUser.Any())
    //                    {
    //                        var totalView = byUser.Sum(x => x.View);
    //                        results.Users.Add(new UserReportDto(user.Name, user.UserCode, totalView));
    //                    }
    //                    else
    //                    {
    //                        results.Users.Add(new UserReportDto(user.Name, user.UserCode, 0));
    //                    }
    //                }

    //                results.Users = results.Users.OrderByDescending(x => x.TotalView).ToList();
    //                results.TotalView = domainData.UserViews.Sum(x => x.View);
    //            }

    //            return results;
    //        }

    //        return null;
    //    }
    //}

    public async Task<ReportSummary> GoogleAnalyticsReportAsync(
GoogleAnalyticsReport request,
CancellationToken cancellationToken = default)
    {
       
        // === Lấy config (email + private key) ===
        var config =  await _staffManagerQueries.GetConfigAnalyticsAsync();
        if (config.Email == null || config.PrivateKey == null)
        {
            Console.WriteLine("Config rỗng.");
            return null;
        }

        var serviceAccountEmail = config.Email;
        var privateKey = config.PrivateKey.Replace("\\n", "\n");

        // === Tạo credential & lấy access token ===
        var credential = new ServiceAccountCredential(
            new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = new[] { "https://www.googleapis.com/auth/analytics.readonly" }
            }.FromPrivateKey(privateKey));

        var accessToken = await credential.GetAccessTokenForRequestAsync(cancellationToken: cancellationToken);

        if (string.IsNullOrEmpty(accessToken))
        {
            Console.WriteLine("Access token rỗng.");
            return null;
        }

        // === Lấy danh sách domain ===
        var domains = await _staffManagerQueries.GetListInfoWebAsync(request.DomainIds);
        if (domains == null || !domains.Any())
        {
            Console.WriteLine("Không có domain nào.");
            return null;
        }

        // === Lấy danh sách nhân viên (Name + UserCode) ===
        var users = await _staffManagerQueries.GetUserCodeAsync();
        if (users == null || !users.Any())
        {
            Console.WriteLine("Không có nhân viên.");
            return null;
        }

        var allDomainReports = new List<(int DomainId, string DomainName, List<UserViewDto> Views)>();
        var vnTimeZone = TimeSpan.FromHours(7);
        using (var client = new HttpClient())
        {           

            // StartDate
            var convertStartDate = request.StartDate != null
                ? request.StartDate.Value.Add(vnTimeZone).ToString("yyyy-MM-dd")
                : DateTime.UtcNow.Add(vnTimeZone).ToString("yyyy-MM-dd");

            // EndDate
            var convertEndDate = request.EndDate != null
                ? request.EndDate.Value.Add(vnTimeZone).ToString("yyyy-MM-dd")
                : DateTime.UtcNow.Add(vnTimeZone).ToString("yyyy-MM-dd");

            var json = $@"{{
  ""dateRanges"": [
    {{
      ""startDate"": ""{convertStartDate}"",
      ""endDate"": ""{convertEndDate}""
    }}
  ],
  ""dimensions"": [
    {{
      ""name"": ""landingPage""
    }}
  ],
  ""metrics"": [
    {{
      ""name"": ""sessions""
    }}
  ],
  ""metricAggregations"": [
    ""TOTAL""
  ]
}}";
//            var json = $@"{{
//  ""dateRanges"": [
//    {{
//      ""startDate"": ""{convertStartDate}"",
//      ""endDate"": ""{convertEndDate}""
//    }}
//  ],
//  ""dimensions"": [
//    {{
//      ""name"": ""unifiedPagePathScreen""
//    }}
//  ],
//  ""metrics"": [
//    {{
//      ""name"": ""screenPageViews""
//    }}
//  ],
//  ""metricAggregations"": [
//    ""TOTAL""
//  ]
//}}";

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            // Gọi GA4 cho từng domain (property)
            foreach (var domain in domains)
            {
                var linkViews = new List<UserViewDto>();

                var url = $"https://analyticsdata.googleapis.com/v1beta/properties/{domain.IdAnalytic}:runReport";

                var response = await client.PostAsync(
                    url,
                    new StringContent(json, Encoding.UTF8, "application/json"),
                    cancellationToken);

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                //Console.WriteLine("GA4 response:");
                //Console.WriteLine(responseContent);

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<RootObject>(responseContent);
                    if (data?.rows != null && data.rows.Any())
                    {
                        foreach (var row in data.rows)
                        {
                            var link = row.dimensionValues?.FirstOrDefault();
                            var viewString = row.metricValues?.FirstOrDefault();

                            if (link != null && viewString != null &&
                                int.TryParse(viewString.value, out var view))
                            {
                                linkViews.Add(new UserViewDto(link.value, view));
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"GA4 lỗi: {(int)response.StatusCode} - {response.ReasonPhrase}");
                }

                // GIẢ ĐỊNH: domain.DomainName tồn tại – nếu tên property khác thì sửa lại
                allDomainReports.Add((domain.Id, domain.Name, linkViews));
            }
        }

        // ==============================
        // 1. Tính thống kê theo domain
        // ==============================

        var domainStats = new List<DomainAll>();
        var totalViewAllDomains = 0;

        foreach (var dr in allDomainReports)
        {
            var traffic = (int)dr.Views.Sum(v => v.View);
            totalViewAllDomains += traffic;

            domainStats.Add(new DomainAll
            {
                Id=dr.DomainId,
                DomainName = dr.DomainName,
                Traffic = traffic
                // Ratio sẽ tính sau khi biết tổng view
            });
        }

        // ==============================
        // 2. Tính thống kê theo user
        // ==============================

        var userViewList = new List<UserView>();

        foreach (var user in users)
        {
            if (user.Name == "Sa")
            {
                var x = 1;
            }
            var userCode = user.UserCode;
            var userName = user.Name;

            var domainViews = new List<DomainView>();
            var totalViewUser = 0;

            foreach (var dr in allDomainReports)
            {
                var viewOnDomain = 0;

                if (!string.IsNullOrEmpty(userCode))
                {
                    var userCodeFull = "-" + userCode + "-";
                    viewOnDomain =(int) dr.Views
                        .Where(x => !string.IsNullOrEmpty(x.Link) &&
                                    x.Link.Contains(userCodeFull, StringComparison.OrdinalIgnoreCase))
                        .Sum(x => x.View);
                }

                totalViewUser += viewOnDomain;

                domainViews.Add(new DomainView
                {
                    Id = dr.DomainId.ToString(),
                    Domain = dr.DomainName,
                    View = viewOnDomain
                });
            }

            userViewList.Add(new UserView
            {
                User = userName,
                TotalView = totalViewUser,
                DomainViews = domainViews
                // Rank sẽ gán sau
            });
        }
        userViewList= userViewList.Where(x => x.TotalView > 0).ToList();
        // Gán Rank cho user theo TotalView desc
        var orderedUsers = userViewList
            .OrderByDescending(u => u.TotalView)
            .ToList();

        for (int i = 0; i < orderedUsers.Count; i++)
        {
            orderedUsers[i].Rank = i + 1;
        }

        // ==============================
        // 3. Tính Ratio & TopDomain/TopUser
        // ==============================

        if (totalViewAllDomains > 0)
        {
            foreach (var d in domainStats)
            {
                // Ratio dạng 0–1; muốn % thì nhân 100
                d.Ratio = (double)d.Traffic / totalViewAllDomains;
            }
        }
        else
        {
            foreach (var d in domainStats)
            {
                d.Ratio = 0;
            }
        }

        var topDomain = domainStats
            .OrderByDescending(d => d.Traffic)
            .FirstOrDefault();

        var topUser = orderedUsers
            .OrderByDescending(u => u.TotalView)
            .FirstOrDefault();

        var summary = new ReportSummary
        {
            TotalView = totalViewAllDomains,
            // Nếu muốn tính tất cả user thì dùng orderedUsers.Count
            // nếu chỉ tính user có view > 0 thì:
            TotalUser = orderedUsers.Count(u => u.TotalView > 0),
            TopDomain = topDomain?.DomainName,
            TopUser = topUser?.User,
            Domains = domainStats
                .OrderByDescending(d => d.Traffic)
                .ToList(),
            Users = orderedUsers
        };

        return summary;
    }



    public async Task<List<ComboboxIdNameDto>> GetComboboxAsync()
    {
        var result = await _staffManagerQueries.GetComboboxWebAsync();
        return result.ToList();
    }

    public async Task<List<ReportUserGroupResponseDto>> ReportUserGroupNewAsync(ReportUserPostQuery request)
    {
        // Config Date
        var dateNow = DateTime.Now;
        request.StartDate = request.StartDate != null ? new DateTime(request.StartDate.Value.Year, request.StartDate.Value.Month, request.StartDate.Value.Day, 00, 00, 00, 000) : new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 00, 00, 00, 000);
        request.EndDate = request.EndDate != null ? new DateTime(request.EndDate.Value.Year, request.EndDate.Value.Month, request.EndDate.Value.Day, 23, 59, 59, 999) : new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 23, 59, 59, 999);

        // Thông tin người dùng và nhóm
        var userGroup = await _staffManagerQueries.ReportUserGroupNewAsync(request.EndDate);
        var listGroupId = userGroup.Select(x => x.GroupId).Distinct();

        var groupAmount = await _staffManagerQueries.ReportPostAsync(listGroupId, request.StartDate, request.EndDate);

        // Thông tin người dùng
        var users = userGroup.GroupBy(x => new { x.UserId, x.Name });
        var totalDaysDifference = Round((request.EndDate.Value - request.StartDate.Value).TotalDays);

        var results = new List<ReportUserGroupResponseDto>();
        foreach (var user in users)
        {
            var firstGroup = user.FirstOrDefault();
            if (firstGroup == null) continue;

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
                if (group.Created > request.StartDate)
                {
                    var createdDate = new DateTime(group.Created.Year, group.Created.Month, group.Created.Day, 00, 00, 00, 000);
                    var difference = request.StartDate.Value - createdDate;
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

    public async Task<List<ReportData>> ApiGetWagesAsync(string tokenAK, string siteId, string startDate, string endDate)
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
            { "siteId", $"{siteId}" },
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
            var reportDataArray = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportData[]>(json);
            return reportDataArray.ToList();
        }

        return null;
    }

    public async Task<IEnumerable<UserByOwnerDto>> GetListUserAsync(GetListUserQuery query)
    {
        var listUser = await _staffManagerQueries.GetListUserByOwnerAsync(query.UserId);
        return listUser;
    }

    public async Task<bool> UpdateRatioUserAsync(UpdateRatioUserCommand command)
    {
        var listUser = await _staffManagerQueries.UpdateRatioAsync(command.UserIds, command.Ratio);
        throw new NotImplementedException();
    }

    public async Task<ResultXYDto> GetResultAsync(GetDataFromStringQuery query, CancellationToken cancellationToken)
    {

        string input = ";;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2;-2";  // Chuỗi của bạn với 54 số, mỗi số cách nhau bằng dấu chấm phẩy
        input = query.Key.Trim(';');
        // Tách chuỗi thành mảng các giá trị
        string[] values = input.Split(';');

        // Kiểm tra độ dài của mảng (phải là 54)
        //if (values.Length != 54)
        //{
        //    Console.WriteLine("Chuỗi không hợp lệ. Cần 54 số.");
        //    return;
        //}

        // Tạo mảng 2D 9 hàng và 6 cột
        int rowAll = 9;
        int colAll = 6;
        int[,] board = new int[rowAll, colAll];

        // Chuyển đổi giá trị chuỗi thành số nguyên và đưa vào mảng 2D
        int index = 0;
        for (int row = 0; row < rowAll; row++)
        {
            for (int col = 0; col < colAll; col++)
            {
                board[row, col] = int.Parse(values[index]);
                index++;
            }
        }

        // Mảng board (giá trị của ô, -1 là ô đã gắn cờ mìn, -2 là ô chưa mở, 0,1,2,... là ô đã mở)
        //int[,] board = {
        //    {  1, -1,  2,  1, -2, -2 },
        //    {  2,  3, -1,  2,  1, -2 },
        //    { -1, -1,  3, -1,  2, -2 },
        //    {  2, -1,  4,  2,  2,  1 },
        //    {  1,  2, -1,  1, -1,  1 },
        //    { -2,  1,  1,  2,  2,  1 }
        //};
        // Mảng boardOpen (các ô đã mở, true = ô đã mở, false = ô chưa mở)
        bool[,] boardOpen = new bool[rowAll, colAll];
        // Tìm các ô có thể mở hoặc gắn cờ mìn

        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                // Nếu ô đã mở (0, 1, 2, 3...), đánh dấu là true
                if (board[row, col] !=-2)
                {
                    boardOpen[row, col] = true;
                }
            }
        }


        var listX = new List<int>();
        var listY = new List<int>();
        // Gọi hàm quét bảng
        while (true)
        {
            var result = FindNextOpenableAndFlaggableCells(board, boardOpen);

            // Nếu có ô có thể mở hoặc gắn cờ, in ra và cập nhật
            if (result.openableCells.Count > 0 || result.flaggableCells.Count > 0)
            {
                foreach (var cell in result.openableCells)
                {
                    boardOpen[cell.x, cell.y] = true; // Đánh dấu ô là đã mở
                }

                listX.AddRange(result.openableCells.Select(result => result.x * colAll + 1 + result.y));
                listY.AddRange(result.flaggableCells.Select(result => result.x * colAll + 1 + result.y));
                //// In ra ô mở và gắn cờ
                //Console.WriteLine("Ô có thể mở thêm:");
                //foreach (var cell in result.openableCells)
                //{
                //    Console.WriteLine($"({cell.x}, {cell.y})");
                //}

                //Console.WriteLine("Ô có thể gắn cờ thêm:");
                //foreach (var cell in result.flaggableCells)
                //{
                //    Console.WriteLine($"({cell.x}, {cell.y})");
                //}
            }
            else
            {

                break; // Thoát khỏi vòng lặp
            }
        }
        if (listX.Count > 0 || listY.Count > 0)
        {
            return new ResultXYDto(listX, listY, 0, listX.Count, listY.Count);
        }
        else
        {
            // Nếu không tìm thấy ô nào hợp lệ, mở ô ngẫu nhiên chưa mở
            var randomCell = GetRandomUnopenedCell(boardOpen);
            if (randomCell.x != -1 && randomCell.y != -1) // Kiểm tra ô hợp lệ
            {
                Console.WriteLine($"Không tìm thấy ô hợp lệ, mở ô ngẫu nhiên tại ({randomCell.x}, {randomCell.y})");
                boardOpen[randomCell.x, randomCell.y] = true; // Đánh dấu ô đã mở
                return new ResultXYDto(listX, listY, randomCell.x * colAll + 1 + randomCell.y, listX.Count, listY.Count);
            }
            else
            {
                return new ResultXYDto(listX, listY, -1, 0, 0);
            }

            //return new ResultXYDto(listX, listY, , listX.Count, listY.Count);
        }




        // Xử lý ở đây nha anh
        //return new ResultXYDto((result.x* colAll +1+ result.y), flag);
    }

    #region Coin
    Random rand = new Random();

    // Hàm tìm ô có thể mở hoặc gắn cờ
    public (List<(int x, int y)> openableCells, List<(int x, int y)> flaggableCells) FindNextOpenableAndFlaggableCells(int[,] board, bool[,] boardOpen)
    {
        List<(int x, int y)> openableCells = new List<(int x, int y)>();
        List<(int x, int y)> flaggableCells = new List<(int x, int y)>();
        int rows = board.GetLength(0);
        int cols = board.GetLength(1);

        // Quét toàn bộ bảng
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Nếu ô đã mở
                if (boardOpen[row, col] || board[row, col] == -1)
                {
                    // Lấy các ô lân cận
                    var neighbors = GetNeighbors(row, col, rows, cols);
                    int flaggedMines = 0;
                    int unopenedCells = 0;

                    // Đếm số ô chưa mở và mìn đã gắn cờ
                    foreach (var (ni, nj) in neighbors)
                    {
                        if (boardOpen[ni, nj] && board[ni, nj] == -1)
                            flaggedMines++;
                        if (!boardOpen[ni, nj])
                            unopenedCells++;
                    }

                    // Nếu số mìn lân cận khớp với số trên ô, mở tất cả các ô chưa mở
                    if (flaggedMines == board[row, col] && unopenedCells > 0)
                    {
                        foreach (var (ni, nj) in neighbors)
                        {
                            if (!boardOpen[ni, nj] && board[ni, nj] != -1)
                            {
                                openableCells.Add((ni, nj));
                                boardOpen[ni, nj] = true;
                            }
                        }
                    }

                    // Nếu số ô chưa mở bằng đúng số mìn còn lại, gắn cờ mìn
                    if (unopenedCells == board[row, col] - flaggedMines)
                    {
                        foreach (var (ni, nj) in neighbors)
                        {
                            if (!boardOpen[ni, nj] && board[ni, nj] == -2)
                            {
                                flaggableCells.Add((ni, nj));
                                boardOpen[ni, nj] = true;
                                board[ni, nj] = -1;
                            }
                        }
                    }
                }
            }
        }

        return (openableCells, flaggableCells);
    }

    // Hàm lấy danh sách các ô lân cận
    public List<(int, int)> GetNeighbors(int row, int col, int maxRows, int maxCols)
    {
        List<(int, int)> neighbors = new List<(int, int)>();

        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = col - 1; j <= col + 1; j++)
            {
                if (i >= 0 && i < maxRows && j >= 0 && j < maxCols && !(i == row && j == col))
                {
                    neighbors.Add((i, j));
                }
            }
        }

        return neighbors;
    }

    // Hàm lấy ô ngẫu nhiên chưa mở
    public (int x, int y) GetRandomUnopenedCell(bool[,] boardOpen)
    {
        List<(int x, int y)> unopenedCells = new List<(int x, int y)>();
        int rows = boardOpen.GetLength(0);
        int cols = boardOpen.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (!boardOpen[row, col])
                {
                    unopenedCells.Add((row, col));
                }
            }
        }

        // Chọn ngẫu nhiên một ô chưa mở
        if (unopenedCells.Count > 0)
        {
            return unopenedCells[rand.Next(unopenedCells.Count)];
        }

        return (-1, -1); // Trả về (-1, -1) nếu không còn ô nào chưa mở
    }

    #endregion

    public async Task<bool> CreateMailTMAsync(CreateMailTMCommand command, CancellationToken cancellationToken)
    {
        using HttpClient client = new HttpClient();
        var url = "https://api.mail.tm/accounts";

        var requestBody = new
        {
            address = command.Address,
            password = command.Password
        };

        var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, content, cancellationToken);
        var responseString = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == System.Net.HttpStatusCode.OK ||
            response.StatusCode == System.Net.HttpStatusCode.Created)
        {
            return true;
        }
        return false;
    }

    public async Task<string> GetTokenAsync(string userName, string password, CancellationToken cancellationToken)
    {
        using var client = new HttpClient();

        var url = "https://api.mail.tm/token";

        var requestBody = new
        {
            address = userName,
            password = password
        };

        var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, content, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<MailTMTokenResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return tokenResponse.Token;
        }

        // Có thể log lỗi tại đây nếu cần
        return null;
    }

    public async Task<string> GetCodeMailTMAsync(GetCodeMailTMQuery command, CancellationToken cancellationToken)
    {
        var tokenInfo = await GetTokenAsync(command.Address, command.Password, cancellationToken);

        var result = "";
        if (string.IsNullOrEmpty(tokenInfo))
        {
            return result;
        }

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenInfo);

        var response = await client.GetAsync("https://api.mail.tm/messages", cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var responseDto = JsonConvert.DeserializeObject<MessageCollection>(responseContent);
            var intro = "";
            if (responseDto?.Members != null)
            {
                responseDto.Members = responseDto.Members.OrderByDescending(x => x.CreatedAt).ToList();

                if (!string.IsNullOrEmpty(command.Email))
                {
                    var members = responseDto.Members.Where(x => x.From.Address == command.Email).ToList();

                    if (members != null && members.Any())
                    {
                        if (!string.IsNullOrEmpty(command.Title))
                        {
                            var message = members.FirstOrDefault(x => x.Subject?.IndexOf(command.Title, StringComparison.OrdinalIgnoreCase) >= 0);
                            if (message != null)
                            {
                                intro = message.Intro;
                            }
                        }
                        else
                        {
                            intro = members.FirstOrDefault()?.Intro;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(command.Title))
                    {
                        var message = responseDto.Members.FirstOrDefault(x => x.Subject?.IndexOf(command.Title, StringComparison.OrdinalIgnoreCase) >= 0);
                        if (message != null)
                        {
                            intro = message.Intro;
                        }
                    }
                    else
                    {
                        intro = responseDto.Members.FirstOrDefault()?.Intro;
                    }
                }
            }

            if (!string.IsNullOrEmpty(intro))
            {
                if (!string.IsNullOrEmpty(intro) && intro.Length >= 6)
                {
                    result = intro[^6..];
                }
            }
            return result;
        }
        return result;
    }
}