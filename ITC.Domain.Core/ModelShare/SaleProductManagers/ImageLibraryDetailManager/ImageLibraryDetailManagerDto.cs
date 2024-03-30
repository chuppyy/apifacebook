using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NCore.Actions;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.SaleProductManagers.ImageLibraryDetailManager;

/// <summary>
///     Modal phân trang slide
/// </summary>
public class ImageLibraryDetailManagerPagingDto
{
    public              Guid                       Id          { get; set; }
    public              string                     Name        { get; set; }
    public              string                     Content     { get; set; }
    public              string                     OwnerId     { get; set; }
    [JsonIgnore] public int                        StatusId    { get; set; }
    public              string                     StatusName  => ActionStatusEnum.GetById(StatusId)?.Name;
    public              string                     StatusColor => ActionStatusColorEnum.GetById(StatusId)?.Name;
    public              bool                       IsLocal     { get; set; }
    public              string                     FilePath    { get; set; }
    public              string                     LinkUrl     { get; set; }
    public              List<ActionAuthorityModel> Actions     { get; set; }
    [JsonIgnore] public int                        TotalRecord { get; set; }
}

public class ImageLibraryDetailPagingModel : PagingModel
{
    public Guid ImageId { get; set; }
}