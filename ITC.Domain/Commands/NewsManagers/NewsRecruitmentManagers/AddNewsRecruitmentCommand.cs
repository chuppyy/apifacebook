using ITC.Domain.Core.ModelShare.NewsManagers.NewsRecruitmentManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsRecruitmentManagers;

/// <summary>
///     Command thêm bài viết
/// </summary>
public class AddNewsRecruitmentCommand : NewsRecruitmentCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AddNewsRecruitmentCommand(NewsRecruitmentEventModel model)
    {
        Name          = model.Name;
        Summary       = model.Summary;
        Content       = model.Content;
        SeoKeyword    = model.SeoKeyword;
        AvatarId      = model.AvatarId;
        DateTimeStart = model.DateTimeStart;
        Type          = model.Type;
        StatusId      = model.StatusId;
    }

#endregion

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