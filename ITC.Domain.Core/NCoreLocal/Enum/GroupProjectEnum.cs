using System.Collections.Generic;
using System.Linq;
using NCore.Systems;

namespace ITC.Domain.Core.NCoreLocal.Enum;

/// <summary>
///     Enum nhóm các dự án để tiến hành bốc thăm
/// </summary>
public class GroupProjectEnum : EnumerationCore
{
    /// <summary>
    ///     Bơi
    /// </summary>
    public static readonly GroupProjectEnum Swimming = new(1, "Bơi");

    protected GroupProjectEnum(int id, string name) : base(id, name)
    {
    }

    public GroupProjectEnum()
    {
    }

    /// <summary>
    ///     GetList
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<GroupProjectEnum> GetList()
    {
        return GetAll<GroupProjectEnum>();
    }

    /// <summary>
    ///     Lấy dữ liệu theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static GroupProjectEnum GetById(int id)
    {
        return GetList().FirstOrDefault(x => x.Id == id);
    }
}