using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     Quản lý comment
/// </summary>
public class CommentManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public CommentManager(Guid id, string name, string email, string content, DateTime startDate, int position,
                          int groupId, Guid projectId, string parentId, Guid productId, string secrectKey, string phone,
                          string createdBy = null) :
        base(id, createdBy)
    {
        StatusId   = ActionStatusEnum.Pending.Id;
        ProjectId  = projectId;
        Position   = position;
        PLeft      = 0;
        PRight     = 0;
        UserAgree  = "";
        StartAgree = DateTime.Now;
        SecrectKey = secrectKey;
        Update(name, email, content, startDate, groupId, parentId, productId, phone, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected CommentManager()
    {
    }

    /// <summary>
    ///     Tên người comment
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Email người comment
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Phone người comment
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    ///     Nội dung comment
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Ngày comment
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Nhóm comment
    /// </summary>
    public int GroupId { get; set; }

    /// <summary>
    ///     Mã sản phẩm, bài viết
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Mã bí mật
    /// </summary>
    public string SecrectKey { get; set; }

    /// <summary>
    ///     Mã cha - con
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    ///     Giá trị trái
    /// </summary>
    public int PLeft { get; set; }

    /// <summary>
    ///     Giá trị phải
    /// </summary>
    public int PRight { get; set; }

    /// <summary>
    ///     Người duyệt
    /// </summary>
    public string UserAgree { get; set; }

    /// <summary>
    ///     Ngày duyệt
    /// </summary>
    public DateTime StartAgree { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="content"></param>
    /// <param name="startDate"></param>
    /// <param name="groupId"></param>
    /// <param name="parentId"></param>
    /// <param name="productId"></param>
    /// <param name="createdBy"></param>
    public void Update(string name,      string email, string content, DateTime startDate, int groupId, string parentId,
                       Guid   productId, string phone, string createdBy = null)
    {
        Name      = name;
        Email     = email;
        Content   = content;
        StartDate = startDate;
        GroupId   = groupId;
        ProductId = productId;
        ParentId  = parentId;
        Phone     = phone;
        Update(createdBy);
    }


    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="content"></param>
    /// <param name="createdBy"></param>
    public void UpdateContent(string name, string email, string content, string createdBy = null)
    {
        Name    = name;
        Email   = email;
        Content = content;
        Update(createdBy);
    }

    /// <summary>
    ///     Duyệt đánh giá
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="createdBy"></param>
    public void AgreeComment(string userId, string createdBy = null)
    {
        UserAgree  = userId;
        StartAgree = DateTime.Now;
        StatusId   = ActionStatusEnum.Active.Id;
        Update(createdBy);
    }
}