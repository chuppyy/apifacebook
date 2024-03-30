using ITC.Domain.Core.ModelShare.NewsManagers.NewsGroupManagers;

namespace ITC.Domain.Commands.NewsManagers.NewsGroupManagers;

/// <summary>
///     Command Cập nhật nhóm tin
/// </summary>
public class UpdateNewsGroupCommand : NewsGroupCommand
{
    #region Constructors

    public UpdateNewsGroupCommand()
    {
        
    }

    /// <summary>
    ///     Hàm dựng với tham số
    /// </summary>
    /// <param name="model">Dữ liệu nhận từ FE</param>
    public UpdateNewsGroupCommand(NewsGroupEventModel model)
    {
        Id           = model.Id;
        Description  = model.Description;
        Name         = model.Name;
        Position     = model.Position;
        ParentId     = model.ParentId;
        TypeId       = model.NewsGroupTypeId;
        Domain       = model.Domain;
        IdViaQc      = model.IdViaQc;
        AgreeVia     = model.AgreeVia;
        LinkTree     = model.LinkTree;
        StaffId      = model.StaffId;
        DomainVercel = model.DomainVercel;
        Amount = model.Amount;
        AmountPosted = model.AmountPosted;
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