using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsSeoKeyWordManagers;

public class NewsSeoKeyWordEventModel : PublishModal
{
    public Guid   Id          { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }
}