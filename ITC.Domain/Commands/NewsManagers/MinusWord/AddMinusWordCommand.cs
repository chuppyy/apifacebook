using ITC.Domain.Core.ModelShare.StudyManagers.MinusWord;

namespace ITC.Domain.Commands.NewsManagers.MinusWord;

/// <summary>
///     Command thêm môn học
/// </summary>
public class AddMinusWordCommand : MinusWordCommand
{
#region Constructors

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public AddMinusWordCommand(MinusWordEventModel model)
    {
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