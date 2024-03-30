using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.ContactCustomerManager;

/// <summary>
///     [Model] nhận dữ liệu liên hệ từ FE
/// </summary>
public class ContactCustomerManagerEventModel : PublishModal
{
    public Guid Id { get; set; }

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
    ///     Nội dung
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Thời gian liên hệ
    /// </summary>
    public DateTime ContactDateTime { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }
}

/// <summary>
///     [Model-nhân viên xử lý] nhận dữ liệu liên hệ từ FE
/// </summary>
public class ContactCustomerHandlerManagerEventModel : PublishModal
{
    public Guid Id { get; set; }

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
}

/// <summary>
///     [Model] Trả về ContactCustomerManagerGetByIdModel
/// </summary>
public class ContactCustomerManagerGetByIdModel : ContactCustomerManagerEventModel
{
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
}