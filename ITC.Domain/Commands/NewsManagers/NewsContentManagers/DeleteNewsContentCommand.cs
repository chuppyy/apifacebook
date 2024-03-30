﻿#region

using System;
using System.Collections.Generic;

#endregion

namespace ITC.Domain.Commands.NewsManagers.NewsContentManagers;

/// <summary>
///     Command xóa bài viết
/// </summary>
public class DeleteNewsContentCommand : NewsContentCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    public DeleteNewsContentCommand(List<Guid> model)
    {
        Model = model;
    }

#endregion

#region Properties

    /// <summary>
    ///     Id cần xóa
    /// </summary>
    public List<Guid> Model { get; set; }

#endregion

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