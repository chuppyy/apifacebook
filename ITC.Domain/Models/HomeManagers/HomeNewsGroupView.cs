using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;
using ITC.Domain.Core.ModelShare.HomeManagers.HomeNewsGroupViewManagers;

namespace ITC.Domain.Models.HomeManagers;

/// <summary>
///     Danh sách các tin sẽ hiển thị trên trang chủ
/// </summary>
public class HomeNewsGroupView : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public HomeNewsGroupView(Guid id,       string name,     string description, string parentId, string url,
                             int  position, int    statusId, string secretKey,   string createdBy = null)
        : base(id, createdBy)
    {
        PLeft                    = 1;
        PRight                   = 2;
        HomeNewsGroupViewDetails = new List<HomeNewsGroupViewDetail>();
        Update(name, description, parentId, url, position, statusId, secretKey, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected HomeNewsGroupView()
    {
    }

    /// <summary>
    ///     Tên menu
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Mã cha - con
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    ///     Giá trị trái
    /// </summary>
    public int PLeft { get; set; }

    /// <summary>
    ///     Giá trị phải
    /// </summary>
    public int PRight { get; set; }

    /// <summary>
    ///     Đường dẫn
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Mã bí mật
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<HomeNewsGroupViewDetail> HomeNewsGroupViewDetails { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên menu</param>
    /// <param name="description">Mô tả</param>
    /// <param name="parentId">Mã cha - con</param>
    /// <param name="url">Đường dẫn</param>
    /// <param name="position">Vị trí hiển thị</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="secretKey">Mã bí mật</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string name,
                       string description,
                       string parentId,
                       string url,
                       int    position,
                       int    statusId,
                       string secretKey,
                       string createdBy = null)
    {
        Name        = name;
        Description = description;
        ParentId    = parentId;
        Url         = url;
        Position    = position;
        StatusId    = statusId;
        SecretKey   = secretKey;
        Update(createdBy);
    }

    /// <summary>
    ///     Thêm nhóm tin vào menu
    /// </summary>
    /// <param name="models">Danh sách nhóm tin</param>
    /// <param name="historyPosition">Lịch sử vị trí</param>
    /// <param name="userId">Người tạo</param>
    public void AddNewsGroup(List<HomeNewsGroupViewDetailModel> models, int historyPosition, string userId)
    {
        foreach (var items in models)
            HomeNewsGroupViewDetails.Add(new HomeNewsGroupViewDetail(Guid.NewGuid(),
                                                                     items.Id,
                                                                     historyPosition,
                                                                     userId));
    }
}