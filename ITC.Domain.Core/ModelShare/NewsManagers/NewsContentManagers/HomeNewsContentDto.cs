using System;
using System.Collections.Generic;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;

/// <summary>
///     [Home_Modal] Danh sách bài viết
/// </summary>
public class HomeNewsContentDto
{
    public string Id                { get; set; }
    public string Name              { get; set; }
    public string Author            { get; set; }
    public string Link              { get; set; }
    public string NewsGroupTypeName { get; set; }
    public string NewsGroupTypeKey  { get; set; }
    public string NewsGroupName     { get; set; }
    public string NewsGroupKey      { get; set; }
    public string DateTimeStart     { get; set; }
    public int    ViewEye           { get; set; }
    public string AvatarLink        { get; set; }
    public bool   AvatarLocal       { get; set; }
}

/// <summary>
///     [Model] Bài viết
/// </summary>
public class HomeNewsContentViewDto
{
    /// <summary>
    ///     Mã bài viết
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     Tên bài viết
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Tóm tắt
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    ///     Nội dung
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Tác giả
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    ///     Link đường dẫn gốc
    /// </summary>
    public string UrlRootLink { get; set; }

    /// <summary>
    ///     Từ khóa SEO
    /// </summary>
    public string SeoKeyword { get; set; }

    public string FilePath { get; set; }
    public string LinkUrl  { get; set; }
    public bool   IsLocal  { get; set; }

    /// <summary>
    ///     Ngày viết bài
    /// </summary>
    public string DateTimeStart { get; set; }

    /// <summary>
    ///     Chế độ hiển thị file đính kèm
    /// </summary>
    public int AttackViewId { get; set; }

    /// <summary>
    ///     Trạng thái
    /// </summary>
    public int StatusId { get; set; }

    /// <summary>
    ///     Tên nhóm bài viết
    /// </summary>
    public string NewsGroupName { get; set; }

    /// <summary>
    ///     Tên loại bài viết
    /// </summary>
    public string NewsGroupTypeName { get; set; }

    /// <summary>
    ///     Lượt xem
    /// </summary>
    public int ViewEye { get; set; }

    /// <summary>
    ///     Lượt xem
    /// </summary>
    public int Review { get; set; }

    public string NewsGroupId     { get; set; }
    public string NewsGroupTypeId { get; set; }

    /// <summary>
    ///     Danh sách file đính kèm
    /// </summary>
    public List<NewsContentAttackModel> NewsContentAttackModels { get; set; }
}

/// <summary>
///     [Model] Danh sách top 5 bài viết theo dữ liệu
/// </summary>
public class HomeNewsContentTop5Dto
{
    public string   Id            { get; set; }
    public string   Name          { get; set; }
    public DateTime DateTimeStart { get; set; }
    public string   Author        { get; set; }
    public string   Route         { get; set; }
    public string   AvatarLink    { get; set; }
    public bool     AvatarLocal   { get; set; }
}

/// <summary>
///     [Model] Danh sách top 5 bài viết theo dữ liệu
/// </summary>
public class Top5ViewEye
{
    public string Id       { get; set; }
    public string Name     { get; set; }
    public string Author   { get; set; }
    public int    ViewEye  { get; set; }
    public string Url      { get; set; }
    public string FilePath { get; set; }
    public string LinkUrl  { get; set; }
    public bool   IsLocal  { get; set; }
    public int    Width    { get; set; }
    public int    Height   { get; set; }
}