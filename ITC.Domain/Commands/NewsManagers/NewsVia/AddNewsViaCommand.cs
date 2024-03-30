using ITC.Domain.Core.ModelShare.StudyManagers.NewsVia;

namespace ITC.Domain.Commands.NewsManagers.NewsVia;

/// <summary>
///     Command thêm môn học
/// </summary>
public class AddNewsViaCommand : NewsViaCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public AddNewsViaCommand(NewsViaEventModel model)
    {
        Code    = model.Code;
        Content = model.Content;
        Token   = model.Token;
        IdTkQc  = model.IdTkQc;
        StaffId = model.StaffId;
    }

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