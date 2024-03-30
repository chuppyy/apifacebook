#region

using System;

#endregion

namespace ITC.Domain.Core.Models;

/// <summary>
///     Lớp cơ sở của các lớp dữ liệu
/// </summary>
public class RootModel : Entity
{
#region Constructors

    protected RootModel()
    {
        Created  = DateTime.Now;
        Modified = DateTime.Now;
    }

    protected RootModel(Guid id) : base(id)
    {
        Created  = DateTime.Now;
        Modified = DateTime.Now;
    }

    protected RootModel(Guid id, string createdBy) : this(id)
    {
        CreatedBy  = createdBy;
        ModifiedBy = createdBy;
    }

    // protected RootModel(Guid id, int portal, string createdBy) : this(id)
    // {
    //     PortalId   = portal;
    //     CreatedBy  = createdBy;
    //     ModifiedBy = createdBy;
    // }

#endregion

#region Properties

    /// <summary>
    ///     Ngày tạo
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    ///     Người tạo
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    ///     Có xóa hay không
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    ///     Ngày chỉnh sửa gần nhất
    /// </summary>
    public DateTime Modified { get; set; }

    /// <summary>
    ///     Người chỉnh sửa
    /// </summary>
    public string ModifiedBy { get; set; }

    // public int PortalId { get; set; }
    /// <summary>
    ///     Trạng thái
    /// </summary>
    public int StatusId { get; set; }

#endregion

#region Methods

    /// <summary>
    ///     Xóa dữ liệu
    /// </summary>
    public void Delete(string updatedBy = null)
    {
        IsDeleted = true;
        if (!string.IsNullOrEmpty(updatedBy)) ModifiedBy = updatedBy;
        Modified = DateTime.Now;
    }

    /// <summary>
    ///     Khôi phục dữ liệu
    /// </summary>
    public void Recover(string updatedBy = null)
    {
        IsDeleted = false;
        if (!string.IsNullOrEmpty(updatedBy)) ModifiedBy = updatedBy;
        Modified = DateTime.Now;
    }


    /// <summary>
    ///     Khởi tạo
    /// </summary>
    public void Intialize()
    {
        Created  = DateTime.Now;
        Modified = DateTime.Now;
    }

    /// <summary>
    ///     Cập nhật dữ liệu
    /// </summary>
    /// <param name="modifiedBy"></param>
    public void Update(string modifiedBy = null, string description = null)
    {
        ModifiedBy = modifiedBy;
        Modified   = DateTime.Now;
    }

#endregion
}