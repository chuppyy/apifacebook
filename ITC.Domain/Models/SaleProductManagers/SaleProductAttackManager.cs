using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     Quản lý giới thiệu - file đính kèm
/// </summary>
public class SaleProductAttackManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public SaleProductAttackManager(Guid id, string name, string content, Guid avatarId, Guid saleProductManagerId,
                                    int  position, string createdBy = null) :
        base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Update(name, content, avatarId, saleProductManagerId, position, createdBy);
    }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Nội dung
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Mã nội dung giới thiệu
    /// </summary>
    public Guid SaleProductManagerId { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên</param>
    /// <param name="content">Nội dung</param>
    /// <param name="avatarId">Ảnh đại diện</param>
    /// <param name="saleProductManagerId">Mã giới thiệu</param>
    /// <param name="position">Vị trí</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name, string content, Guid avatarId, Guid saleProductManagerId, int position,
                       string createdBy = null)
    {
        Name                 = name;
        Content              = content;
        SaleProductManagerId = saleProductManagerId;
        AvatarId             = avatarId;
        Position             = position;
        Update(createdBy);
    }
}