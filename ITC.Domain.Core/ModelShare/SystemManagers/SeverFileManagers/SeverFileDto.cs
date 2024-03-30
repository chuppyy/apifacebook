using System;
using NCore.Enums;

namespace ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

public class ServerFilePagingDto
{
    /// <summary>
    ///     Mã File
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Tên file
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    ///     Tên file gốc
    /// </summary>
    public string FileNameRoot { get; set; }

    /// <summary>
    ///     Là file từ máy tính
    /// </summary>
    public bool IsLocal { get; set; }

    /// <summary>
    ///     Đường dẫn file
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    ///     Đuôi mở rộng file
    /// </summary>
    public string FileExtension { get; set; }

    /// <summary>
    ///     Nhóm dữ liệu file
    ///     [Dùng cho các trường hợp dữ liệu file nhiều cho từng người]
    /// </summary>
    public int GroupFile { get; set; }

    /// <summary>
    ///     Loại file
    /// </summary>
    public int FileType { get; set; }

    /// <summary>
    ///     Loại video
    /// </summary>
    public int VideoType { get; set; }

    /// <summary>
    ///     Tên loại file
    /// </summary>
    public string FileTypeName => FileTypeEnumeration.GetById(FileType)?.Name;

    /// <summary>
    ///     Tên loại video
    /// </summary>
    public string VideoTypeName => VideoTypeEnumeration.GetById(VideoType)?.Name;
}