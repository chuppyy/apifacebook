using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.AuthorityManager;

/// <summary>
///     Phân quyền sử dụng cho người dùng
/// </summary>
public class AuthorityUser : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected AuthorityUser()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public AuthorityUser(Guid id, Guid companyId, Guid userId, Guid authorityId, string createBy = null) :
        base(id, createBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Update(companyId, userId, authorityId, createBy);
    }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    ///     Mã người dùng
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Mã quyền sử dụng
    /// </summary>
    public Guid AuthorityId { get; set; }

    public void Update(Guid companyId, Guid userId, Guid authorityId, string createBy = null)
    {
        CompanyId   = companyId;
        UserId      = userId;
        AuthorityId = authorityId;
        Update(createBy);
    }
}