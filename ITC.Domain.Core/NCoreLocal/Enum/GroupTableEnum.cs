using System.Collections.Generic;
using System.Linq;
using NCore.Systems;

namespace ITC.Domain.Core.NCoreLocal.Enum;

/// <summary>
///     Enum nhóm dữ liệu cùng cấu trúc SubjectTypeManager
/// </summary>
public class GroupTableEnum : EnumerationCore
{
    /// <summary>
    ///     Loại môn học
    /// </summary>
    public static readonly GroupTableEnum SubjectTypeManager = new(1, "Loại môn học");

    /// <summary>
    ///     Kiểu người dùng
    /// </summary>
    public static readonly GroupTableEnum UserTypeManager = new(2, "Kiểu người dùng");

    /// <summary>
    ///     Người dùng
    /// </summary>
    public static readonly GroupTableEnum UserManager = new(3, "Người dùng");

    /// <summary>
    ///     Môn thi đấu
    /// </summary>
    public static readonly GroupTableEnum SportSubjectTypeManager = new(4, "Môn thi đấu");

    /// <summary>
    ///     Chức vụ
    /// </summary>
    public static readonly GroupTableEnum PositionManager = new(5, "Chức vụ");

    /// <summary>
    ///     Nhiệm vụ
    /// </summary>
    public static readonly GroupTableEnum MissionManager = new(6, "Nhiệm vụ");

    /// <summary>
    ///     Môn học
    /// </summary>
    public static readonly GroupTableEnum SubjectManager = new(7, "Môn học");

    /// <summary>
    ///     Nội dung thi đấu
    /// </summary>
    public static readonly GroupTableEnum SportSubjectManager = new(8, "Nội dung thi đấu");

    /// <summary>
    ///     Quản trị tài khoản
    /// </summary>
    public static readonly GroupTableEnum ProjectAccountManager = new(9, "Quản trị tài khoản dự án");

    /// <summary>
    ///     Vận động viên
    /// </summary>
    public static readonly GroupTableEnum Atheles = new(10, "Vận động viên");

    /// <summary>
    ///     Lãnh đạo đoàn
    /// </summary>
    public static readonly GroupTableEnum TeamLead = new(11, "Lãnh đạo - cán bộ đoàn");

    /// <summary>
    ///     Loại khách hàng
    /// </summary>
    public static readonly GroupTableEnum CustomerTypeManager = new(12, "Loại khách hàng");

    /// <summary>
    ///     Loại nhân viên
    /// </summary>
    public static readonly GroupTableEnum StaffTypeManager = new(13, "Loại nhân viên");

    /// <summary>
    ///     Độ ưu tiên
    /// </summary>
    public static readonly GroupTableEnum PriorityManager = new(14, "Độ ưu tiên");

    /// <summary>
    ///     Loại sản phẩm
    /// </summary>
    public static readonly GroupTableEnum ProductTypeManager = new(15, "Loại sản phẩm");

    /// <summary>
    ///     Hình thức giao hàng
    /// </summary>
    public static readonly GroupTableEnum DeliveryMethodManager = new(16, "Hình thức giao hàng");

    /// <summary>
    ///     Hình thức đào tạo
    /// </summary>
    public static readonly GroupTableEnum FormsOfTrainingManager = new(17, "Hình thức đào tạo");

    /// <summary>
    ///     Hệ đào tạo
    /// </summary>
    public static readonly GroupTableEnum TrainingSystemManager = new(18, "Hệ đào tạo");

    /// <summary>
    ///     Loại sản phẩm hiển thị trên website bán hàng
    /// </summary>
    public static readonly GroupTableEnum SaleProductTypeManager =
        new(19, "Loại sản phẩm hiển thị trên website bán hàng");

    /// <summary>
    ///     Sản phẩm hiển thị trên website bán hàng
    /// </summary>
    public static readonly GroupTableEnum SaleProductManager = new(20, "Sản phẩm hiển thị trên website bán hàng");

    protected GroupTableEnum(int id, string name) : base(id, name)
    {
    }

    /// <summary>
    ///     GetList
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<GroupTableEnum> GetList()
    {
        return new List<GroupTableEnum>
        {
            SubjectTypeManager,
            UserTypeManager,
            UserManager,
            SportSubjectTypeManager,
            PositionManager,
            MissionManager,
            SubjectManager,
            SportSubjectManager,
            ProjectAccountManager,
            Atheles,
            TeamLead,
            CustomerTypeManager,
            StaffTypeManager,
            PriorityManager,
            ProductTypeManager,
            SaleProductTypeManager,
            SaleProductManager
        };
    }

    /// <summary>
    ///     Lấy dữ liệu theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static GroupTableEnum GetById(int id)
    {
        return GetList().FirstOrDefault(x => x.Id == id);
    }
}