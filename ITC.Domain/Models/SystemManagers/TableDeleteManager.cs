using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.SystemManagers;

/// <summary>
///     Danh sách table xóa
/// </summary>
public class TableDeleteManager : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected TableDeleteManager()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="code"></param>
    /// <param name="createBy"></param>
    public TableDeleteManager(Guid id, string name, int code, string createBy = null) : base(id, createBy)
    {
        Name = name;
        Code = code;
    }

    /// <summary>
    ///     Tên table
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mã code
    /// </summary>
    public int Code { get; set; }
}