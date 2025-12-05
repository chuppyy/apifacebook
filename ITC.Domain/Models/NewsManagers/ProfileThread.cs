using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.NewsManagers;

/// <summary>
///     Via
/// </summary>
public class ProfileThread : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
   

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected ProfileThread()
    {
    }

    public string Profile    { get; set; }
    public int Position { get; set; }
    public DateTime ModifiedDate   { get; set; }

    public void Update(int position)
    {
        Position = position;
        Update();
    }
}