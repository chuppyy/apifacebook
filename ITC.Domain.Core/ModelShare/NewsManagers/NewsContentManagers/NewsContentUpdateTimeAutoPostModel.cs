using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.NewsManagers.NewsContentManagers;

/// <summary>
/// Thời gian đăng bài tự động
/// </summary>
public class NewsContentUpdateTimeAutoPostModel : PublishModal
{
    public Guid   Id   { get; set; }
    public DateTime? Time { get; set; }
}