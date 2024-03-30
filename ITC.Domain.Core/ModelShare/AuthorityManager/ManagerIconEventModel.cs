using System.Text.Json.Serialization;

namespace ITC.Domain.Core.ModelShare.AuthorityManager;

/// <summary>
///     Class truyền dữ liệu icon
/// </summary>
public class ManagerIconEventModel
{
    public int Id { get; set; }

    /// <summary>
    ///     Tên
    /// </summary>
    public string Name { get; set; }
}

public class ManagerIconPagingModel
{
    public              int    Id          { get; set; }
    public              string Name        { get; set; }
    [JsonIgnore] public int    TotalRecord { get; set; }
}