#region

using System;
using ITC.Domain.Core.Models;
using Microsoft.AspNetCore.Identity;

#endregion

namespace ITC.Infra.CrossCutting.Identity.Models;

public class ApplicationUser : IdentityUser
{
#region Constructors

    public ApplicationUser()
    {
        IsDeleted      = false;
        IsSuperAdmin   = false;
        LockoutEnabled = false;
    }

    public ApplicationUser(string     userName, string fullName, string email, string phoneNumber, string userTypeId,
                           string     avatar,
                           UserTarget user) : this()
    {
        UserName    = userName;
        FullName    = fullName;
        Email       = email;
        PhoneNumber = phoneNumber;
        UserTypeId  = userTypeId;
        Avatar      = avatar;
        User        = user;
    }

    public ApplicationUser(string userName, string fullName, string email, string phoneNumber, string userTypeId,
                           string avatar,
                           string managementUnitId, int portalId, UserTarget user) : this(userName, fullName, email,
        phoneNumber, userTypeId, avatar,
        user)
    {
        ManagementUnitId = managementUnitId;
        PortalId         = portalId;
    }

    public ApplicationUser(string userName, string fullName, string email, string phoneNumber, string userTypeId,
                           string avatar,
                           string managementUnitId, string officalProfileId, int portalId, UserTarget user)
        : this(userName, fullName, email, phoneNumber, userTypeId, avatar, user)
    {
        ManagementUnitId = managementUnitId;
        PortalId         = portalId;
        OfficalProfileId = officalProfileId;
    }

    /// <summary>
    ///     Hàm dựng đầy đủ thông tin
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="fullName"></param>
    /// <param name="email"></param>
    /// <param name="phoneNumber"></param>
    /// <param name="userTypeId"></param>
    /// <param name="avatar"></param>
    /// <param name="managementUnitId"></param>
    /// <param name="user"></param>
    public ApplicationUser(string userName, string fullName, string email, int portalId, string phoneNumber,
                           string userTypeId, string avatar,
                           string managementUnitId, UserTarget user,
                           string address, string fax) : this(userName, fullName, email, phoneNumber, userTypeId,
                                                              avatar, user)
    {
        PortalId         = portalId;
        ManagementUnitId = managementUnitId;
        Address          = address;
        Fax              = fax;
    }

    public ApplicationUser(string userName, string fullName, string email, int portalId, string phoneNumber,
                           string userTypeId, string avatar,
                           string managementUnitId, UserTarget user,
                           int    provinceId, int districtId, int wardId, string address, string fax) : this(userName,
        fullName, email, phoneNumber,
        userTypeId, avatar, user)
    {
        PortalId         = portalId;
        ManagementUnitId = managementUnitId;
        ProvinceId       = provinceId;
        DistrictId       = districtId;
        WardId           = wardId;
        Address          = address;
        Fax              = fax;
    }

#endregion

#region Properties

    /// <summary>
    ///     Địa chỉ
    /// </summary>
    public string Address { get; private set; }

    /// <summary>
    ///     Avatar
    /// </summary>
    public string Avatar { get; private set; }

    public int DistrictId { get; set; }

    /// <summary>
    ///     Số fax
    /// </summary>
    public string Fax { get; private set; }

    public string FullName     { get; private set; }
    public bool   IsDeleted    { get; private set; }
    public bool   IsSuperAdmin { get; }

    /// <summary>
    ///     Id đơn vị quản lý
    /// </summary>
    public string ManagementUnitId { get; private set; }

    /// <summary>
    ///     Id hồ sơ cán bộ
    /// </summary>
    public string OfficalProfileId { get; set; }

    public int        PortalId               { get; }
    public int        ProvinceId             { get; set; }
    public string     RefreshToken           { get; set; }
    public DateTime   RefreshTokenExpiryTime { get; set; }
    public UserTarget User                   { get; private set; }

    //public UserType UserType { get; private set; }
    public string UserTypeId { get; private set; }
    public int    WardId     { get; set; }

    /// <summary>
    ///     Địa chỉ website
    /// </summary>
    public string Website { get; set; }

#endregion

#region Methods

    public void AddEmail(string email)
    {
        Email          = email;
        EmailConfirmed = false;
    }

    public void SetActive(bool isActive)
    {
        EmailConfirmed = isActive;
    }

    public void SetDelete(bool isDelete)
    {
        IsDeleted = isDelete;
    }


    public void SetAvatar(string avatar)
    {
        Avatar = avatar;
    }

    public void SetName(string avatar)
    {
        FullName = avatar;
    }

    public void SetUserName(string avatar)
    {
        UserName = avatar;
    }

    public void SetLock(bool isLock, DateTime lockoutEnd)
    {
        LockoutEnabled = isLock;
        LockoutEnd     = lockoutEnd;
    }

    public void SetManagementUnitId(string id)
    {
        ManagementUnitId = id;
    }

    public void Setpassword(string pass)
    {
    }

    public void UpdateApplicationUser(string email, string fullName, string phoneNumber, string userTypeId,
                                      string avatar)
    {
        Email       = email;
        FullName    = fullName;
        PhoneNumber = phoneNumber;
        UserTypeId  = userTypeId;
        if (!string.IsNullOrEmpty(avatar))
            Avatar = avatar;
    }

    public void UpdateApplicationUser(string email,  string fullName, string phoneNumber, string userTypeId,
                                      string avatar, string address)
    {
        UpdateApplicationUser(email, fullName, phoneNumber, userTypeId, avatar);
        Address = address;
    }

    public void UpdateApplicationUser(string email,   string fullName, string address, string phoneNumber, string fax,
                                      string website, string avatar)
    {
        Email       = email;
        FullName    = fullName;
        Address     = address;
        Fax         = fax;
        Website     = website;
        PhoneNumber = phoneNumber;
        if (!string.IsNullOrEmpty(avatar))
            Avatar = avatar;
    }

    public void UpdateApplicationUser(string email,   string fullName, int provinceId, int districtId, int wardId,
                                      string address, string phoneNumber,
                                      string fax,     string website, string avatar)
    {
        Email       = email;
        FullName    = fullName;
        ProvinceId  = provinceId;
        DistrictId  = districtId;
        WardId      = wardId;
        Address     = address;
        Fax         = fax;
        Website     = website;
        PhoneNumber = phoneNumber;
        if (!string.IsNullOrEmpty(avatar))
            Avatar = avatar;
    }

    public void UpdateApplicationUser(string managenentId, string email,  string fullName, int provinceId,
                                      int    districtId,   int    wardId, string address,
                                      string phoneNumber,  string fax,    string website, string avatar)
    {
        ManagementUnitId = managenentId;
        Email            = email;
        FullName         = fullName;
        ProvinceId       = provinceId;
        DistrictId       = districtId;
        WardId           = wardId;
        Address          = address;
        Fax              = fax;
        Website          = website;
        PhoneNumber      = phoneNumber;
        if (!string.IsNullOrEmpty(avatar))
            Avatar = avatar;
    }

    public void UpdateRefreshToken(string refreshToken, DateTime expiryTime)
    {
        RefreshToken           = refreshToken;
        RefreshTokenExpiryTime = expiryTime;
    }

    /// <summary>
    ///     Cập nhật người chỉnh sửa
    /// </summary>
    /// <param name="modifyBy"></param>
    /// <param name="modifyDate"></param>
    public void UpdateUserTager(string modifiedBy, DateTime lastDateModified)
    {
        if (User == null) User = new UserTarget();
        User.UpdateUserTager(modifiedBy, lastDateModified);
    }

#endregion
}