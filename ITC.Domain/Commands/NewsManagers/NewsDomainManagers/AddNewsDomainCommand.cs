using ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsDomainManagers;

/// <summary>
///     Command thêm loại nhóm tin
/// </summary>
public class AddNewsDomainCommand : NewsDomainCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AddNewsDomainCommand(NewsDomainEventModel model)
    {
        Name        = model.Name;
        DomainNew   = model.DomainNew;
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