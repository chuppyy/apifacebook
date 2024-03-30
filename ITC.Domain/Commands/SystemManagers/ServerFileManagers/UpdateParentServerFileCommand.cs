using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

namespace ITC.Domain.Commands.SystemManagers.ServerFileManagers;

/// <summary>
///     Command cập nhật mã cha - con
/// </summary>
public class UpdateParentServerFileCommand : ServerFileCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public UpdateParentServerFileCommand(FolderServerFileEvent model)
    {
        Id       = model.Id;
        FileName = model.Name;
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