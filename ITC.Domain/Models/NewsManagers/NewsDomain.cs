using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.NewsManagers;

/// <summary>
///     NewsDomain
/// </summary>
public class NewsDomain : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    public NewsDomain()
    {
        
    }
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public NewsDomain(Guid id, string name, string domainNew, string description, string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Update(name, domainNew, description, createdBy);
    }
    public NewsDomain(string name, string domainNew, string description, string createdBy = null)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Update(name, domainNew, description, createdBy);
    }

    public string Name        { get; set; }
    public string DomainNew   { get; set; }
    public string Description { get; set; }

    public void Update(string name, string domainNew, string description, string createdBy = null)
    {
        Name        = name;
        DomainNew   = domainNew;
        Description = description;
        Update(createdBy);
    }
}