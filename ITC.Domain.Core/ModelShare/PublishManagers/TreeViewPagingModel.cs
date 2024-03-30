using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.PublishManagers;

/// <summary>
///     [Model] DÙng truyền dữ liệu trong TreeView
/// </summary>
public class TreeViewPagingModel : ModuleIdentityModel
{
    public string VSearch         { get; set; }
    public Guid   NewsGroupTypeId { get; set; }
    public bool   IsAll           { get; set; }
    public int    TypeId          { get; set; }

    /// <summary>
    ///     Là lấy dữ liệu từ modal
    /// </summary>
    public bool IsModal { get; set; }
}

/// <summary>
///     [Model] DÙng truyền dữ liệu trong TreeView
/// </summary>
public class TreeViewPagingModelLibrary : ModuleIdentityModel
{
    public string VSearch { get; set; }
    public string UserId  { get; set; }
}