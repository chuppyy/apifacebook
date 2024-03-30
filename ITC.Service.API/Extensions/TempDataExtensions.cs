#region

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

#endregion

namespace ITC.Service.API.Extensions;

/// <summary>
/// </summary>
public static class TempDataExtensions
{
#region Methods

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tempData"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
    {
        object o;
        tempData.TryGetValue(key, out o);
        return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
    }

    /// <summary>
    /// </summary>
    /// <param name="tempData"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static object GetJson(this ITempDataDictionary tempData, string key)
    {
        object o;
        tempData.TryGetValue(key, out o);
        return o;
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tempData"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
    {
        tempData[key] = JsonConvert.SerializeObject(value);
    }

#endregion
}