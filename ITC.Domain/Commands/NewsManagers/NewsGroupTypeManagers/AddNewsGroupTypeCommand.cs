using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsGroupTypeManagers;

/// <summary>
///     Command thêm loại nhóm tin
/// </summary>
public class AddNewsGroupTypeCommand : NewsGroupTypeCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AddNewsGroupTypeCommand(NewsGroupTypeEventModel model)
    {
        Name        = model.Name;
        Description = model.Description;
        CreateBy    = model.CreatedBy;
        MetaTitle   = model.MetaTitle;
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