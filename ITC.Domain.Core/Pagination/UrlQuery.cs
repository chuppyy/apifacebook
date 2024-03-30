#region

using System.Collections.Generic;

#endregion

namespace ITC.Domain.Core.Pagination;

public class UrlQuery
{
#region Static Fields and Constants

    private const int MaxPageSize = int.MaxValue;

#endregion

#region Fields

    private int _pageSize   = int.MaxValue;
    private int _pageNumber = 1;

#endregion

#region Properties

    public string Keyword { get; set; }

    public string ManagementId { get; set; }

    public int? PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value ?? 1;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < MaxPageSize ? value : MaxPageSize;
    }

    public int    PortalId     { get; set; }
    public string RoleIdentity { get; set; }
    public string SchoolYearId { get; set; }
    public string UnitId       { get; set; }

#endregion
}

public class UrlQuerySeftAssessment
{
#region Static Fields and Constants

    private const int maxPageSize = 100;

#endregion

#region Fields

    private int _pageSize  = 50;
    private int pageNumber = 1;

#endregion

#region Properties

    public string Keyword { get; set; }

    public int? PageNumber
    {
        get => pageNumber;
        set => pageNumber = value ?? 1;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < maxPageSize ? value : maxPageSize;
    }

    public int PortalId { get; set; }

    public string SchoolYearId { get; set; }

    public string SelfAssessmentManagementId { get; set; }

#endregion
}

public sealed class Pagination<T>
{
#region Properties

    public int     CurrentPage { get; set; }
    public List<T> PageLists   { get; set; }
    public int     TotalPage   { get; set; }
    public int     TotalRecord { get; set; }
    public bool    IsAddNew    { get; set; }

#endregion
}

public class PageData
{
#region Properties

    public List<int> PageSizes { get; set; } = new() { 10, 20, 50, 100 };

#endregion
}