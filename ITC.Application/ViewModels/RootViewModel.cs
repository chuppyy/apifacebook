#region

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace ITC.Application.ViewModels;

public class RootViewModel
{
#region Properties

    public string   CreateBy { get; set; }
    public DateTime Created  { get; set; }

    /// <summary>
    ///     Thông tin lỗi
    ///     Dùng cho nạp từ tập tin
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    ///     Tồn tại lỗi
    ///     Dùng cho nạp từ tập tin
    /// </summary>
    public bool HasError { get; set; }

    public string Hidden { get; set; }

    public bool   IsRegister { get; set; } = true;
    public bool   IsShow     { get; set; } = true;
    public string ModifiedBy { get; set; }

    public int PortalId { get; set; }

    /// <summary>
    ///     Tổng số bản ghi
    /// </summary>
    public int TotalRecord { get; set; }

    /// <summary>
    ///     Mã người dùng
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public string ManagementId { get; set; }

#endregion
}

public class RootViewModelMini
{
    /// <summary>
    ///     Mã người dùng
    /// </summary>
    [IgnoreDataMember]
    public string CreateBy { get; set; }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    [IgnoreDataMember]
    public string ManagementId { get; set; }
}

public class FileReportViewModel
{
#region Properties

    public List<string> ListTempFile { get; set; }
    public string       TempFile     { get; set; }
    public string       TempFileHost { get; set; }
    public string       TempFileName { get; set; }

#endregion
}