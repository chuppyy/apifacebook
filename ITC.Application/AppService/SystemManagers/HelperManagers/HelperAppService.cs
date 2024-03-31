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
        const string serviceAccountEmail = "ganews@news-417902.iam.gserviceaccount.com";
        const string privateKey = "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDVEg0BKkkdf8Tz\n/AQ7JM/C7t/rEuY1A6Ze73zvmmm9cR+Bgi6ifxLMxRcO5QOLSXNACUseNQ9kTn7D\nahfnEneWUcUZicur7+j3CDNMP765CbpuS1bqGAYOziV+4kH062nrCGv1Xr1clWq+\n4Syt3sLEooxdMZ6fh3gQLyanvEndt59otwSHZkvuVASvX2UviZULaHrVMDh8yFq5\nJ7CdJazSr1G4EdxfwJXkG9GPEua7qfLa7zdWCsOTuyedE++FolcC9BSaqokjYuPy\npMLQ+FaRjeKECcIdU4zETHwXJVSeRwobYHIs2s00kLdZDm8KbMNq0NhLr4FtA/QF\nqHJ2v3DrAgMBAAECggEAC05Bjw+2jwXRHzbiOwi4cz2WPoLVNj8kxukG5/Z5b9+Q\nbksdvM3MOJNTUR0ebHe6fCLQU3XkL0x32a2AsS9cOU51NwKw4kU51W1Y9wasNGUW\nFa4gmmMNDlkqkGNUYGON3G9ToWDdNdMz0gcLIqVX1zr7ApSw4pCRDx86QUzkQZlK\nrgn8/cKXcTGjCf6//GL9RRHKZ4hG8Qp3Mj0H/fI3fY82RXn6RPb/sk2OE+baQenY\nF5JcxgZN6LIM+dy4k16Hvzae73u6baoCQdfZXRWGwq0TiIMhgGuBkdtnzMZ2DtSB\nHU1K8y+QUw3dSyl3DM/zS6vYQzmg2lhmjLI3GiS56QKBgQDsdRMUDaZHsM/s0bZc\n2EAxzIAxq6Bqm4vohtRvEHe99TnoiojKbTlwhZm63YHUua05CmCG66+2cWqKujAU\nQResrK2XNTyu43uXh9gEW0ylth58SCiRdYfcJCC3JIsphpF/M3qbErfF/2aiYgfx\n4XhYXpuybtNbNqgg6WLnO8tmnQKBgQDmril0T1gxcNRgTLUAOY4nt73Y69peQKvg\ng29JivJCN2AbZUDpQfMLdU2FvYOgp4U8hSoT9VsQesPS0kqNmEkpd+hhL1hc4ANx\nRPoqv1ZB1ejWCi4UBMgqTf3KvfA/VRCHirnB9pdIBvWwmzWrjzFZVXJLx1bKN5ue\nH5fz651bJwKBgFtTXGsCWpaV07jDxotVenXBZkHI58xFB2RnruS2l4jmjdciqnKE\nfQrYjud9ZgejLyQ4vc9eeB8e7udlwewQt/QZPXKJPUbO1Y1RCj2khZX7IQsfU4va\nuP5tdbVGh/kh4FAgsdnzAMeYPSu6cRca3kBDSh9AbqFsGsObYHeuwICBAoGAJ6FG\nF+hFs4C5y62B7v70UBh98hVa15RussyBwvWu9vdCeJJlm20sDwzg+5f6VBBTkHkk\nKHefZG5i1AYyrq656vjhoEic+p+1l7EM9WkXrYnNTXBESEYmCTaK4ljNPGQlydNo\nZE0z4jjn3qZbixS3mqxWTXR6kZUKFBlDZrFUwEMCgYEAnaPXqpGjopNrLcqA/6od\nFCGXFlAcBdPIik0IysGrw5HQ1f1Nf+oXIPtQJ794xumQstLW4X3RGi2KzEMnxkGH\nUwdz5GpJaXKQCZm73pNB++y+DoGc8qlRzPrv/fFfE8nxLQ3h7fhTuWTZBbsJhZ5j\nKGlGrybSzi8s8K2ZIkwbfgM=\n-----END PRIVATE KEY-----\n";

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