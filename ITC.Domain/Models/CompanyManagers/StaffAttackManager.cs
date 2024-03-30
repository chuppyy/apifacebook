using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.CompanyManagers;

/// <summary>
///     Người dùng - file đính kèm
/// </summary>
public class StaffAttackManager : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected StaffAttackManager()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id">Mã người dùng</param>
    /// <param name="staffAttackManagerId">Mã người dùng</param>
    /// <param name="fileId">Mã file</param>
    /// <param name="createBy">Người tạo</param>
    public StaffAttackManager(Guid id, Guid staffAttackManagerId, Guid fileId, string createBy = null) :
        base(id, createBy)
    {
        Update(staffAttackManagerId, fileId, createBy);
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id">Mã người dùng</param>
    /// <param name="fileId">Mã file</param>
    /// <param name="createBy">Người tạo</param>
    public StaffAttackManager(Guid id, Guid fileId, string createBy = null) : base(id, createBy)
    {
        FileId   = fileId;
        StatusId = ActionStatusEnum.Active.Id;
    }

    /// <summary>
    ///     Mã người dùng
    /// </summary>
    public Guid StaffManagerId { get; set; }

    /// <summary>
    ///     Mã file đính kèm
    /// </summary>
    public Guid FileId { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="staffAttackManagerId">Mã người dùng</param>
    /// <param name="fileId">Mã file</param>
    /// <param name="createBy">Người tạo</param>
    public void Update(Guid staffAttackManagerId, Guid fileId, string createBy = null)
    {
        StaffManagerId = staffAttackManagerId;
        FileId         = fileId;
        Update(createBy);
    }
}