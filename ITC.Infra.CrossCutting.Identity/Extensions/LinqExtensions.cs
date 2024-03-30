#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Extensions;

public static class LinqExtensions
{
#region Methods

    public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource>    first, IEnumerable<TSource> second,
                                                       Func<TSource, TSource, bool> comparer)
    {
        return first.Where(x => second.Count(y => comparer(x, y)) == 0);
    }

    public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource>    first,
                                                          IEnumerable<TSource>         second,
                                                          Func<TSource, TSource, bool> comparer)
    {
        return first.Where(x => second.Count(y => comparer(x, y)) == 1);
    }

#endregion
}