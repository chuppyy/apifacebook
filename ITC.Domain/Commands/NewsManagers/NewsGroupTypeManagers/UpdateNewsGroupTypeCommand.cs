using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsGroupTypeManagers;

/// <summary>
///     Command cập nhật loại nhóm tin
/// </summary>
public class UpdateNewsGroupTypeCommand : NewsGroupTypeCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateNewsGroupTypeCommand(NewsGroupTypeEventModel model)
    {
        Id          = model.Id;
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