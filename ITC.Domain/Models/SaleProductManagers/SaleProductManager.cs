using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     Quản lý sản phẩm website bán hàng
/// </summary>
public class SaleProductManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public SaleProductManager(Guid   id,        string name,               string content,    Guid subjectTypeManagerId,
                              string secretKey, int    position,           int    statusId,   string icon,
                              Guid   avatarId,  bool   isShowListMenuHome, string seoKeyword, string summary,
                              Guid   projectId, double price,              double pricePromotion,
                              string createdBy = null)
        : base(id, createdBy)
    {
        ProjectId = projectId;
        ViewEye = 0;
        Update(name,               content,    subjectTypeManagerId, secretKey, position, statusId, icon, avatarId,
               isShowListMenuHome, seoKeyword, summary,              price,     pricePromotion, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected SaleProductManager()
    {
    }

    /// <summary>
    ///     Tên sản phẩm
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Nội dung sản phẩm
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Loại sản phẩm hiên thị website bán hàng
    /// </summary>
    public Guid SaleProductTypeManagerId { get; set; }

    /// <summary>
    ///     Mã bí mật
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Icon
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public Guid AvatarId { get; set; }

    /// <summary>
    ///     Hiển thị trên danh sách menu sản phẩm
    /// </summary>
    public bool IsShowListMenuHome { get; set; }

    /// <summary>
    ///     Từ khóa SEO
    /// </summary>
    public string SeoKeyword { get; set; }

    /// <summary>
    ///     Tóm tắt
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    ///     Lượt xem
    /// </summary>
    public int ViewEye { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Giá bán
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    ///     Giá bán khuyến mãi
    /// </summary>
    public double PricePromotion { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên sản phẩm</param>
    /// <param name="content">Nội dung sản phâm</param>
    /// <param name="subjectTypeManagerId">Loại sản phẩm</param>
    /// <param name="secretKey">Mã bí mật</param>
    /// <param name="position">Vị trí hiển thị</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="icon">icon</param>
    /// <param name="avatarId">ảnh đại diện</param>
    /// <param name="isShowListMenuHome">Hiển thị trên danh sách menu sản phẩm</param>
    /// <param name="seoKeyword">Từ khóa SEO</param>
    /// <param name="summary">Tóm tắt</param>
    /// <param name="price">Gía bán</param>
    /// <param name="pricePromotion">Giá khuyến mãi</param>
    /// <param name="createBy">Người tao</param>
    public void Update(string name,               string content,        Guid   subjectTypeManagerId, string secretKey,
                       int    position,           int    statusId,       string icon,                 Guid   avatarId,
                       bool   isShowListMenuHome, string seoKeyword,     string summary,
                       double price,              double pricePromotion, string createBy = null)
    {
        Name                     = name;
        Content                  = content;
        SaleProductTypeManagerId = subjectTypeManagerId;
        SecretKey                = secretKey;
        Position                 = position;
        StatusId                 = statusId;
        Icon                     = icon;
        AvatarId                 = avatarId;
        IsShowListMenuHome       = isShowListMenuHome;
        SeoKeyword               = seoKeyword;
        Summary                  = summary;
        PricePromotion           = pricePromotion;
        Price                    = price;
        Update(createBy);
    }
}