using ITC.Domain.Core.ModelShare.StudyManagers.MinusWord;

namespace ITC.Domain.Commands.NewsManagers.MinusWord;

/// <summary>
///     Command Cập nhật môn học
/// </summary>
public class UpdateMinusWordCommand : MinusWordCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public UpdateMinusWordCommand(MinusWordEventModel model)
    {
        Id          = model.Id;
        Description = model.Description;
        Root        = model.Root;
        Replace     = model.Replace;
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