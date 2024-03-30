using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     Quản lý khách hàng liên hệ
/// </summary>
public class ContactCustomerManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public ContactCustomerManager(Guid   id, string name, string phone, string email, string address, Guid projectId,
                                  string content, DateTime contactDateTime, int handlerTypeId, string handlerUser,
                                  string handlerContent, DateTime handlerDateTime, string createdBy = null) :
        base(id, createdBy)
    {
        StatusId  = ActionStatusEnum.Active.Id;
        ProjectId = projectId;
        Update(name,           phone,           email, address, content, contactDateTime, handlerTypeId, handlerUser,
               handlerContent, handlerDateTime, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected ContactCustomerManager()
    {
    }

    /// <summary>
    ///     Tên liên hệ
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Điện thoại
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    ///     Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Địa chỉ
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Nội dung
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Thời gian liên hệ
    /// </summary>
    public DateTime ContactDateTime { get; set; }

    /// <summary>
    ///     Loại xử lý
    /// </summary>
    public int HandlerTypeId { get; set; }

    /// <summary>
    ///     Người xử lý
    /// </summary>
    public string HandlerUser { get; set; }

    /// <summary>
    ///     Nội dung xử lý
    /// </summary>
    public string HandlerContent { get; set; }

    /// <summary>
    ///     Thời gian xử lý
    /// </summary>
    public DateTime HandlerDateTime { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Họ và tên</param>
    /// <param name="phone">Điện thoại</param>
    /// <param name="email">Email</param>
    /// <param name="address">Địa chỉ</param>
    /// <param name="content">Nội dung liên hệ</param>
    /// <param name="contactDateTime">Thời gian liên hệ</param>
    /// <param name="handlerTypeId">Loại xử lý</param>
    /// <param name="handlerUser">Người xử lý</param>
    /// <param name="handlerContent">Nội dung xử lý</param>
    /// <param name="handlerDateTime">Thời gian xử lý</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string   name,            string phone,         string email, string address, string content,
                       DateTime contactDateTime, int    handlerTypeId, string handlerUser, string handlerContent,
                       DateTime handlerDateTime, string createdBy = null)
    {
        Name            = name;
        Phone           = phone;
        Email           = email;
        Address         = address;
        Content         = content;
        ContactDateTime = contactDateTime;
        HandlerTypeId   = handlerTypeId;
        HandlerUser     = handlerUser;
        HandlerContent  = handlerContent;
        HandlerDateTime = handlerDateTime;
        Update(createdBy);
    }
}