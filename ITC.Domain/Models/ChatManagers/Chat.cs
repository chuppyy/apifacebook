using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.ChatManagers;

/// <summary>
///     Danh mục
/// </summary>
public class Chat : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public Chat(Guid   id, Guid sender, Guid receiveder, string content, Guid projectId, int portalId,
                string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        UpdateConstruct(sender, receiveder, content, projectId, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected Chat()
    {
    }

    public Guid     Sender       { get; set; }
    public Guid     Receiveder   { get; set; }
    public string   Content      { get; set; }
    public Guid     ProjectId    { get; set; }
    public DateTime SendDateTime { get; set; }

    public void UpdateConstruct(Guid   sender, Guid receiveder, string content, Guid projectId,
                                string modifiedBy = null)
    {
        Sender       = sender;
        Receiveder   = receiveder;
        Content      = content;
        ProjectId    = projectId;
        SendDateTime = DateTime.Now;
        Update(modifiedBy);
    }
}