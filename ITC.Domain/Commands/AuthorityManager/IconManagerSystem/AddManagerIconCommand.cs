namespace ITC.Domain.Commands.AuthorityManager.IconManagerSystem;

/// <summary>
///     Command thêm icon
/// </summary>
public class AddManagerIconCommand : ManagerIconCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public AddManagerIconCommand(string name)
    {
        Name = name;
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