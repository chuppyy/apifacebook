#region

using System;
using System.Linq;
using System.Threading.Tasks;
using ITC.Application.AppService.StudyManagers.MinusWord;
using ITC.Application.Helpers;
using ITC.Domain.Core.Bus;
using ITC.Domain.Core.ModelShare.StudyManagers.MinusWord;
using ITC.Domain.Core.Notifications;
using ITC.Infra.CrossCutting.Identity.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NCore.Modals;
using NCore.Responses;
using NCore.Systems;

#endregion

namespace ITC.Service.API.Controllers.StudyManagers;

/// <summary>
///     Từ loại trừ
/// </summary>
[Route("[controller]")]
[ApiController]
[Authorize]
public class MinusWordController : ApiController
{
#region Fields

    private readonly IMinusWordAppService _minusWordAppService;

#endregion

#region Constructors

    /// <summary>
    ///     Hàm dựng
    /// </summary>
    /// <param name="minusWordAppService"></param>
    /// <param name="notifications"></param>
    /// <param name="mediator"></param>
    public MinusWordController(IMinusWordAppService                     minusWordAppService,
                               INotificationHandler<DomainNotification> notifications,
                               IMediatorHandler                         mediator) :
        base(notifications, mediator)
    {
        _minusWordAppService = minusWordAppService;
    }

#endregion

#region Methods

    /// <summary>
    ///     Thêm mới từ loại trừ
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [CustomAuthorize(ModuleIdentity.MinusWord, TypeAudit.Add)]
    public async Task<IActionResult> Add([FromBody] MinusWordEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _minusWordAppService.Add(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);
    }

    /// <summary>
    ///     Sửa từ loại trừ
    /// </summary>
    /// <param name="model">Model menu</param>
    /// <returns></returns>
    [HttpPut("update")]
    [CustomAuthorize(ModuleIdentity.MinusWord, TypeAudit.Edit)]
    public async Task<IActionResult> Edit([FromBody] MinusWordEventModel model)
    {
        if (ModelState.IsValid) return NResponseCommand(await _minusWordAppService.Update(model), model);
        NotifyModelStateErrors();
        return NResponseCommand(false, model);
    }

    /// <summary>
    ///     Xóa từ loại trừ
    /// </summary>
    /// <param name="model">danh sách Id xóa</param>
    /// <returns></returns>
    [HttpPost("delete")]
    [CustomAuthorize(ModuleIdentity.MinusWord, TypeAudit.Delete)]
    public async Task<IActionResult> Delete([FromBody] DeleteModal model)
    {
        return NResponseCommand(await _minusWordAppService.Delete(model), model);
    }

    /// <summary>
    ///     Lấy từ loại trừ theo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-by-id/{id:guid}")]
    [CustomAuthorize(ModuleIdentity.MinusWord, TypeAudit.View)]
    public IActionResult GetById(Guid id)
    {
        return NResponseCommand(null, _minusWordAppService.GetById(id));
    }

    /// <summary>
    ///     [Phân trang] Danh sách từ loại trừ
    /// </summary>
    /// <returns></returns>
    [CustomAuthorize(ModuleIdentity.MinusWord, TypeAudit.View)]
    [HttpGet("get-paging")]
    public async Task<JsonResponse<Pagination<MinusWordPagingDto>>> GetPaging(
        [FromQuery] PagingModel model)
    {
        return await Task.Run(() =>
        {
            var lData = _minusWordAppService.GetPaging(new PagingModel
            {
                Search         = model.Search,
                PageNumber     = model.PageNumber,
                PageSize       = model.PageSize,
                StatusId       = model.StatusId,
                ModuleIdentity = ModuleIdentity.MinusWord
            }).Result.ToList();
            return new OkResponse<Pagination<MinusWordPagingDto>>(
                "",
                new Pagination<MinusWordPagingDto>
                {
                    PageLists   = lData,
                    TotalRecord = lData.Count > 0 ? lData[0].TotalRecord : 0
                });
        });
    }

    // /// <summary>
    // ///     Combobox
    // /// </summary>
    // /// <returns></returns>
    // [HttpGet("get-combobox")]
    // public async Task<JsonResponse<IEnumerable<ComboboxModal>>> GetCombobox(string search)
    // {
    //     return
    //         new OkResponse<IEnumerable<ComboboxModal>>(
    //             "", await _minusWordAppService.GetCombobox(search));
    // }

#endregion
}