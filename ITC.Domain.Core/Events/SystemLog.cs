using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Core.Events;

/// <summary>
///     Nhật ký hệ thống
/// </summary>
public class SystemLog : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected SystemLog()
    {
    }

    /// <summary>
    ///     Hàm dựng dữ liệu
    /// </summary>
    /// <param name="id">Mã dữ liệu</param>
    /// <param name="actionTime">Thời gian diễn ra</param>
    /// <param name="systemLogType">Loại hành động</param>
    /// <param name="userCreateId">Mã người tạo</param>
    /// <param name="userCreateName">Tên người tạo</param>
    /// <param name="nameFile">Tên file</param>
    /// <param name="description">Mô tả</param>
    /// <param name="dataId">Mã dữ liệu</param>
    /// <param name="dataOld">Dữ liệu cũ</param>
    /// <param name="dataNew">Dữ liệu mới</param>
    /// <param name="createdBy">Người tạo</param>
    public SystemLog(Guid     id,
                     DateTime actionTime,
                     int      systemLogType,
                     string   userCreateId,
                     string   userCreateName,
                     string   nameFile,
                     string   description,
                     Guid?    dataId,
                     string   dataOld,
                     string   dataNew,
                     string   createdBy = null) : base(id, createdBy)
    {
        ActionTime     = actionTime;
        SystemLogType  = systemLogType;
        UserCreateId   = userCreateId;
        UserCreateName = userCreateName;
        NameFile       = nameFile;
        Description    = description;
        DataId         = dataId;
        DataOld        = dataOld;
        DataNew        = dataNew;
    }

    /// <summary>
    ///     Thời gian thực hiện hành động
    /// </summary>
    public DateTime ActionTime { get; set; }

    /// <summary>
    ///     Loại nhật ký
    /// </summary>
    public int SystemLogType { get; set; }

    /// <summary>
    ///     Người tạo [ID]
    /// </summary>
    public string UserCreateId { get; set; }

    /// <summary>
    ///     Người tạo [NAME]
    /// </summary>
    public string UserCreateName { get; set; }

    /// <summary>
    ///     Tên file
    /// </summary>
    public string NameFile { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Mã dữ liệu
    /// </summary>
    public Guid? DataId { get; set; }

    /// <summary>
    ///     Dữ liệu cũ
    /// </summary>
    public string DataOld { get; set; }

    /// <summary>
    ///     Dữ liệu mới
    /// </summary>
    public string DataNew { get; set; }
}