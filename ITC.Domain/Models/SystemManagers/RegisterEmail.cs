using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.SystemManagers;

/// <summary>
///     Đăng ký email
/// </summary>
public class RegisterEmail : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected RegisterEmail()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id"></param>
    /// <param name="email"></param>
    /// <param name="projectId"></param>
    /// <param name="createBy"></param>
    public RegisterEmail(Guid id, string email, Guid projectId, string createBy = null) : base(id, createBy)
    {
        Email            = email;
        ProjectId        = projectId;
        RegisterDateTime = DateTime.Now;
    }

    /// <summary>
    ///     Địa chỉ email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Ngày đăng ký
    /// </summary>
    public DateTime RegisterDateTime { get; set; }
}