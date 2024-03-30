using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;

namespace ITC.Domain.Core.ModelShare.AuthorityManager;

public class MenuRoleViewModel
{
    public Guid   MenuManagerId    { get; set; }
    public int    MenuManagerValue { get; set; }
    public string ICon             { get; set; }
    public string Name             { get; set; }
    public string ParentId         { get; set; }
    public int    Position         { get; set; }
    public int    MenuGroupId      { get; set; }
    public string Router           { get; set; }
}

public class MenuRoleReturnViewModel
{
    public string                               Id          { get; set; }
    public Guid                                 Code        { get; set; }
    public string                               Icon        { get; set; }
    public string                               Label       { get; set; }
    public string                               Title       { get; set; }
    public int                                  Value       { get; set; }
    public string                               To          { get; set; }
    public string                               ParentId    { get; set; }
    public int                                  MenuGroupId { get; set; }
    public int                                  Position    { get; set; }
    public IEnumerable<MenuRoleReturnViewModel> Subs        { get; set; }
    public IEnumerable<MenuRoleReturnViewModel> Children    { get; set; }
}

public class MenuRoleEventViewModel
{
    public IEnumerable<MenuRoleEventViewModel> Children    { get; set; }
    public Guid                                Id          { get; set; }
    public Guid                                ParentId    { get; set; }
    public string                              Text        { get; set; }
    public int                                 Value       { get; set; }
    public bool                                Selected    { get; set; }
    public bool                                Opened      { get; set; }
    public int                                 ParentCount { get; set; }
}

public class MenuByAuthoritiesViewModel
{
    public Guid   Id                     { get; set; }
    public string Name                   { get; set; }
    public string ParentId               { get; set; }
    public int    PermissionValue        { get; set; }
    public int    PermissionValueDefault { get; set; }
    public int    MenuGroupId            { get; set; }
    public int    Position               { get; set; }
}

public class AuthoritiesViewModel
{
    public              Guid   Id          { get; set; }
    public              string Name        { get; set; }
    public              int    StatusId    { get; set; }
    public              string StatusName  => ActionStatusEnum.GetById(StatusId)?.Name;
    [JsonIgnore] public int    TotalRecord { get; set; }
}

public class MenuByAuthoritiesSaveModel
{
    public Guid   Id       { get; set; }
    public Guid   ParentId { get; set; }
    public string Name     { get; set; }
    public int    Value    { get; set; }
}