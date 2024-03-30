#region

using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;
using ITC.Domain.Extensions;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class Module : EntityString
{
#region Fields

    private readonly List<ModuleRole> _moduleRoles;

#endregion

#region Constructors

    public Module()
    {
        IsDeleted               = false;
        Functions               = new HashSet<Function>();
        ModuleDecentralizations = new List<ModuleDecentralization>();
        _moduleRoles            = new List<ModuleRole>();
    }

    public Module(string name, string identity, string groupId, string description, bool isActivation) : this()
    {
        Name          = name;
        Identity      = identity;
        Description   = description;
        CreateDate    = DateTime.UtcNow;
        IsActivation  = isActivation;
        ModuleGroupId = groupId;
    }

#endregion

#region Properties

    /// <summary>
    ///     Ngày tạo
    /// </summary>
    public DateTime CreateDate { get; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; private set; }

    public HashSet<Function> Functions { get; }

    /// <summary>
    ///     Định danh
    /// </summary>
    public string Identity { get; private set; }

    /// <summary>
    ///     Trạng thái sử dụng
    /// </summary>
    public bool IsActivation { get; private set; }

    /// <summary>
    ///     Trạng thái xóa
    /// </summary>
    public bool IsDeleted { get; private set; }

    public         List<ModuleDecentralization> ModuleDecentralizations { get; }
    public virtual ModuleGroup                  ModuleGroup             { get; set; }

    //public string RoleId { get; private set; }
    //public ApplicationRole IdentityRole { get; private set; }
    public string                          ModuleGroupId { get; set; }
    public IReadOnlyCollection<ModuleRole> ModuleRoles   => _moduleRoles;

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; private set; }

#endregion

#region Methods

    public void AddFunctions(List<Function> functions)
    {
        Functions.AddRange(functions);
    }

    public void AddModuleRoles(List<ModuleRole> moduleRoles)
    {
        _moduleRoles.AddRange(moduleRoles);
    }

    /// <summary>
    ///     Thay đổi trạng thái sử dụng
    /// </summary>
    /// <param name="isActivation"></param>
    public void SetActivation(bool isActivation)
    {
        IsActivation = isActivation;
    }

    /// <summary>
    ///     Thay đổi trạng thái xóa
    /// </summary>
    /// <param name="isDelete"></param>
    public void SetDelete(bool isDelete)
    {
        IsDeleted = isDelete;
    }

    public void UpdateModule(string name, string identity, string groupId, string description)
    {
        Name          = name;
        Identity      = identity;
        Description   = description;
        ModuleGroupId = groupId;
    }

#endregion
}