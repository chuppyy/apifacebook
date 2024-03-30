using ITC.Domain.Core.ModelShare.NewsManagers.NewsSeoKeyWordManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsSeoKeyWordManagers;

/// <summary>
///     Command thêm từ khóa seo
/// </summary>
public class AddNewsSeoKeyWordCommand : NewsSeoKeyWordCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AddNewsSeoKeyWordCommand(NewsSeoKeyWordEventModel model)
    {
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