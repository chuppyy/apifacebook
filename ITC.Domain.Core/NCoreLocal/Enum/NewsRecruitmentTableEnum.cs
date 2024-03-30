using System.Collections.Generic;
using System.Linq;
using NCore.Systems;

namespace ITC.Domain.Core.NCoreLocal.Enum;

/// <summary>
///     Enum nhóm dữ liệu cùng cấu trúc NewsRecruitmentTableEnum
/// </summary>
public class NewsRecruitmentTableEnum : EnumerationCore
{
    /// <summary>
    ///     Loại môn học
    /// </summary>
    public static readonly NewsRecruitmentTableEnum NewsRecruitment = new(1, "Tuyển dụng");

    /// <summary>
    ///     Kiểu người dùng
    /// </summary>
    public static readonly NewsRecruitmentTableEnum Quote = new(2, "Báo giá");

    protected NewsRecruitmentTableEnum(int id, string name) : base(id, name)
    {
    }

    public NewsRecruitmentTableEnum()
    {
    }

    /// <summary>
    ///     GetList
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<NewsRecruitmentTableEnum> GetList()
    {
        return GetAll<NewsRecruitmentTableEnum>();
    }

    /// <summary>
    ///     Lấy dữ liệu theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static NewsRecruitmentTableEnum GetById(int id)
    {
        return GetList().FirstOrDefault(x => x.Id == id);
    }
}