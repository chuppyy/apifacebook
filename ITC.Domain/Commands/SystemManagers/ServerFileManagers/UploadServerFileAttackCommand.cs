using System;
using System.Collections.Generic;
using ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ITC.Domain.Commands.SystemManagers.ServerFileManagers;

/// <summary>
///     Command upload file attack
/// </summary>
public class UploadServerFileAttackCommand : ServerFileCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UploadServerFileAttackCommand(UploadFileEventModel model)
    {
        Name                 = model.Name;
        Link                 = model.Link;
        FileType             = model.FileType;
        IsLocal              = model.IsLocal;
        FileModels           = model.FileModels;
        Description          = model.Description;
        VideoType            = model.VideoType;
        ManagementId         = model.ManagementId;
        ParentId             = model.ParentId;
        GroupFile            = model.GroupFile;
        IsSaveSubmitDocument = model.IsSaveSubmitDocument;
        IsSaveWithManagement = model.IsSaveWithManagement;
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

    /// <summary>
    ///     Nhóm dữ liệu file
    ///     [Dùng cho các trường hợp dữ liệu file nhiều cho từng người]
    /// </summary>
    public int GroupFile { get; set; }

    /// <summary>
    ///     Danh sách ID AttackFile
    /// </summary>
    [JsonIgnore]
    public List<Guid> AttackFileIdModels { get; set; }

    /// <summary>
    ///     Lưu dữ liệu vào bảng SubmitDocument
    /// </summary>
    public int IsSaveSubmitDocument { get; set; }

    /// <summary>
    ///     Lưu dữ liệu có sử dụng ManagementId
    /// </summary>
    public int IsSaveWithManagement { get; set; }

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