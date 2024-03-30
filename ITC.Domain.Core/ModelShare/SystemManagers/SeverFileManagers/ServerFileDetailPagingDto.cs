using System;
using System.Text.Json.Serialization;

namespace ITC.Domain.Core.ModelShare.SystemManagers.SeverFileManagers;

/// <summary>
///     Danh sách file chi tiết
/// </summary>
public class ServerFileDetailPagingDto
{
    public Guid Id { get; set; }

    // public              string   FileName     { get; set; }
    public string Checked  { get; set; }
    public string FilePath { get; set; }
    public string LinkUrl  { get; set; }

    public string Link { get; set; }

    // public              DateTime Modified     { get; set; }
    // public              string   Author       { get; set; }
    // public              double   FileSize     { get; set; }
    // public              int      FileType     { get; set; }
    // public              int      VideoType    { get; set; }
    public bool IsFolder { get; set; }

    public bool IsLocal { get; set; }
    // public              int      StatusId     { get; set; }
    // public              string   FileTypeName => FileTypeEnumeration.GetById(FileType)?.Name;
    // public              string   StatusName   => ActionStatusEnum.GetById(StatusId)?.Name;
    // public              string   StatusColor  => ActionStatusColorEnum.GetById(StatusId)?.Name;
    [JsonIgnore] public int TotalRecord { get; set; }
}