using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.Itphonui;

/// <summary>
///     Quản lý dự án
/// </summary>
public class ProjectManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public ProjectManager(Guid     id,             string   name,           string description,
                          DateTime startDate,      DateTime endDate,        int    numberOfExtend,
                          string   hostName,       string   titleOne,       string titleTwo,
                          string   titleMobileOne, string   titleMobileTwo, string titleMobileThree,
                          bool     isUseLocalUrl,  string   code,           string createdBy = null)
        : base(id, createdBy)
    {
        StatusId = ActionStatusEnum.Active.Id;
        Code = code;
        Update(name,           description,    startDate,        endDate, numberOfExtend, hostName, titleOne, titleTwo,
               titleMobileOne, titleMobileTwo, titleMobileThree, isUseLocalUrl, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected ProjectManager()
    {
    }

    /// <summary>
    ///     Tên dự án
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Ngày bắt đầu
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    ///     Ngày kết thúc
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    ///     Số lần gia hạn
    /// </summary>
    public int NumberOfExtend { get; set; }

    /// <summary>
    ///     Địa chỉ host
    /// </summary>
    public string HostName { get; set; }

    public string TitleOne         { get; set; }
    public string TitleTwo         { get; set; }
    public string TitleMobileOne   { get; set; }
    public string TitleMobileTwo   { get; set; }
    public string TitleMobileThree { get; set; }

    /// <summary>
    ///     Tiêu đề xuất hiện trên trang login
    /// </summary>
    public string TitleOneLogin { get; set; }

    /// <summary>
    ///     Ảnh đại diện
    /// </summary>
    public string ImageAvatar { get; set; }

    /// <summary>
    ///     Mã code truy cập theo từng dự án
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    ///     Sử dụng local url đối với dự án này để kiểm tra và phát triển tính năng
    /// </summary>
    public bool IsUseLocalUrl { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="numberOfExtend"></param>
    /// <param name="hostName"></param>
    /// <param name="titleOne"></param>
    /// <param name="titleTwo"></param>
    /// <param name="titleMobileOne"></param>
    /// <param name="titleMobileTwo"></param>
    /// <param name="titleMobileThree"></param>
    /// <param name="isUseLocalUrl"></param>
    /// <param name="modifiedBy"></param>
    public void Update(string name, string description, DateTime startDate, DateTime endDate, int numberOfExtend,
                       string hostName, string titleOne, string titleTwo, string titleMobileOne,
                       string titleMobileTwo, string titleMobileThree, bool isUseLocalUrl, string modifiedBy = null)
    {
        Name             = name;
        Description      = description;
        StartDate        = startDate;
        EndDate          = endDate;
        NumberOfExtend   = numberOfExtend;
        HostName         = hostName;
        TitleOne         = titleOne;
        TitleTwo         = titleTwo;
        TitleMobileOne   = titleMobileOne;
        TitleMobileTwo   = titleMobileTwo;
        TitleMobileThree = titleMobileThree;
        IsUseLocalUrl    = isUseLocalUrl;
        Update(modifiedBy);
    }
}