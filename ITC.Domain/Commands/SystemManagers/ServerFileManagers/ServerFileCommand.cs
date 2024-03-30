#region

using System;
using ITC.Domain.Core.Commands;

#endregion

namespace ITC.Domain.Commands.SystemManagers.ServerFileManagers;

/// <summary>
///     Command ServerFileCommand
/// </summary>
public abstract class ServerFileCommand : Command
{
    /// <summary>
    ///     Mã Id
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

    // /// <summary>
    // ///     Kiểu file 
    // /// </summary>
    // public int FileType { get; set; }

    /// <summary>
    ///     Kiểu video <see cref="VideoTypeEnum" />
    /// </summary>
    public int VideoType { get; set; }

    /// <summary>
    ///     Tên thư mục
    /// </summary>
    public string Folder { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Là thư mục
    /// </summary>
    public bool IsFolder { get; set; }

    /// <summary>
    ///     Mã cha - con
    /// </summary>
    public string ParentId { get; set; }
}