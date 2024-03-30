namespace ITC.Service.API.Helpers;

/// <summary>
/// </summary>
public static class EquipmentHelper
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    public static int MaxDayInMonth(int year, int month)
    {
        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 ||
            month == 12) return 31;

        if (month == 2)
        {
            if (year % 100 == 0)
            {
                if (year / 100 % 4 == 0)
                    return 29;
                return 28;
            }

            return year % 4 == 0 ? 29 : 28;
        }

        return 30;
    }

#endregion
}