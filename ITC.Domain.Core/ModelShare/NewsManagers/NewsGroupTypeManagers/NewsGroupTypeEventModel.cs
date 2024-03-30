using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupTypeManagers;

public class NewsGroupTypeEventModel : PublishModal
{
    public Guid   Id          { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }
    public string MetaTitle   { get; set; }
}