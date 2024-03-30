using System;
using Newtonsoft.Json;

namespace ITC.Domain.Core.ModelShare.Itphonui.ManagementManagers;

/// <summary>
///     Trả về danh sách đơn vị trong project dưới dạng combobox
/// </summary>
public class ManagementViewProjectModel
{
    public              Guid   Id          { get; set; }
    public              string Name        { get; set; }
    public              string Checked     { get; set; }
    [JsonIgnore] public int    TotalRecord { get; set; }
}