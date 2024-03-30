using ITC.Domain.Core.ModelShare.NewsManagers.NewsRecruitmentManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsRecruitmentManagers;

/// <summary>
///     Command cập nhật bài viết
/// </summary>
public class UpdateNewsRecruitmentCommand : NewsRecruitmentCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateNewsRecruitmentCommand(NewsRecruitmentEventModel model)
    {
        Id            = model.Id;
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