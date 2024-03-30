using System.Collections.Generic;
using System.Linq;
using NCore.Systems;

namespace ITC.Domain.Core.NCoreLocal.Enum;

/// <summary>
///     Enum loại hiển thị bài viết trong slide Manager
/// </summary>
public class UrlTypeViewEnum : EnumerationCore
{
    public static readonly UrlTypeViewEnum NewsInSytemt   = new(1, "Bài viết hệ thống");
    public static readonly UrlTypeViewEnum NewsAbout      = new(2, "Giới thiệu");
    public static readonly UrlTypeViewEnum NewsDiffFerent = new(3, "Khác");

    protected UrlTypeViewEnum(int id, string name) : base(id, name)
    {
    }

    public UrlTypeViewEnum()
    {
    }

    /// <summary>
    ///     GetList
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<UrlTypeViewEnum> GetList()
    {
        return GetAll<UrlTypeViewEnum>();
    }

    /// <summary>
    ///     Lấy dữ liệu theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static UrlTypeViewEnum GetById(int id)
    {
        return GetList().FirstOrDefault(x => x.Id == id);
    }
}