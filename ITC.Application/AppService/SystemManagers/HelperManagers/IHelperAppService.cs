#region

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ITC.Domain.Commands.CompanyManagers.StaffManager;
using ITC.Domain.Commands.GoogleAnalytics.Models;
using ITC.Domain.Core.ModelShare.SystemManagers.HelperManagers;
using ITC.Domain.ResponseDto;
using NCore.Modals;
using static ITC.Application.AppService.SystemManagers.HelperManagers.HelperAppService;

#endregion

namespace ITC.Application.AppService.SystemManagers.HelperManagers;

/// <summary>
///     Class interface service Helper
/// </summary>
public interface IHelperAppService
{
    /// <summary>
    ///     [Combobox] Trạng thái hiển thị danh sách file đính kèm
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ComboboxModalInt>> GetAttackViewCombobox();

    /// <summary>
    ///     Cập nhật nhóm tin
    /// </summary>
    /// <param name="model"></param>
    Task<bool> UpdateStatus(UpdateStatusHelperModal model);

    /// <summary>
    ///     Kiểm tra thời gian
    /// </summary>
    /// <param name="model"></param>
    Task<bool> CheckTime(CheckTimeModel model);

    Task<ReportSummary> GoogleAnalyticsReportAsync(GoogleAnalyticsReport query, CancellationToken cancellationToken);
    Task<List<ComboboxIdNameDto>> GetComboboxAsync();

    Task<List<ReportUserGroupResponseDto>> ReportUserGroupNewAsync(ReportUserPostQuery query);

    Task<IEnumerable<UserByOwnerDto>> GetListUserAsync(GetListUserQuery query);
    Task<bool> UpdateRatioUserAsync(UpdateRatioUserCommand command);
    Task<ResultXYDto> GetResultAsync(GetDataFromStringQuery query, CancellationToken cancellationToken);
    Task<bool> CreateMailTMAsync(CreateMailTMCommand command, CancellationToken cancellationToken);
    Task<string> GetTokenAsync(string userName, string password, CancellationToken cancellationToken);
    Task<string> GetCodeMailTMAsync(GetCodeMailTMQuery command, CancellationToken cancellationToken);
}