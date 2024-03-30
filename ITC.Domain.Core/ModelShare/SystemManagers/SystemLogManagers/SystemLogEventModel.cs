using System;
using ITC.Domain.Core.Events;

namespace ITC.Domain.Core.ModelShare.SystemManagers.SystemLogManagers;

public class SystemLogEventModel : Event
{
    protected SystemLogEventModel(int    systemLogType,
                                  string nameFile,
                                  string description,
                                  Guid?  dataId,
                                  string dataOld,
                                  string dataNew,
                                  string userId,
                                  string userName)
    {
        SystemLogType  = systemLogType;
        NameFile       = nameFile;
        Description    = description;
        DataId         = dataId;
        DataOld        = dataOld;
        DataNew        = dataNew;
        UserCreateId   = userId;
        UserCreateName = userName;
    }

    /// <summary>
    ///     Loại nhật ký
    /// </summary>
    public int SystemLogType { get; set; }

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

    /// <summary>
    ///     Người tạo [ID]
    /// </summary>
    public string UserCreateId { get; set; }

    /// <summary>
    ///     Người tạo [NAME]
    /// </summary>
    public string UserCreateName { get; set; }
}