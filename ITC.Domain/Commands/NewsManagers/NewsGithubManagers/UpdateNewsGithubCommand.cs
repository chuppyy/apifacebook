using ITC.Domain.Core.ModelShare.NewsManagers.NewsGithubManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsGithubManagers;

/// <summary>
///     Command cập nhật loại nhóm tin
/// </summary>
public class UpdateNewsGithubCommand : NewsGithubCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateNewsGithubCommand(NewsGithubEventModel model)
    {
        Id          = model.Id;
        Name        = model.Name;
        Code        = model.Code;
        Description = model.Description;
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