using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.NewsManagers;

/// <summary>
///     Via
/// </summary>
public class NewsVia : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public NewsVia(Guid id, string code, string content, string token, string idTkQc, Guid staffId, int position, string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Position = position;
        Update(code, content, token, idTkQc, staffId, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NewsVia()
    {
    }

    public string Code    { get; set; }
    public string Content { get; set; }
    public string Token   { get; set; }
    public string IdTkQc  { get; set; }
    public Guid   StaffId { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    public void Update(string code, string content, string token, string idTkQc, Guid staffId, string createdBy = null)
    {
        Code    = code;
        Content = content;
        Token   = token;
        IdTkQc  = idTkQc;
        StaffId = staffId;
        Update(createdBy);
    }
}