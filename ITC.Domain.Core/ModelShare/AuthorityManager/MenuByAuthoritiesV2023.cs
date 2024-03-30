using System;
using System.Collections.Generic;

namespace ITC.Domain.Core.ModelShare.AuthorityManager;

/// <summary>
///     Modal trả về danh sách menu theo quyền sử dụng đang chọn
/// </summary>
public class MenuByAuthoritiesV2023
{
    public IEnumerable<MenuByAuthoritiesV2023> Nodes      { get; set; }
    public Guid                                Id         { get; set; }
    public Guid                                MenuId     { get; set; }
    public string                              MenuParent { get; set; }
    public string                              Label      { get; set; }
    public string                              MenuName   { get; set; }
    public int                                 Position   { get; set; }
    public int                                 Value      { get; set; }
}