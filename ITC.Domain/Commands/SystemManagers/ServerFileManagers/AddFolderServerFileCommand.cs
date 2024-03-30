using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

namespace ITC.Domain.Commands.SystemManagers.ServerFileManagers;

/// <summary>
///     Command thêm thư mục
/// </summary>
public class AddFolderServerFileCommand : ServerFileCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public AddFolderServerFileCommand(FolderServerFileEvent model)
    {
        FileName     = model.Name;
        FileNameRoot = model.Name;
        Folder       = model.Name;
        Description  = model.Description;
        IsFolder     = true;
        ParentId     = model.ParentId;
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