using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SportManagers;

/// <summary>
///     Quản lý nội dung thi đấu
/// </summary>
public class SportSubjectManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public SportSubjectManager(Guid   id,       string name,   int    sexId, int levelId, Guid subjectTypeManagerId,
                               int    position, bool   isTeam, string description, int numberOfPeopleInRound,
                               string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Update(name, sexId, levelId, subjectTypeManagerId, position, isTeam, description, numberOfPeopleInRound,
               createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected SportSubjectManager()
    {
    }

    /// <summary>
    ///     Tên nội dung
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Giới tính
    /// </summary>
    public int SexId { get; set; }

    /// <summary>
    ///     Bậc thi đấu
    /// </summary>
    public int LevelId { get; set; }

    /// <summary>
    ///     Môn thi đấu
    /// </summary>
    public Guid SubjectTypeManagerId { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Là môn đồng đội
    /// </summary>
    public bool IsTeam { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Số lượng VĐV tối đa cho 1 vòng đấu
    /// </summary>
    public int NumberOfPeopleInRound { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên nội dung</param>
    /// <param name="sexId">Giới tính</param>
    /// <param name="levelId">Bậc thi đấu</param>
    /// <param name="subjectTypeManagerId">Môn thi đấu</param>
    /// <param name="position">Vị trí</param>
    /// <param name="isTeam"></param>
    /// <param name="description"></param>
    /// <param name="numberOfPeopleInRound"></param>
    /// <param name="createdBy"></param>
    public void Update(string name,        int sexId, int levelId, Guid subjectTypeManagerId, int position, bool isTeam,
                       string description, int numberOfPeopleInRound, string createdBy = null)
    {
        Name                  = name;
        SexId                 = sexId;
        LevelId               = levelId;
        SubjectTypeManagerId  = subjectTypeManagerId;
        Position              = position;
        IsTeam                = isTeam;
        Description           = description;
        NumberOfPeopleInRound = numberOfPeopleInRound;
        Update(createdBy);
    }
}