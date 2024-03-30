using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.MenuManager;

/// <summary>
///     Phòng ban
/// </summary>
public class RoomManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public RoomManager(Guid id, string name, string description, Guid companyId, string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Update(name, description, companyId, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected RoomManager()
    {
    }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Ghi chú
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public Guid CompanyId { get; set; }

    public void Update(string name, string description, Guid company, string createdBy)
    {
        Name        = name;
        Description = description;
        CompanyId   = company;
        Update(createdBy);
    }
}