using System.Collections.Generic;
using System.Linq;
using NCore.Systems;

namespace ITC.Domain.Core.NCoreLocal.Enum;

/// <summary>
///     Enum loại hiển thị slide
/// </summary>
public class SlideTypeViewEnum : EnumerationCore
{
    public static readonly SlideTypeViewEnum Boder01 = new(1, "Khung 01");
    public static readonly SlideTypeViewEnum Boder02 = new(2, "Khung 02");
    public static readonly SlideTypeViewEnum Boder03 = new(3, "Khung 03");

    protected SlideTypeViewEnum(int id, string name) : base(id, name)
    {
    }

    public SlideTypeViewEnum()
    {
    }

    /// <summary>
    ///     GetList
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<SlideTypeViewEnum> GetList()
    {
        return GetAll<SlideTypeViewEnum>();
    }

    /// <summary>
    ///     Lấy dữ liệu theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static SlideTypeViewEnum GetById(int id)
    {
        return GetList().FirstOrDefault(x => x.Id == id);
    }
}