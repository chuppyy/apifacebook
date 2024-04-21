using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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
        [IgnoreDataMember]
        public string IdAnalytic { get; set; }
        [IgnoreDataMember]
        public string TokenAK { get; set; }
        [IgnoreDataMember]
        public int IdTypeMoney { get; set; }
        [IgnoreDataMember]
        public double RatioMem { get; set; }
    }

    public class ConfigAnalyticsDto
    {
        public string Email { get; set; }
        public string PrivateKey { get; set; }
        public string TokenAK { get; set; }
    }

    public class ReportUserGroupNewDto : GroupReportDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }

    public class GroupReportDto
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime Created { get; set; }
        public int TotalPost { get; set; }
        public double Kpi { get; set; }
        /// <summary>
        /// Tiền
        /// </summary>
        public double Wages { get; set; }
    }

    public class TotalPostByGroupDto
    {
        public Guid GroupId { get; set; }
        public int Amount { get; set; }
    }

    public class ReportUserGroupResponseDto
    {
        public ReportUserGroupResponseDto()
        {
        }

        public ReportUserGroupResponseDto(Guid userId, string name, List<GroupReportDto> groups)
        {
            UserId = userId;
            Name = name;
            Groups = groups;
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<GroupReportDto> Groups { get; set; }
    }

    public class ReportData
    {
        public int IdDomain { get; set; }
        public  List<UserViewDto> UserViews { get; set; }
        public string Wages { get; set; }
    }
    public class MoneyData
    {
        public string Domain { get; set; }
        public string Wages { get; set; }
    }

    public class UserByOwnerDto
    {
        public string Name { get; set; }
        public Guid AvatarId { get; set; }
        public string UserCode { get; set; }
        public float Ratio { get; set; }
        public string UserId { get; set; }
    }
}
