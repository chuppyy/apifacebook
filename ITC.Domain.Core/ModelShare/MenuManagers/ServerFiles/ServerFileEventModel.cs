using System;
using Microsoft.AspNetCore.Http;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.MenuManagers.ServerFiles;

public class ServerFileEventModel : PublishModal
{
    public Guid      Id         { get; set; }
    public IFormFile Files      { get; set; }
    public string    FilePath   { get; set; }
    public string    FileExtend { get; set; }
}

/// <summary>
///     Class gửi dữ liệu serverFile về FE
/// </summary>
public class ServerFileUploadSendFeModel
{
    public Guid   Id         { get; set; }
    public string FilePath   { get; set; }
    public string FileExtend { get; set; }
}