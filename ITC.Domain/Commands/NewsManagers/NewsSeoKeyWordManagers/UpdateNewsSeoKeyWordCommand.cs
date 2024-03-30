using ITC.Domain.Core.ModelShare.NewsManagers.NewsSeoKeyWordManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsSeoKeyWordManagers;

/// <summary>
///     Command cập nhật từ khóa SEO
/// </summary>
public class UpdateNewsSeoKeyWordCommand : NewsSeoKeyWordCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateNewsSeoKeyWordCommand(NewsSeoKeyWordEventModel model)
    {
        Id          = model.Id;
        Name        = model.Name;
        Description = model.Description;
        CreateBy    = model.CreatedBy;
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