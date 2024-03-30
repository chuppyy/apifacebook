using System.Collections.Generic;
using System.Linq;
using NCore.Systems;

namespace ITC.Domain.Core.NCoreLocal.Enum;

/// <summary>
///     Enum loại xử lý liên hệ khách hàng
/// </summary>
public class HandlerContactManagerEnum : EnumerationCore
{
    public static readonly HandlerContactManagerEnum ChoXuLy   = new(1, "Chờ xử lý");
    public static readonly HandlerContactManagerEnum DaXuLy    = new(2, "Đã xử lý");
    public static readonly HandlerContactManagerEnum KhongXuLy = new(3, "Không xử lý");

    protected HandlerContactManagerEnum(int id, string name) : base(id, name)
    {
    }

    public HandlerContactManagerEnum()
    {
    }

    /// <summary>
    ///     GetList
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<HandlerContactManagerEnum> GetList()
    {
        return GetAll<HandlerContactManagerEnum>();
    }

    /// <summary>
    ///     Lấy dữ liệu theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static HandlerContactManagerEnum GetById(int id)
    {
        return GetList().FirstOrDefault(x => x.Id == id);
    }
}