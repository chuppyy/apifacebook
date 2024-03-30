using System.Collections.Generic;
using NCore.Systems;

namespace ITC.Application.ViewModels;

/// <summary>
///     Class cơ bản import file
/// </summary>
public class ImportExcelQueryBase
{
    /// <summary>
    ///     Kiểm tra validate
    ///     Nếu Validate = true, chỉ kiểm tra lỗi, ko thêm vào db
    ///     Nếu validate = false, thêm vào db
    /// </summary>
    public bool IsValidate { get; set; }
}

/// <summary>
///     Class cơ bản query import excel
/// </summary>
public class ImportExcelBase
{
    /// <summary>
    ///     Kiểm tra có import thành công hay không
    /// </summary>
    public bool? IsSuccess { get; set; }

    /// <summary>
    ///     Các lỗi nếu có
    /// </summary>
    public List<ErrorDetail> MessErr { get; set; }
}