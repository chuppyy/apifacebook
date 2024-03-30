using System.Collections.Generic;
using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ITC.Domain.Commands.SystemManagers.ServerFileManagers;

/// <summary>
///     Command upload file
/// </summary>
public class UploadServerFile2023Command : ServerFileCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UploadServerFile2023Command(UploadFileEventModel model)
    {
        Name         = model.Name;
        Link         = model.Link;
        FileType     = model.FileType;
        IsLocal      = model.IsLocal;
        FileModels   = model.FileModels;
        Description  = model.Description;
        VideoType    = model.VideoType;
        ManagementId = model.ManagementId;
        ParentId     = model.ParentId;
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

    /// <summary>
    ///     Danh sách file
    /// </summary>
    [JsonIgnore]
    public List<IFormFile> FileModels { get; set; }

    /// <summary>
    ///     ManagementId
    /// </summary>
    public string ManagementId { get; set; }

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