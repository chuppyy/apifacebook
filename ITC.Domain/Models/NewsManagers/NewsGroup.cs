using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;
using ITC.Domain.Extensions;

namespace ITC.Domain.Models.NewsManagers;

/// <summary>
///     Nhóm tin
/// </summary>
public class NewsGroup : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public NewsGroup(Guid   id,         string name,         string description,     string parentId,  int    position,
                     string secretKey,  int    statusId,     Guid   newsGroupTypeId, Guid   projectId, string domain,
                     string metatitle,  string idViaQc,      bool   agreeVia,        string linkTree,  Guid   staffId,
                     bool   isShowMain, string domainVercel, string createdBy = null, int? amount = null, int? amountPosted = null)
        : base(id, createdBy)
    {
        PLeft        = 1;
        PRight       = 2;
        ProjectId    = projectId;
        NewsContents = new List<NewsContent>();
        Update(name,    description, parentId, position, secretKey,  statusId, newsGroupTypeId, domain, metatitle,
               idViaQc, agreeVia,    linkTree, staffId,  isShowMain, domainVercel, createdBy, amount, amountPosted);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected NewsGroup()
    {
    }

    /// <summary>
    ///     Tên nhóm
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
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Mã bí mật
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    ///     Loại nhóm dữ liệu
    /// </summary>
    public Guid NewsGroupTypeId { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Domain
    /// </summary>
    public string Domain { get; set; }

    /// <summary>
    ///     Tên SEO
    /// </summary>
    public string MetaTitle { get; set; }

    public string IdViaQc  { get; set; }
    public bool   AgreeVia { get; set; }

    /// <summary>
    /// ID Page quảng cáo
    /// </summary>
    public string LinkTree { get; set; }

    public string DomainVercel   { get; set; }
    public Guid   StaffId    { get; set; }
    public bool   IsShowMain { get; set; }

    /// <summary>
    /// Số lần để reset
    /// </summary>
    public int? Amount { get; set; }
    /// <summary>
    /// Số lần đã đăng
    /// </summary>
    public int? AmountPosted { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<NewsContent> NewsContents { get; set; }

    /// <summary>
    /// Enum <see cref="GroupType"/>
    /// </summary>
    public int? TypeId { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name">Tên menu</param>
    /// <param name="description">Mô tả</param>
    /// <param name="parentId">Mã cha - con</param>
    /// <param name="position">Vị trí hiển thị</param>
    /// <param name="secretKey">Mã bí mật</param>
    /// <param name="statusId">Trạng thái</param>
    /// <param name="newsGroupTypeId">Loại dữ liệu</param>
    /// <param name="domain">Domain</param>
    /// <param name="metatitle">metatitle</param>
    /// <param name="idViaQc"></param>
    /// <param name="agreeVia"></param>
    /// <param name="linkTree"></param>
    /// <param name="staffId"></param>
    /// <param name="isShowMain"></param>
    /// <param name="domainVercel"></param>
    /// <param name="createdBy">Người tạo</param>
    /// <param name="amount"></param>
    /// <param name="amountPosted"></param>
    public void Update(string name,      string description, string parentId,        int    position,
                       string secretKey, int    statusId,    Guid   newsGroupTypeId, string domain,
                       string metatitle, string idViaQc,     bool   agreeVia,        string linkTree,
                       Guid   staffId,   bool   isShowMain,  string domainVercel, string createdBy = null, int? amount = null, int? amountPosted = null)
    {
        Name            = name;
        Description     = description;
        ParentId        = parentId;
        Position        = position;
        SecretKey       = secretKey;
        StatusId        = statusId;
        NewsGroupTypeId = newsGroupTypeId;
        Domain          = domain;
        MetaTitle       = metatitle;
        IdViaQc         = idViaQc;
        AgreeVia        = agreeVia;
        LinkTree        = linkTree;
        StaffId         = staffId;
        IsShowMain      = isShowMain;
        DomainVercel    = domainVercel;
        Update(createdBy);
        Amount = amount;
        AmountPosted = amountPosted;
    }
}