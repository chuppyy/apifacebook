using System;
using ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsContentManagers;

/// <summary>
///     Command cập nhật bài viết
/// </summary>
public class UpdateTimeAutoPostNewsContentCommand : NewsContentCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public UpdateTimeAutoPostNewsContentCommand(NewsContentUpdateTimeAutoPostModel model)
    {
        Id   = model.Id;
        Time = model.Time;
    }

#endregion
    public DateTime? Time { get; set; }

#region Methods

    /// <summary>
    ///     Kiểm tra valid
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        return true;
    }

#endregion
}