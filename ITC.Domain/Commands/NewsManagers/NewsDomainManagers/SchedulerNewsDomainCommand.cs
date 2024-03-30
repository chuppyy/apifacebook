using System.Collections.Generic;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsDomainManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsDomainManagers;

/// <summary>
///     Command thêm loại nhóm tin
/// </summary>
public class SchedulerNewsDomainCommand : NewsDomainCommand
{
#region Constructors

/// <summary>
///     Hàm dựng với tham số
/// </summary>
/// <param name="model"></param>
public SchedulerNewsDomainCommand(NewsDomainSchedulerEvent model)
{
    DomainVercels = model.Model;
}

#endregion

public List<DomainVercel> DomainVercels { get; set; }

#region Methods

    /// <summary>
    ///     Kiểm tra valid
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        return true;
    }

#endregion
}