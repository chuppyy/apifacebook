using System;
using MediatR;
using NCore.Systems;

namespace ITC.Domain.Commands.GoogleAnalytics.Models
{
    /// <summary>
    /// Báo cáo google analytics
    /// </summary>
    public class GoogleAnalyticsReport : IRequest<JsonResponse<string>>
    {
        public GoogleAnalyticsReport()
        {
            
        }

        public GoogleAnalyticsReport(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime? StartDate { get; set; }                                        
        public DateTime? EndDate { get; set; }
    }
}
