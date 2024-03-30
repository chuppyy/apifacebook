using System.Collections.Generic;
using System.Linq;
using NCore.Systems;

namespace ITC.Domain.Core.NCoreLocal.Enum;

/// <summary>
///     Enum bậc tham gia thi đấu
/// </summary>
public class LevelCompetitionEnum : EnumerationCore
{
    public static readonly LevelCompetitionEnum TieuHoc    = new(1, "Tiểu học");
    public static readonly LevelCompetitionEnum Thcs       = new(2, "Trung học cơ sở");
    public static readonly LevelCompetitionEnum Thpt       = new(3, "Trung học phổ thông");
    public static readonly LevelCompetitionEnum ThcsThpt   = new(4, "THCS - THPT");
    public static readonly LevelCompetitionEnum ThThcsThpt = new(5, "TH - THCS - THPT");

    protected LevelCompetitionEnum(int id, string name) : base(id, name)
    {
    }

    /// <summary>
    ///     GetList
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<LevelCompetitionEnum> GetList()
    {
        return new List<LevelCompetitionEnum>
        {
            TieuHoc,
            Thcs,
            Thpt,
            ThcsThpt,
            ThThcsThpt
        };
    }

    /// <summary>
    ///     Lấy dữ liệu theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static LevelCompetitionEnum GetById(int id)
    {
        return GetList().FirstOrDefault(x => x.Id == id);
    }
}