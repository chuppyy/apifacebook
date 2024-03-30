#region

using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class Function : EntityString
{
#region Fields

    private readonly List<FunctionRole> _functionRoles;

#endregion

#region Constructors

    public Function()
    {
        IsActivation              = true;
        FunctionDecentralizations = new HashSet<FunctionDecentralization>();
        _functionRoles            = new List<FunctionRole>();
    }

    public Function(string name, int weight, string description) : this()
    {
        Name        = name;
        Weight      = weight;
        Description = description;
        CreateDate  = DateTime.UtcNow;
    }

    public Function(string name, int weight, string description, string moduleId) : this(name, weight, description)
    {
        ModuleId = moduleId;
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

    public HashSet<FunctionDecentralization> FunctionDecentralizations { get; set; }
    public IReadOnlyCollection<FunctionRole> FunctionRoles             => _functionRoles;

    /// <summary>
    ///     Trạng thái sử dụng
    /// </summary>
    public bool IsActivation { get; }

    /// <summary>
    ///     Mã module
    /// </summary>
    public string ModuleId { get; private set; }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     Trọng số
    /// </summary>
    public int Weight { get; private set; }

#endregion

#region Methods

    public void AddFunctionRoles(List<FunctionRole> functionRoles)
    {
        _functionRoles.AddRange(functionRoles);
    }


    public void UpdateFunction(string name, int weight, string description, string moduleId)
    {
        Name        = name;
        Weight      = weight;
        Description = description;
        ModuleId    = moduleId;
    }

#endregion
}