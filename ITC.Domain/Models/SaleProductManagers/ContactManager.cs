using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     Quản lý liên hệ
/// </summary>
public class ContactManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public ContactManager(Guid   id,       string name,     string phone,     string email, string skype, string zalo,
                          string address,  string timeWork, string taxNumber, Guid   projectId, bool isShowHomePage,
                          int    position, Guid   avatarId, string googleMap, string googleMapLink, string hotline,
                          string facebook, string twitter,  string linkedin,  string youtube,
                          string createdBy = null) :
        base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        ProjectId = projectId;
        Update(name,     phone,     email,         skype, zalo, address, timeWork, taxNumber, isShowHomePage, position,
               avatarId, googleMap, googleMapLink, hotline, facebook, twitter, linkedin, youtube, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected ContactManager()
    {
    }

    /// <summary>
    ///     Tên liên hệ
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Điện thoại
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    ///     Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Skype
    /// </summary>
    public string Skype { get; set; }

    /// <summary>
    ///     Zalo
    /// </summary>
    public string Zalo { get; set; }

    /// <summary>
    ///     Địa chỉ
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    ///     Thời gian làm việc
    /// </summary>
    public string TimeWork { get; set; }

    /// <summary>
    ///     Mã số thuế
    /// </summary>
    public string TaxNumber { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Hiển thị trên trang chủ
    /// </summary>
    public bool IsShowHomePage { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Bản đồ
    /// </summary>
    public string GoogleMap { get; set; }

    /// <summary>
    ///     Bản đồ link
    /// </summary>
    public string GoogleMapLink { get; set; }

    /// <summary>
    ///     Đường dây nóng
    /// </summary>
    public string Hotline { get; set; }

    /// <summary>
    ///     Facebook
    /// </summary>
    public string Facebook { get; set; }

    /// <summary>
    ///     Youtube
    /// </summary>
    public string Youtube { get; set; }

    /// <summary>
    ///     Twitter
    /// </summary>
    public string Twitter { get; set; }

    /// <summary>
    ///     Linkedin
    /// </summary>
    public string Linkedin { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên liên hệ</param>
    /// <param name="phone">Điện thoại</param>
    /// <param name="email">Email</param>
    /// <param name="skype">Skype</param>
    /// <param name="zalo">Zalo</param>
    /// <param name="address">Địa chỉ</param>
    /// <param name="timeWork">Thời gian làm việc</param>
    /// <param name="taxNumber">Mã số thuế</param>
    /// <param name="isShowHomePage">Hiển thị trên trang chủ</param>
    /// <param name="position">Vị trí</param>
    /// <param name="avatarId">Anh đại diện</param>
    /// <param name="googleMap">Địa chỉ bản đồ</param>
    /// <param name="googleMapLink">Địa chỉ bản đồ rút gọn</param>
    /// <param name="hotline">Đường dây nóng</param>
    /// <param name="facebook">Facebook</param>
    /// <param name="twitter">Twitter</param>
    /// <param name="linkedin">Linkedin</param>
    /// <param name="youtube">Youtube</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name,     string phone,     string email, string skype, string zalo, string address,
                       string timeWork, string taxNumber, bool   isShowHomePage, int position,
                       Guid   avatarId, string googleMap, string googleMapLink, string hotline,
                       string facebook, string twitter,   string linkedin, string youtube, string createdBy = null)
    {
        Name           = name;
        Phone          = phone;
        Email          = email;
        Skype          = skype;
        Zalo           = zalo;
        Address        = address;
        TimeWork       = timeWork;
        TaxNumber      = taxNumber;
        IsShowHomePage = isShowHomePage;
        Position       = position;
        AvatarId       = avatarId;
        GoogleMap      = googleMap;
        GoogleMapLink  = googleMapLink;
        Hotline        = hotline;
        Facebook       = facebook;
        Twitter        = twitter;
        Linkedin       = linkedin;
        Youtube        = youtube;
        Update(createdBy);
    }
}