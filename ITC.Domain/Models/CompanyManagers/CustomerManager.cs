using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.CompanyManagers;

/// <summary>
///     Khách hàng
/// </summary>
public class CustomerManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public CustomerManager(Guid   id, string name, string description, Guid companyId, string phone, string address,
                           string email,
                           string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Update(name, description, companyId, phone, address, email, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected CustomerManager()
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

    /// <summary>
    ///     Điện thoại
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    ///     Địa chỉ
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    ///     Email
    /// </summary>
    public string Email { get; set; }

    public void Update(string name, string description, Guid company, string phone, string address, string email,
                       string createdBy)
    {
        Name        = name;
        Description = description;
        CompanyId   = company;
        Phone       = phone;
        Address     = address;
        Email       = email;
        Update(createdBy);
    }
}