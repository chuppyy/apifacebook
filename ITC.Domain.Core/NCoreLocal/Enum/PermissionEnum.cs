using System.Collections.Generic;
using System.Linq;
using NCore.Systems;

namespace ITC.Domain.Core.NCoreLocal.Enum;

/// <summary>
///     Enum bậc tham gia thi đấu
/// </summary>
public class PermissionEnum : EnumerationCore
{
    public static readonly PermissionEnum Xem           = new(1, "Xem");
    public static readonly PermissionEnum Them          = new(2, "Thêm");
    public static readonly PermissionEnum Sua           = new(4, "Sửa");
    public static readonly PermissionEnum Xoa           = new(5, "Xóa");
    public static readonly PermissionEnum Duyet         = new(16, "Duyệt");
    public static readonly PermissionEnum XemTacGiaKhac = new(32, "Xem dữ liệu tác giả khác");
    public static readonly PermissionEnum ChayTuDong    = new(64, "Chạy lịch tự động");

    protected PermissionEnum(int id, string name) : base(id, name)
    {
    }

    public PermissionEnum()
    {
    }

    /// <summary>
    ///     GetList
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<PermissionEnum> GetList()
    {
        return GetAll<PermissionEnum>();
    }

    /// <summary>
    ///     Lấy dữ liệu theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static PermissionEnum GetById(int id)
    {
        return GetList().FirstOrDefault(x => x.Id == id);
    }
}