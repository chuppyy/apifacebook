﻿#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

#endregion

namespace ITC.Domain.Extensions;

public static class EnumerableExtensions
{
#region Methods

    /// <summary>
    ///     Provides easier linear traversing over all items in collection and executing a function on each of them.
    /// </summary>
    /// <typeparam name="T">Type of objects in the collection.</typeparam>
    /// <param name="collection">Collection to traverse.</param>
    /// <param name="action">Function to execute on each item in the collection.</param>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (var item in collection) action(item);

        return collection;
    }

    public static string GetDescription(this Enum enu)
    {
        var attr = GetDisplayAttribute(enu);
        return attr != null ? attr.Description : enu.ToString();
    }


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
        var field = type.GetField(value.ToString());
        return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
    }

#endregion
}