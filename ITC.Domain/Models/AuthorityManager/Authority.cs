using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;
using ITC.Domain.Core.ModelShare.AuthorityManager;
using NCore.Actions;

namespace ITC.Domain.Models.AuthorityManager;

/// <summary>
///     Phân quyền sử dụng
/// </summary>
public class Authority : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected Authority()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public Authority(Guid   id,
                     Guid   companyId,
                     Guid   projectId,
                     string name,
                     string description,
                     string createBy = null) : base(id, createBy)
    {
        StatusId  = ActionStatusEnum.Active.Id;
        ProjectId = projectId;
        Update(companyId, name, description, createBy);
        AuthorityDetails = new List<AuthorityDetail>();
    }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    ///     Tên quyền
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Đường dẫn hiển thị trang chủ
    /// </summary>
    public string UrlHomePage { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<AuthorityDetail> AuthorityDetails { get; set; }

    public void Update(Guid companyId, string name, string description, string modifieldBy = null)
    {
        CompanyId   = companyId;
        Name        = name;
        Description = description;
        Update(modifieldBy);
    }

    /// <summary>
    ///     Thêm mới quyền sử dụng
    /// </summary>
    /// <param name="models">Danh sách menu</param>
    /// <param name="historyPosition">Lịch sử vị trí</param>
    /// <param name="createdBy">Người tạo</param>
    public void AddDetail(List<MenuByAuthoritiesSaveModel> models, int historyPosition, string createdBy)
    {
        foreach (var zModel in models)
            AuthorityDetails.Add(new AuthorityDetail(Guid.NewGuid(),
                                                     zModel.Id,
                                                     zModel.Value,
                                                     historyPosition,
                                                     zModel.Name,
                                                     createdBy));
    }

    /// <summary>
    ///     Cập nhật quyền sử dụng
    /// </summary>
    /// <param name="models"></param>
    /// <param name="authoritiesId"></param>
    /// <param name="historyPosition"></param>
    /// <param name="createdBy"></param>
    public void UpdateDetail(List<MenuByAuthoritiesSaveModel> models, Guid authoritiesId, int historyPosition,
                             string                           createdBy)
    {
        foreach (var zModel in models)
            AuthorityDetails.Add(new AuthorityDetail(Guid.NewGuid(),
                                                     authoritiesId,
                                                     zModel.Id,
                                                     zModel.Value,
                                                     historyPosition,
                                                     createdBy));
    }
}