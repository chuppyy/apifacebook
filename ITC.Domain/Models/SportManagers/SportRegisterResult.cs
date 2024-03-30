using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SportManagers;

/// <summary>
///     Kết quả thi đấu
/// </summary>
public class SportRegisterResult : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public SportRegisterResult(Guid   id, Guid sportRegisterId, int roundId, int competitions, int land, bool violate,
                               string violateContent, string achievement, int pointDraw, string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Update(sportRegisterId, roundId, competitions, land, violate, violateContent, achievement, pointDraw,
               createdBy);
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public SportRegisterResult(Guid   id,             int    roundId,     int competitions, int    land, bool violate,
                               string violateContent, string achievement, int pointDraw,    string createdBy = null)
        : base(id, createdBy)
    {
        StatusId       = ActionStatusEnum.Active.Id;
        RoundId        = roundId;
        Competitions   = competitions;
        Land           = land;
        Violate        = violate;
        ViolateContent = violateContent;
        Achievement    = achievement;
        PointDraw      = pointDraw;
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected SportRegisterResult()
    {
    }

    /// <summary>
    ///     Mã đăng ký nội dung
    /// </summary>
    public Guid SportRegisterId { get; set; }

    /// <summary>
    ///     Vòng thi đấu
    /// </summary>
    public int RoundId { get; set; }

    /// <summary>
    ///     Lượt thi
    /// </summary>
    public int Competitions { get; set; }

    /// <summary>
    ///     Làn bơi
    /// </summary>
    public int Land { get; set; }

    /// <summary>
    ///     Vi phạm
    /// </summary>
    public bool Violate { get; set; }

    /// <summary>
    ///     Nội dung vi phạm
    /// </summary>
    public string ViolateContent { get; set; }

    /// <summary>
    ///     Thành tích
    /// </summary>
    public string Achievement { get; set; }

    /// <summary>
    ///     Điểm cá nhân
    /// </summary>
    public int Point { get; set; }

    /// <summary>
    ///     Xếp hạng
    /// </summary>
    public int Rank { get; set; }

    /// <summary>
    ///     Số thứ tự thăm
    /// </summary>
    public int PointDraw { get; set; }

    /// <summary>
    ///     Tổng số giây
    /// </summary>
    public int TotalSecond { get; set; }

    /// <summary>
    ///     Tổng số phần trăm giấy
    /// </summary>
    public int TotalSecondPercent { get; set; }

    /// <summary>
    ///     Lượt thi
    /// </summary>
    public int NumberOfTurn { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="sportRegisterId">Mã đăng ký</param>
    /// <param name="roundId">Vòng thi đấu</param>
    /// <param name="competitions">Lượt thi</param>
    /// <param name="land">Làn bơi</param>
    /// <param name="violate">Vi phạm</param>
    /// <param name="violateContent">Nội dung vi phạm</param>
    /// <param name="achievement">Thành tích</param>
    /// <param name="pointDraw">Số thứ tự thăm</param>
    /// <param name="created">NGười tạo</param>
    public void Update(Guid   sportRegisterId, int    roundId,     int competitions, int    land, bool violate,
                       string violateContent,  string achievement, int pointDraw,    string created = null)
    {
        SportRegisterId = sportRegisterId;
        RoundId         = roundId;
        Competitions    = competitions;
        Land            = land;
        Violate         = violate;
        ViolateContent  = violateContent;
        Achievement     = achievement;
        PointDraw       = pointDraw;
        Update(created);
    }
}