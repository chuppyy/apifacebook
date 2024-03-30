using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

namespace ITC.Domain.Commands.SystemManagers.ServerFileManagers;

/// <summary>
///     Command cập nhật tên file
/// </summary>
public class UpdateFileNameServerFileCommand : ServerFileCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateFileNameServerFileCommand(UpdateFileNameModal model)
    {
        FileName = model.Name;
        Id       = model.Id;
        ParentId = model.ParentId;
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