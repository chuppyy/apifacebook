using System;

namespace ITC.Domain.Core.ModelShare.CompanyManager.StaffManagers;

public class StaffManagerByUserDto
{
    public Guid   Id                 { get; set; }
    public string Name               { get; set; }
    public Guid   ManagementId       { get; set; }
    public string ManagementName     { get; set; }
    public Guid   ProjectId          { get; set; }
    public string ProjectName        { get; set; }
    public int    LevelCompetitionId { get; set; }
}