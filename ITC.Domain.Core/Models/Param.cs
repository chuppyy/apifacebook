namespace ITC.Domain.Core.Models;

public class Param
{
#region Static Fields and Constants

    private const int MaxPageSize = int.MaxValue;

#endregion

#region Fields

    private int _pageNumber;
    private int _pageSize = int.MaxValue;

#endregion

#region Properties

    public bool IsSchool { get; set; }

    public string ManagementId { get; set; }

    public int PageNumber
    {
        get
        {
            if (_pageNumber == 0) _pageNumber = 1;
            return _pageNumber;
        }
        set => _pageNumber = value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < MaxPageSize ? value : MaxPageSize;
    }

    public string ParentManagementId { get; set; }
    public string SchoolYearId       { get; set; }
    public string UnitId             { get; set; }

#endregion
}