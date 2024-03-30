namespace ITC.Infra.CrossCutting.Identity.Models.QueryModel;

public class AccountInfoModel
{
#region Properties

    /// <summary>
    ///     Địa chỉ
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    ///     Avatar
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    ///     Quận/Huyện
    /// </summary>
    public string District { get; set; }

    public int DistrictId { get; set; }

    /// <summary>
    ///     Số fax
    /// </summary>
    public string Fax { get; set; }

    public string FullName  { get; set; }
    public bool   IsDeleted { get; set; }

    public bool IsSuperAdmin { get; set; }

    /// <summary>
    ///     Id đơn vị quản lý
    /// </summary>
    public string ManagementUnitId { get; }

    public string PhoneNumber { get; set; }

    /// <summary>
    ///     Thành phố
    /// </summary>
    public string Province { get; set; }

    public int    ProvinceId { get; set; }
    public string UserTypeId { get; set; }


    /// <summary>
    ///     Phường/Xã
    /// </summary>
    public string Ward { get; set; }

    public int WardId { get; set; }

    /// <summary>
    ///     Địa chỉ website
    /// </summary>
    public string Website { get; }

#endregion
}