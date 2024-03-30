using ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsDomainManagers;

/// <summary>
///     Command cập nhật loại nhóm tin
/// </summary>
public class UpdateNewsDomainCommand : NewsDomainCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateNewsDomainCommand(NewsDomainEventModel model)
    {
        Id          = model.Id;
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