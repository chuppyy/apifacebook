using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.NewsManagers;

/// <summary>
///     NewsGithub
/// </summary>
public class NewsGithub : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public NewsGithub(Guid id, string code, string name, string description, int position, string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Position = position;
        Update(code, name, description, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NewsGithub()
    {
    }

    public string Code        { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    public void Update(string code, string name, string description, string createdBy = null)
    {
        Code        = code;
        Name        = name;
        Description = description;
        Update(createdBy);
    }
}