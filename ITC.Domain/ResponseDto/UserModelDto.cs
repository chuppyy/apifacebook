using System.Collections.Generic;

namespace ITC.Domain.ResponseDto
{
    public class UserModelDto
    {
        public string Name { get; set; }
        public string UserCode { get; set; }
    }

    public class DimensionValue
    {
        public string value { get; set; }
    }

    public class MetricValue
    {
        public string value { get; set; }
    }

    public class Row
    {
        public List<DimensionValue> dimensionValues { get; set; }
        public List<MetricValue> metricValues { get; set; }
    }

    public class Total
    {
        public List<DimensionValue> dimensionValues { get; set; }
        public List<MetricValue> metricValues { get; set; }
    }

    public class Metadata
    {
        public string currencyCode { get; set; }
        public string timeZone { get; set; }
    }

    public class RootObject
    {
        public List<Row> rows { get; set; }
        public List<Total> totals { get; set; }
        public int rowCount { get; set; }
        public Metadata metadata { get; set; }
        public string kind { get; set; }
    }

    public class UserViewDto
    {
        public UserViewDto()
        {

        }

        public UserViewDto(string link, double view)
        {
            Link = link;
            View = view;
        }
        public string Link { get; set; }
        public double View { get; set; }
    }

    public class UserReportDto
    {
        public UserReportDto()
        {

        }

        public UserReportDto(string name, string userCode, double totalView)
        {
            Name = name;
            UserCode = userCode;
            TotalView = totalView;
        }

        public string Name { get; set; }
        public string UserCode { get; set; }
        public double TotalView { get; set; }
    }

    public class ReportGoogleAnalyticsDto
    {
        public List<UserReportDto> Users { get; set; }
        public double TotalView { get; set; }
    }

    public class ComboboxIdNameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class WebsiteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdAnalytic { get; set; }
    }
}
