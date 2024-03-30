using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.MenuManager;

/// <summary>
///     từ thay thế
/// </summary>
public class MinusWord : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public MinusWord(Guid id, string root, string replace, string description, string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Update(root, replace, description, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected MinusWord()
    {
    }

    /// <summary>
    ///     Từ gốc
    /// </summary>
    public string Root { get; set; }

    /// <summary>
    ///     Từ thay thế
    /// </summary>
    public string Replace { get; set; }

    /// <summary>
    ///     Ghi chú
    /// </summary>
    public string Description { get; set; }

    public int Position { get; set; }

    public void Update(string root, string replace, string description, string createdBy = null)
    {
        Root        = root;
        Replace     = replace;
        Description = description;
        Update(createdBy);
    }
}