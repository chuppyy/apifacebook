using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.Itphonui;

/// <summary>
///     Cấu hình thời gian thêm - sửa  - xóa
/// </summary>
public class ProjectConfigCrudManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public ProjectConfigCrudManager(Guid     id,        Guid     projectManagerId, DateTime addStart, DateTime addEnd,
                                    DateTime editStart, DateTime editEnd, DateTime deleteStart, DateTime deleteEnd,
                                    string   createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Update(projectManagerId, addStart, addEnd, editStart, editEnd, deleteStart, deleteEnd, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected ProjectConfigCrudManager()
    {
    }

    public Guid     ProjectManagerId { get; set; }
    public DateTime AddStart         { get; set; }
    public DateTime AddEnd           { get; set; }
    public DateTime EditStart        { get; set; }
    public DateTime EditEnd          { get; set; }
    public DateTime DeleteStart      { get; set; }
    public DateTime DeleteEnd        { get; set; }
    public DateTime DrawStart        { get; set; }
    public DateTime DrawEnd          { get; set; }
    public int      OnePeoplePoint   { get; set; }
    public int      TwoPeoplePoint   { get; set; }
    public int      ThreePeoplePoint { get; set; }
    public int      FourPeoplePoint  { get; set; }
    public int      OneTeamPoint     { get; set; }
    public int      TwoTeamPoint     { get; set; }
    public int      ThreeTeamPoint   { get; set; }
    public int      FourTeamPoint    { get; set; }

    /// <summary>
    ///     Số VĐV tối đa cho mỗi vòng đấu
    /// </summary>
    public int NumberOfAthleteInRound { get; set; }

    /// <inheritdoc cref="Update" />
    public void Update(Guid     projectManagerId, DateTime addStart,    DateTime addEnd,    DateTime editStart,
                       DateTime editEnd,          DateTime deleteStart, DateTime deleteEnd, string   createdBy = null)
    {
        ProjectManagerId = projectManagerId;
        AddStart         = addStart;
        AddEnd           = addEnd;
        EditStart        = editStart;
        EditEnd          = editEnd;
        DeleteStart      = deleteStart;
        DeleteEnd        = deleteEnd;
        Update(createdBy);
    }
}