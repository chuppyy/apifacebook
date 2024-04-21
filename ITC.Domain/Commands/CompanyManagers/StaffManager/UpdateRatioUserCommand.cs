using System.Collections.Generic;

namespace ITC.Domain.Commands.CompanyManagers.StaffManager
{
    public class UpdateRatioUserCommand
    {
        public List<string> UserIds { get; set; }
        public float Ratio { get; set; }
    }
}
