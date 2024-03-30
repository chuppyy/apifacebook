using System;
using ITC.Domain.Core.Models;
using NCore.Actions;

namespace ITC.Domain.Models.SaleProductManagers;

/// <summary>
///     Quản lý hỏi đáp
/// </summary>
public class QuestionAskManager : RootModel
{
    /// <summary>
    ///     Hàm dựng có tham số
    /// </summary>
    public QuestionAskManager(Guid   id, string question, string answer, Guid projectId, int position,
                              string createdBy = null) :
        base(id, createdBy)
    {
        StatusId  = ActionStatusEnum.Active.Id;
        ProjectId = projectId;
        Position  = position;
        Update(question, answer, createdBy);
    }

    /// <summary>
    ///     Hàm dựng mặc định
    /// </summary>
    protected QuestionAskManager()
    {
    }

    /// <summary>
    ///     Tên câu hỏi
    /// </summary>
    public string Question { get; set; }

    /// <summary>
    ///     Trả lời
    /// </summary>
    public string Answer { get; set; }

    /// <summary>
    ///     Mã dự án
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Vị trí
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    ///     Cập nhật
    /// </summary>
    /// <param name="question">Tên câu hỏi</param>
    /// <param name="answer">Câu trả lời</param>
    /// <param name="createdBy">Người tạo</param>
    public void Update(string question, string answer, string createdBy = null)
    {
        Question = question;
        Answer   = answer;
        Update(createdBy);
    }
}