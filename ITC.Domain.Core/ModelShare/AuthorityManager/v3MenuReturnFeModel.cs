using System;
using System.Collections.Generic;

namespace ITC.Domain.Core.ModelShare.AuthorityManager;

/// <summary>
///     [v3_2023] Model trả về dữ liệu menu
/// </summary>
public class v3MenuReturnFeModel
{
    /// <summary>
    ///     ICon
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    ///     Tên dữ liệu tương ứng trong project FE
    /// </summary>
    public string PageName { get; set; }

    /// <summary>
    ///     Tên hiển thị
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Menu con
    /// </summary>
    public List<v3MenuReturnFeModel> SubMenu { get; set; }

    public string ParentId { get; set; }
    public int    Position { get; set; }
    public Guid   Code     { get; set; }
    public int    Value    { get; set; }
    public string To       { get; set; }
    public string Id       { get; set; }
    public string Label    { get; set; }
}