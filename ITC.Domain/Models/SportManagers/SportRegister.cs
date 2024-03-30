using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SportManagers;

/// <summary>
///     Đăng ký thi đấu
/// </summary>
public class SportRegister : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public SportRegister(Guid id,       Guid   staffId, Guid subjectId, int levelId, Guid subjectDetailId,
                         int  position, string createdBy = null)
        : base(id, createdBy)
    {
        StatusId             = ActionStatusEnum.Active.Id;
        SportRegisterResults = new List<SportRegisterResult>();
        Update(staffId, subjectId, levelId, subjectDetailId, position, createdBy);
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public SportRegister(Guid id,       Guid   subjectId, int levelId, Guid subjectDetailId,
                         int  position, string createdBy = null)
        : base(id, createdBy)
    {
        StatusId             = ActionStatusEnum.Active.Id;
        SportRegisterResults = new List<SportRegisterResult>();
        SubjectId            = subjectId;
        LevelId              = levelId;
        SubjectDetailId      = subjectDetailId;
        Position             = position;
        IsDraw               = false;
        DrawDateTime         = DateTime.Now;
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected SportRegister()
    {
    }

    /// <summary>
    ///     Mã VĐV
    /// </summary>
    public Guid StaffManagerId { get; set; }

    /// <summary>
    ///     Môn thi đấu
    /// </summary>
    public Guid SubjectId { get; set; }

    /// <summary>
    ///     Bậc thi đấu
    /// </summary>
    public int LevelId { get; set; }

    /// <summary>
    ///     Nội dung thi đấu
    /// </summary>
    public Guid SubjectDetailId { get; set; }

    /// <summary>
    ///     Số thứ tự
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Boc tham
    /// </summary>
    public bool IsDraw { get; set; }

    /// <summary>
    ///     Thoi gian boc tham
    /// </summary>
    public DateTime DrawDateTime { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<SportRegisterResult> SportRegisterResults { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="staffId">mã người dùng</param>
    /// <param name="subjectId">môn thi</param>
    /// <param name="levelId">bậc thi đấu</param>
    /// <param name="subjectDetailId">nội dung thi</param>
    /// <param name="position">Số thứ tự</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(Guid staffId,  Guid   subjectId, int levelId, Guid subjectDetailId,
                       int  position, string createdBy = null)
    {
        StaffManagerId  = staffId;
        SubjectId       = subjectId;
        LevelId         = levelId;
        SubjectDetailId = subjectDetailId;
        Position        = position;
        Update(createdBy);
    }

    /// <summary>
    ///     Thêm mới kết quả
    /// </summary>
    public void AddResult()
    {
    }
}