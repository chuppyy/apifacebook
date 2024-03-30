using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

/// <summary>
///     [Model] Upload File
/// </summary>
public class UploadFileEventModel : PublishModal
{
    public Guid Id { get; set; }

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
    ///     Kiểu video <see cref="VideoTypeEnum" />
    /// </summary>
    public int VideoType { get; set; }

    /// <summary>
    ///     Là file từ máy tính
    /// </summary>
    public bool IsLocal { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Mã quản lý
    /// </summary>
    public string ManagementId { get; set; }

    /// <summary>
    ///     Mã quản lý
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    ///     Nhóm dữ liệu file
    ///     [Dùng cho các trường hợp dữ liệu file nhiều cho từng người]
    /// </summary>
    public int GroupFile { get; set; }

    /// <summary>
    ///     Lưu dữ liệu vào bảng SubmitDocument
    /// </summary>
    public int IsSaveSubmitDocument { get; set; }

    /// <summary>
    ///     Lưu dữ liệu có sử dụng ManagementId
    /// </summary>
    public int IsSaveWithManagement { get; set; }

    /// <summary>
    ///     Danh sách file
    /// </summary>
    [JsonIgnore]
    public List<IFormFile> FileModels { get; set; }

    /// <summary>
    ///     Danh sách ID AttackFile
    /// </summary>
    [JsonIgnore]
    public List<Guid> AttackFileIdModels { get; set; }
}

/// <summary>
///     [Model] Update FileName
/// </summary>
public class UpdateFileNameModal : PublishModal
{
    /// <summary>
    ///     Mã file
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên file
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mã cha - con
    /// </summary>
    public string ParentId { get; set; }
}

/// <summary>
///     [Model] Resize-Image
/// </summary>
public class ResizeImageModal : PublishModal
{
    /// <summary>
    ///     Danh sách ID dữ liệu
    /// </summary>
    [FromQuery(Name = "listModels")]
    public List<Guid> ListModels { get; set; }
}