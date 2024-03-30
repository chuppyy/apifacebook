using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;

/// <summary>
///     Modal phân trang bài viết
/// </summary>
public class NewsContentPagingDto
{
    public              Guid                       Id            { get; set; }
    public              string                     Name          { get; set; }
    public              string                     NewsGroupName { get; set; }
    [JsonIgnore] public string                     OwnerId       { get; set; }
    public              int                        StatusId      { get; set; }
    public              bool                       AvatarLocal   { get; set; }
    public              string                     AvatarLink    { get; set; }
    public              string                     OwnerName     { get; set; }
    public              string                     LinkTree      { get; set; }
    public              DateTime                   DateTimeStart { get; set; }
    public              DateTime?                  TimeAutoPost  { get; set; }
    public              List<ActionAuthorityModel> Actions       { get; set; }
    [JsonIgnore] public int                        TotalRecord   { get; set; }
}

/// <summary>
///     Modal phân trang bài viết
/// </summary>
public class NewsContentPagingByIdDto
{
    public              Guid      Id           { get; set; }
    public              string    Name         { get; set; }
    public              string    MetaName     { get; set; }
    public              string    MetaKey      { get; set; }
    public              string    Domain       { get; set; }
    public              string    UserCode     { get; set; }
    public int TypeId { get; set; }
}

/// <summary>
///     Modal phân trang danh sách combobox
/// </summary>
public class NewsContentPagingComboboxDto
{
    public              string   Id            { get; set; }
    public              string   Name          { get; set; }
    public              string   Summary       { get; set; }
    public              string   Author        { get; set; }
    public              string   NewsGroupName { get; set; }
    public              DateTime DateTimeStart { get; set; }
    [JsonIgnore] public int      TotalRecord   { get; set; }
}

public class AvatarModal
{
    public Guid   Id      { get; set; }
    public bool   IsLocal { get; set; }
    public string Name    { get; set; }
    public string Link    { get; set; }
}

public class ReadLink
{
    public bool   IsTrue { get; set; }
    public string Title  { get; set; }
    public Guid   ImageId  { get; set; }
    public string ImageLink  { get; set; }
    public string Body   { get; set; }
}

public class PostNewFaceEvent
{
    public Guid   Id   { get; set; }
    public string Host { get; set; }
    public bool? IsPostImg { get; set; }
}

public class PostNewFaceError
{
    public bool   Result { get; set; }
    public string Value  { get; set; }
}

public class AutoPostFaceModel
{
    public Guid   Id          { get; set; }
    public string Name        { get; set; }
    public string TkqcId      { get; set; }
    public string Token       { get; set; }
    public string PageId      { get; set; }
    public bool   AvatarLocal { get; set; }
    public string AvatarLink  { get; set; }
    public string LinkUrl     { get; set; }
    public int TypeId     { get; set; }
}

public class UploadFileDto
{
    public string Link { get; set; }
}