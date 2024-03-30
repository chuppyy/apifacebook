using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.SystemManagers;

/// <summary>
///     Năm học
/// </summary>
public class SchoolYearManager : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected SchoolYearManager()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name">Tên năm học</param>
    /// <param name="projectId">Mã dự án</param>
    /// <param name="dateStart">Ngày bắt đầu</param>
    /// <param name="dateEnd">Ngày kết thúc</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="description">Ghi chú</param>
    /// <param name="position">Vị trí</param>
    /// <param name="createBy">Người tạo</param>
    public SchoolYearManager(Guid id, string name, Guid projectId, DateTime? dateStart, DateTime? dateEnd, int statusId,
                             string description, int position, string createBy = null) : base(id, createBy)
    {
        Update(name, projectId, dateStart, dateEnd, statusId, description, position, createBy);
    }

    /// <summary>
    ///     Tên năm học
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Ngày bắt đầu
    /// </summary>
    public DateTime? DateStart { get; set; }

    /// <summary>
    ///     Ngày kết thúc
    /// </summary>
    public DateTime? DateEnd { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên năm học</param>
    /// <param name="projectId">Mã dự án</param>
    /// <param name="dateStart">Ngày bắt đầu</param>
    /// <param name="dateEnd">Ngày kết thúc</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="description">Ghi chú</param>
    /// <param name="position">Vị trí</param>
    /// <param name="createBy">Người tạo</param>
    public void Update(string name,        Guid projectId, DateTime? dateStart, DateTime? dateEnd, int statusId,
                       string description, int  position,  string    createBy = null)
    {
        StatusId    = statusId;
        Name        = name;
        ProjectId   = projectId;
        DateStart   = dateStart;
        DateEnd     = dateEnd;
        Description = description;
        Position    = position;
        Update(createBy);
    }
}