using System;

namespace ITC.Domain.Commands.GoogleAnalytics.Models
{
    public class ReportUserPostQuery
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
