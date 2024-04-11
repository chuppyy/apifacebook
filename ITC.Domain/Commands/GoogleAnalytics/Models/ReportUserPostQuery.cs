using System;
using System.Collections.Generic;

namespace ITC.Domain.Commands.GoogleAnalytics.Models
{
    public class ReportUserPostQuery
    {
        public List<int> DomainIds { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
