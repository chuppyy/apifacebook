using System;
using ITC.Domain.Core.Models;

namespace ITC.Domain.Models.AuthorityManager;

/// <summary>
///     Doanh nghiệp
/// </summary>
public class AuthorityDetail : RootModel
{
    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected AuthorityDetail()
    {
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public AuthorityDetail(Guid   id,
                           Guid   authorityId,
                           Guid   menuManagerId,
                           int    value,
                           int    historyPosition,
                           string createBy = null) :
        base(id, createBy)
    {
        Update(authorityId, menuManagerId, value, historyPosition, createBy);
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public AuthorityDetail(Guid   id,
                           Guid   authorityId,
                           Guid   menuManagerId,
                           int    value,
                           int    historyPosition,
                           string name,
                           string createBy = null) :
        base(id, createBy)
    {
        Name = name;
        Update(authorityId, menuManagerId, value, historyPosition, createBy);
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public AuthorityDetail(Guid   id,
                           Guid   menuManagerId,
                           int    value,
                           int    historyPosition,
                           string name,
                           string createBy = null) : base(id, createBy)
    {
        MenuManagerId   = menuManagerId;
        Value           = value;
        HistoryPosition = historyPosition;
        Name            = name;
    }

    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public AuthorityDetail(Guid   id,
                           int    value,
                           int    historyPosition,
                           string createBy = null) : base(id, createBy)
    {
        Value           = value;
        HistoryPosition = historyPosition;
    }

    /// <summary>
    ///     Mã quyền
    /// </summary>
    public Guid AuthorityId { get; set; }

    /// <summary>
    ///     Mã menu
    /// </summary>
    public Guid MenuManagerId { get; set; }

    /// <summary>
    ///     Giá trị
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    ///     Lịch sử vị trí
    /// </summary>
    public int HistoryPosition { get; set; }

    /// <summary>
    ///     Tên hiển thị sau khi sắp xếp
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Vị trí hiển thị
    /// </summary>
    public int Position { get; set; }

    public void Update(Guid   authorityId, Guid menuManagerId, int value, int historyPosition,
                       string modifieldBy = null)
    {
        AuthorityId     = authorityId;
        MenuManagerId   = menuManagerId;
        Value           = value;
        HistoryPosition = historyPosition;
        Update(modifieldBy);
    }
}