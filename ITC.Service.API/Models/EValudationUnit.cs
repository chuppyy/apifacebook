#region

using ITC.Domain.Interfaces;
using ITC.Service.API.Extensions;
using Microsoft.AspNetCore.Http;

#endregion

namespace ITC.Service.API.Models;

/// <summary>
///     Đơn vị đánh giá
/// </summary>
public class SesstionValue
{
#region Constructors

#endregion

#region Properties

    /// <summary>
    ///     Mã cấp học
    /// </summary>
    public string EducationLevelCode { get; set; }

    /// <summary>
    ///     Id Năm học đang chọn
    /// </summary>
    public string SchoolYearId { get; set; }

    /// <summary>
    ///     Id đơn vị
    /// </summary>
    public string UnitId { get; set; }

    /// <summary>
    ///     Tên đơn vị đánh giá
    /// </summary>
    public string UnitName { get; set; }

    /// <summary>
    ///     Id đăng nhập của đơn vị
    /// </summary>
    public string UserId { get; set; }

#endregion

#region Methods

    /// <summary>
    ///     Lấy session
    /// </summary>
    /// <param name="user"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public static SesstionValue GetValue(ISession session, IUser user)
    {
        var value                = session.GetObjectFromJson<SesstionValue>(SessionKey.EValuationUnit);
        if (value == null) value = new SesstionValue { SchoolYearId = user.SchoolYear };
        return value;
    }


    /// <summary>
    ///     Set session
    /// </summary>
    /// <param name="session"></param>
    /// <param name="value"></param>
    public static void SetValue(ISession session, SesstionValue value)
    {
        session.SetObjectAsJson(SessionKey.EValuationUnit, value);
    }

#endregion
}