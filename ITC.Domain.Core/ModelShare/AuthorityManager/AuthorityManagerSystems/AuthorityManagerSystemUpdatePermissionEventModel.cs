using System;
using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.AuthorityManager.AuthorityManagerSystems;

/// <summary>
///     [Model] Cập nhật giá trị quyền sử dụng cho menu
/// </summary>
public class AuthorityManagerSystemUpdatePermissionEventModel : PublishModal
{
    public List<AuthorityManagerSystemUpdatePermissionEventDto> Model              { get; set; }
    public bool                                                 IsUpdatePermission { get; set; }
}

public class AuthorityManagerSystemUpdatePermissionEventDto
{
    public Guid   Id       { get; set; }
    public string Label    { get; set; }
    public int    Value    { get; set; }
    public int    Position { get; set; }
}