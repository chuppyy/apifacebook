using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.ProductManagers;

/// <summary>
///     Loại giá dành riêng cho từng khách hàng
/// </summary>
public class CustomerPriceManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public CustomerPriceManager(Guid   id, Guid customerId, double price, int position, int statusId,
                                string createdBy = null)
        : base(id, createdBy)
    {
        Update(customerId, price, position, statusId, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected CustomerPriceManager()
    {
    }

    /// <summary>
    ///     Đơn giá
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Mã khách hàng
    /// </summary>
    public Guid CustomerManagerId { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="customerId">mã khách hàng</param>
    /// <param name="price">đơn giá</param>
    /// <param name="position">vị trí</param>
    /// <param name="statusId">trạng thái</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(Guid customerId, double price, int position, int statusId, string createdBy = null)
    {
        CustomerManagerId = customerId;
        Price             = price;
        StatusId          = statusId;
        Position          = position;
        Update(createdBy);
    }
}