namespace ITC.Domain.Commands.AuthorityManager.IconManagerSystem;

/// <summary>
///     Command cập nhật icon
/// </summary>
public class UpdateManagerIconCommand : ManagerIconCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateManagerIconCommand(int id, string name)
    {
        Id   = id;
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