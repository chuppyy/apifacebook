using System.Collections.Generic;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsContentManagers;

/// <summary>
///     Command cập nhật bài viết
/// </summary>
public class UpdateNewsContentCommand : NewsContentCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateNewsContentCommand(NewsContentEventModel model)
    {
        Id                       = model.Id;
        Name                     = model.Name;
        Summary                  = model.Summary;
        Content                  = model.Content;
        Author                   = model.Author;
        UrlRootLink              = model.UrlRootLink;
        NewsGroupId              = model.NewsGroupId;
        SeoKeyword               = model.SeoKeyword;
        AvatarId                 = model.AvatarId;
        DateTimeStart            = model.DateTimeStart;
        NewsGroupTypeId          = model.NewsGroupTypeId;
        AttackViewId             = model.AttackViewId;
        StatusId                 = model.StatusId;
        NewsContentAttackModels  = model.NewsContentAttackModels;
        NewsContentContentModels = model.NewsContentContentModels;
        IsMinusWord              = model.IsMinusWord;
        AgreeVia                 = model.AgreeVia;
        LinkTree                 = model.LinkTree;
    }

#endregion

    /// <summary>
    ///     Thay thế từ khóa cấm
    /// </summary>
    public bool IsMinusWord { get; set; }

    /// <summary>
    ///     Danh sách file đính kèm
    /// </summary>
    public List<NewsContentAttackModel> NewsContentAttackModels { get; set; }

    /// <summary>
    ///     Cấu trúc bài viết
    /// </summary>
    public List<NewsContentContentModel> NewsContentContentModels { get; set; }

#region Methods

    /// <summary>
    ///     Kiểm tra valid
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        return true;
    }

#endregion
}