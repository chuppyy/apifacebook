#region

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

#endregion

namespace ITC.Service.API.Extensions;

/// <summary>
/// </summary>
public static class SessionExtension
{
#region Methods

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="session"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="session"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetObjectAsJson<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

#endregion
}

/// <summary>
/// </summary>
public class SessionKey
{
#region Static Fields and Constants

    /// <summary>
    /// </summary>
    public const string Demo = "Demo";

    /// <summary>
    /// </summary>
    public const string EValuationUnit = "EValuationUnit";

#endregion
}