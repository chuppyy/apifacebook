using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ITC.Domain.Core.ModelShare.HomeManager;

public class NewsMainModel
{
    public              string   Id            { get; set; }
    public              string   Name          { get; set; }
    public string Summary { get; set; }
    public string UserCode { get; set; }
    public              string   Content       { get; set; }
    public              string   AvatarLink    { get; set; }
    [JsonIgnore] public bool     AvatarLocal   { get; set; }
    public              DateTime DateTimeStart { get; set; }
}

public class NewsGroupMainModel : NewsMainModel
{
    public string GroupName { get; set; }
    public string Id        { get; set; }
}

public class NewsGroupMainEvent
{
    public List<Guid> Group  { get; set; }
    public int        Number { get; set; }
}

public class HomeMainGroupModel
{
    public string                            GroupName { get; set; }
    public IEnumerable<HomeMainContentModel> Detail    { get; set; }
}

public class HomeMainContentModel
{
    public string   Id            { get; set; }
    public string   Name          { get; set; }
    public string   AvatarLink    { get; set; }
    public DateTime DateTimeStart { get; set; }
}

public class NewsLifeMainEvent
{
    public List<int> Model { get; set; }
}

public class HomeNewsLifeModel
{
    public string TenBaiDang { get; set; }
    public string AnhDaiDien { get; set; }
    public string TenTheLoai { get; set; }
    public string IDPage     { get; set; }
    public string IdQC       { get; set; }
    public string Token      { get; set; }
}