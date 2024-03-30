using System;

namespace ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

/// <summary>
///     Thông tin chi tiết File
/// </summary>
public class ServerFileDetailInfo
{
    public Guid   Id         { get; set; }
    public string Name       { get; set; }
    public string ParentId   { get; set; }
    public string LinkUrl    { get; set; }
    public string FilePath   { get; set; }
    public bool   IsLocal    { get; set; }
    public string FolderName { get; set; }
}

/// <summary>
///     Model gửi dữ liệu lưu ServerFile bằng dapper
/// </summary>
public class ServerFileSendSave
{
    /// <summary>
    ///     Mã File
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Đuôi mở rộng
    /// </summary>
    public string FileExtension { get; set; }

    /// <summary>
    ///     Tên file
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    ///     Tên file gốc
    /// </summary>
    public string FileNameRoot { get; set; }

    /// <summary>
    ///     Đường dẫn
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    ///     Dung lượng file
    /// </summary>
    public double FileSize { get; set; }

    /// <summary>
    ///     Kiểu file <see cref="FileTypeEnum" />
    /// </summary>
    public int FileType { get; set; }

    /// <summary>
    ///     Tên thư mục
    /// </summary>
    public string Folder { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Mã thư mục cha - con
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public Guid? ManagementId { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Mã người dùng
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     Trạng thái
    /// </summary>
    public int StatusId { get; set; }

    /// <summary>
    ///     Nhóm dữ liệu file
    ///     [Dùng cho các trường hợp dữ liệu file nhiều cho từng người]
    /// </summary>
    public int GroupFile { get; set; }
}