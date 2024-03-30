using ITC.Domain.Core.ModelShare.NewsManagers.NewsGithubManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsGithubManagers;

/// <summary>
///     Command thêm loại nhóm tin
/// </summary>
public class AddNewsGithubCommand : NewsGithubCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AddNewsGithubCommand(NewsGithubEventModel model)
    {
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