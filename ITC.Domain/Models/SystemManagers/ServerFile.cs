using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SystemManagers;

/// <summary>
///     ServerFile
/// </summary>
public class ServerFile : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    public ServerFile()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fileExtension"></param>
    /// <param name="fileName"></param>
    /// <param name="fileNameRoot"></param>
    /// <param name="filePath"></param>
    /// <param name="fileSize"></param>
    /// <param name="fileType"></param>
    /// <param name="folder"></param>
    /// <param name="description"></param>
    /// <param name="parentId"></param>
    /// <param name="managementId"></param>
    /// <param name="linkUrl"></param>
    /// <param name="videoType"></param>
    /// <param name="position"></param>
    /// <param name="createBy"></param>
    public ServerFile(Guid   id,          string fileExtension, string fileName,     string fileNameRoot,
                      string filePath,    double fileSize,      int    fileType,     string folder,
                      string description, string parentId,      Guid   managementId, string linkUrl,
                      int    videoType,   int    position,      string createBy = null) : base(id, createBy)
    {
        FileExtension = fileExtension;
        FileName      = fileName;
        FileNameRoot  = fileNameRoot;
        FilePath      = filePath;
        FileSize      = fileSize;
        FileType      = fileType;
        Folder        = folder;
        Description   = description;
        IsFolder      = false;
        ParentId      = parentId;
        PLeft         = 1;
        PRight        = 2;
        CloudId       = "";
        CloudFolder   = "";
        ManagementId  = managementId;
        AvatarLink    = "";
        LinkUrl       = linkUrl;
        VideoType     = videoType;
        Position      = position;
        IsRoot        = false;
        IsLocal       = false;
        StatusId      = ActionStatusEnum.Active.Id;
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="folder"></param>
    /// <param name="description"></param>
    /// <param name="parentId"></param>
    /// <param name="position"></param>
    /// <param name="createBy"></param>
    public ServerFile(Guid   id, string name, string folder, string description, string parentId, int position,
                      string createBy = null) : base(id, createBy)
    {
        FileExtension = "";
        FileName      = name;
        FileNameRoot  = name;
        FilePath      = "";
        FileSize      = 0;
        FileType      = 0;
        Folder        = folder;
        Description   = description;
        IsFolder      = true;
        ParentId      = parentId;
        PLeft         = 0;
        PRight        = 0;
        CloudId       = "";
        CloudFolder   = "";
        ManagementId  = Guid.Empty;
        AvatarLink    = "";
        LinkUrl       = "";
        VideoType     = 0;
        Position      = position;
        IsRoot        = false;
        StatusId      = ActionStatusEnum.Active.Id;
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="parentId"></param>
    /// <param name="position"></param>
    /// <param name="linkUrl"></param>
    /// <param name="fileExtension"></param>
    /// <param name="fileType"></param>
    /// <param name="videoType"></param>
    /// <param name="createBy"></param>
    public ServerFile(Guid   id, string name, string description, string parentId, int position, string linkUrl,
                      string fileExtension, int fileType, int videoType, string createBy = null) : base(id, createBy)
    {
        FileExtension = fileExtension;
        FileName      = name;
        FileNameRoot  = name;
        FilePath      = "";
        FileSize      = 0;
        FileType      = fileType;
        Folder        = "";
        Description   = description;
        IsFolder      = false;
        ParentId      = parentId;
        PLeft         = 0;
        PRight        = 0;
        CloudId       = "";
        CloudFolder   = "";
        ManagementId  = Guid.Empty;
        AvatarLink    = "";
        LinkUrl       = linkUrl;
        VideoType     = videoType;
        Position      = position;
        IsRoot        = false;
        StatusId      = ActionStatusEnum.Active.Id;
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    /// <param name="id"></param>
    /// <param name="avatarLink"></param>
    /// <param name="position"></param>
    /// <param name="createBy"></param>
    public ServerFile(Guid id, string avatarLink, int position, string createBy = null) : base(id, createBy)
    {
        IsRoot     = false;
        AvatarLink = avatarLink;
        Position   = position;
        //Update("", "", "", 0, 0, "", "", "", 0, 0, false, "", "", Guid.Empty, "", avatarLink, position, createBy);
    }

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
    ///     Là file từ máy tính
    /// </summary>
    public bool IsLocal { get; set; }

    /// <summary>
    ///     Đường dẫn dữ liệu
    /// </summary>
    public string LinkUrl { get; set; }

    /// <summary>
    ///     Mã thư mục cha - con
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    ///     Giá trị trái
    /// </summary>
    public int PLeft { get; set; }

    /// <summary>
    ///     Giá trị phải
    /// </summary>
    public int PRight { get; set; }

    /// <summary>
    ///     Mã cloud
    /// </summary>
    public string CloudId { get; set; }

    /// <summary>
    ///     Tên thư mục cloud
    /// </summary>
    public string CloudFolder { get; set; }

    /// <summary>
    ///     Mã đơn vị
    /// </summary>
    public Guid? ManagementId { get; set; }

    /// <summary>
    ///     Đường dẫn base64 cho ảnh đại diện
    /// </summary>
    public string AvatarLink { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Là thư mục gốc
    /// </summary>
    public bool IsRoot { get; set; }

    /// <summary>
    ///     Nhóm dữ liệu file
    ///     [Dùng cho các trường hợp dữ liệu file nhiều cho từng người]
    /// </summary>
    public int GroupFile { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    public void Update(string fileName,   string fileExtension, string filePath, double fileSize, int fileType,
                       string folder,     string description, string idParentId,
                       bool   isFolder,   string cloudId, string cloudFolder, Guid managementId, string fileNameRoot,
                       string avatarLink, int    position, string modifiedBy = null)
    {
        FileName      = fileName;
        FileExtension = fileExtension;
        FilePath      = filePath;
        FileSize      = fileSize;
        FileType      = fileType;
        Folder        = folder;
        Description   = description;
        IsFolder      = isFolder;
        ParentId      = idParentId;
        PLeft         = 0;
        PRight        = 0;
        CloudId       = cloudId;
        CloudFolder   = cloudFolder;
        ManagementId  = managementId;
        FileNameRoot  = fileNameRoot;
        AvatarLink    = avatarLink;
        Position      = position;
        Update(modifiedBy);
    }
}