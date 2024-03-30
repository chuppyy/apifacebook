using System;
using System.Collections.Generic;

namespace ITC.Domain.Core.ModelShare;

/// <summary>
///     Class trả về cây thư mục
/// </summary>
public class TreeViewModel
{
    public IEnumerable<TreeViewModel> Childrens   { get; set; }
    public string                     Code        { get; set; }
    public string                     Description { get; set; }
    public Guid                       Id          { get; set; }
    public string                     Name        { get; set; }
    public string                     ParentName  { get; set; }
    public int                        ParentCount { get; set; }
    public string                     ParentId    { get; set; }
}

/// <summary>
///     Class trả về cây thư mục
/// </summary>
public class TreeViewCompanyModel
{
    public IEnumerable<TreeViewCompanyModel> Children     { get; set; }
    public Guid                              Id           { get; set; }
    public string                            Text         { get; set; }
    public string                            Label        { get; set; }
    public string                            ParentId     { get; set; }
    public bool                              Opened       { get; set; }
    public bool                              OpenedAction { get; set; }
    public bool                              Selected     { get; set; }
}

/// <summary>
///     Class trả về cây thư mục
/// </summary>
public class PermissionDefaultViewModal
{
    public int    Id      { get; set; }
    public string Name    { get; set; }
    public int    Value   { get; set; }
    public string Checked { get; set; }
}

/// <summary>
///     Class trả về cây thư mục
/// </summary>
public class v2023PermissionDefaultViewModal
{
    public int    Id      { get; set; }
    public string Name    { get; set; }
    public int    Value   { get; set; }
    public bool   Checked { get; set; }
}