#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

#endregion

namespace ITC.Service.API.Extensions;

/// <summary>
/// </summary>
public static class EnumExtensions
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="enu"></param>
    /// <returns></returns>
    public static string GetDescription(this Enum enu)
    {
        var attr = GetDisplayAttribute(enu);
        return attr != null ? attr.Description : enu.ToString();
    }

    /// <summary>
    /// </summary>
    /// <param name="enu"></param>
    /// <returns></returns>
    public static string GetDisplayName(this Enum enu)
    {
        var attr = GetDisplayAttribute(enu);
        return attr != null ? attr.Name : enu.ToString();
    }

    private static DisplayAttribute GetDisplayAttribute(object value)
    {
        var type = value.GetType();
        if (!type.IsEnum) throw new ArgumentException(string.Format("Type {0} is not an enum", type));

        // Get the enum field.
        // var field = type.GetField(value.ToString());
        var field = type.GetField(value.ToString() ?? string.Empty);
        return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
    }

#endregion
}