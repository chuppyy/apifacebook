#region

using System.Globalization;

#endregion

namespace ITC.Service.API.Extensions;

/// <summary>
/// </summary>
public class StringExtention
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string IntToRoman(int number)
    {
        switch (number)
        {
            case 1:
                return "I";
            case 2:
                return "II";
            case 3:
                return "III";
            case 4:
                return "IV";
            case 5:
                return "V";
            case 6:
                return "VI";
            case 7:
                return "VII";
            case 8:
                return "VIII";
            case 9:
                return "IX";
            case 10:
                return "X";
            case 11:
                return "XI";
            case 12:
                return "XII";
            default:
                return string.Empty;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public static string MoneyToString(int value, bool unit = false)
    {
        var result       = value.ToString("N", CultureInfo.InvariantCulture);
        if (unit) result += " (VND)";
        return result;
    }

    /// <summary>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public static string MoneyToString(double value, bool unit = false)
    {
        var result       = value.ToString("N", CultureInfo.InvariantCulture);
        if (unit) result += " (VND)";
        return result;
    }

#endregion
}