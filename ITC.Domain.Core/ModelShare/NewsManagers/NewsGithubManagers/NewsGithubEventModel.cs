using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsGithubManagers;

public class NewsGithubEventModel : PublishModal
{
    public Guid   Id          { get; set; }
    public string Code        { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }
}