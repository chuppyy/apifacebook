using System;
using System.Collections.Generic;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;

public class NewsDomainEventModel : PublishModal
{
    public Guid   Id        { get; set; }
    public string Name      { get; set; }
    public string DomainNew { get; set; }
    public string Description { get; set; }
}

public class NewsDomainSchedulerEvent : PublishModal
{
    public List<DomainVercel> Model { get; set; }
}

public class DomainVercel
{
    public string Name     { get; set; }
    public int    IdDomain { get; set; }
}