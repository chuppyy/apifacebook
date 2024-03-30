using System;
using System.Collections.Generic;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.Itphonui;

/// <summary>
///     Quản lý đơn vị
/// </summary>
public class ManagementManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public ManagementManager(Guid   id, Guid projectId, string name, string description, string parentId,
                             int    position,
                             int    levelCompetitionId, string symbol, string accountDefault,
                             string createdBy = null)
        : base(id, createdBy)
    {
        ProjectId                = projectId;
        PLeft                    = 1;
        PRight                   = 2;
        StatusId                 = ActionStatusEnum.Active.Id;
        ManagementDetailManagers = new List<ManagementDetailManager>();
        Update(name, description, parentId, position, levelCompetitionId, symbol, accountDefault, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected ManagementManager()
    {
    }

    /// <summary>
    ///     Tên đơn vị quản lý
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
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Bậc tham gia thi đấu
    /// </summary>
    public int LevelCompetitionId { get; set; }

    /// <summary>
    ///     Ký hiệu
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    ///     Tài khoản đăng nhập mặc định
    /// </summary>
    public string AccountDefault { get; set; }

    /// <summary>
    /// Tên huyện
    /// </summary>
    //public string Province { get; set; }

    /// <summary>
    ///     Liên kết khóa ngoại
    /// </summary>
    public virtual List<ManagementDetailManager> ManagementDetailManagers { get; set; }

    /// <summary>
    ///     Hàm cập nhật
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="parentId"></param>
    /// <param name="position"></param>
    /// <param name="levelCompetitionId"></param>
    /// <param name="symbol"></param>
    /// <param name="accountDefault"></param>
    /// <param name="createdBy"></param>
    public void Update(string name,               string description, string parentId, int position,
                       int    levelCompetitionId, string symbol,      string accountDefault,
                       string createdBy = null)
    {
        Name               = name;
        Description        = description;
        ParentId           = parentId;
        Position           = position;
        LevelCompetitionId = levelCompetitionId;
        Symbol             = symbol;
        AccountDefault     = accountDefault;
        Update(createdBy);
    }
}