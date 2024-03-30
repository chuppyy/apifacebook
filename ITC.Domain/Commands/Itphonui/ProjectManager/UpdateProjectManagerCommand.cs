using ITC.Domain.Core.ModelShare.Itphonui.ProjectManagers;

namespace ITC.Domain.Commands.Itphonui.ProjectManager;

/// <summary>
///     Command cập nhật quản lý dự án
/// </summary>
public class UpdateProjectManagerCommand : ProjectManagerCommand
{
#region Constructors

    public UpdateProjectManagerCommand(ProjectManagerEventModel model)
    {
        Id               = model.Id;
        Name             = model.Name;
        Description      = model.Description;
        StartDate        = model.StartDate;
        EndDate          = model.EndDate;
        NumberOfExtend   = model.NumberOfExtend;
        Nation           = model.Nation;
        HostName         = model.HostName;
        TitleOne         = model.TitleOne;
        TitleTwo         = model.TitleTwo;
        TitleMobileOne   = model.TitleMobileOne;
        TitleMobileTwo   = model.TitleMobileTwo;
        TitleMobileThree = model.TitleMobileThree;
        IsUseLocalUrl    = model.IsUseLocalUrl;
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