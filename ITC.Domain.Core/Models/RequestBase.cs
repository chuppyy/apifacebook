#region

using System.Text.Json.Serialization;

#endregion

namespace ITC.Domain.Core.Models;

public class RequestBase
{
#region Properties

    [JsonIgnore] public string ManagementId     { get; set; }
    [JsonIgnore] public string ManagementUnitId { get; set; }
    [JsonIgnore] public string UserId           { get; set; }

#endregion
}