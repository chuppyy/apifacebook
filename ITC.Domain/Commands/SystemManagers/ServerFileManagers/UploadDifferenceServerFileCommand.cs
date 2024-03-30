using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

namespace ITC.Domain.Commands.SystemManagers.ServerFileManagers;

/// <summary>
///     Command upload file t link khác
/// </summary>
public class UploadDifferenceServerFileCommand : ServerFileCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UploadDifferenceServerFileCommand(UploadDifferenceEventModal model)
    {
        Name        = model.Name;
        Link        = model.Link;
        FileType    = model.FileType;
        IsLocal     = false;
        Description = model.Description;
        VideoType   = model.VideoType;
        ParentId    = model.ParentId;
    }

#endregion

    /// <summary>
    ///     Tên file
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Đường dẫn file
    /// </summary>
    public string Link { get; set; }

    /// <summary>
    ///     Loại file <see cref="FileTypeEnum" />
    /// </summary>
    public int FileType { get; set; }

    /// <summary>
    ///     Là file từ máy tính
    /// </summary>
    public bool IsLocal { get; set; }

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