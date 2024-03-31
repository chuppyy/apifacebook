using System;
using System.Collections.Generic;
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

        public GoogleAnalyticsReport(DateTime? startDate, DateTime? endDate, List<int> domainIds)
        {
            StartDate = startDate;
            EndDate = endDate;
            DomainIds = domainIds;
        }

        public DateTime? StartDate { get; set; }                                        
        public DateTime? EndDate { get; set; }
        public List<int> DomainIds { get; set; }
    }
}
