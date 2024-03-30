using System;

namespace ITC.Infra.CrossCutting.Identity.Models.QueryModel;

/// <summary>
///     Bảng dữ liệu quyền sử dụng theo người dùng
/// </summary>
public class AuthorityByUserDto
{
    public string Name        { get; set; }
    public string ParentId    { get; set; }
    public Guid   AuthorityId { get; set; }
    public Guid   StaffId     { get; set; }
    public string StaffName   { get; set; }
    public string Code        { get; set; }
    public int    Value       { get; set; }
    public int    Position    { get; set; }
}