using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

namespace ITC.Domain.Commands.SystemManagers.ServerFileManagers;

/// <summary>
///     Command cập nhật thư mục
/// </summary>
public class UpdateFolderServerFileCommand : ServerFileCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public UpdateFolderServerFileCommand(FolderServerFileEvent model)
    {
        Id           = model.Id;
        FileName     = model.Name;
        FileNameRoot = model.Name;
        Folder       = model.Name;
        Description  = model.Description;
        ParentId     = model.ParentId;
        IsFolder     = true;
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