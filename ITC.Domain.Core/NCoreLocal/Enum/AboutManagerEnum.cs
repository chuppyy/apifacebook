using System.Collections.Generic;
using System.Linq;
using NCore.Systems;

namespace ITC.Domain.Core.NCoreLocal.Enum;

/// <summary>
///     Enum giới thiệu
/// </summary>
public class AboutManagerEnum : EnumerationCore
{
    public static readonly AboutManagerEnum VeChungToi         = new(1, "Về chúng tôi");
    public static readonly AboutManagerEnum ChinhSachThanhToan = new(2, "Chính sách thanh toán");
    public static readonly AboutManagerEnum ChinhSachDoiTra    = new(3, "Chính sách đổi trả");
    public static readonly AboutManagerEnum ChinhSachBaoMat    = new(4, "Chính sách bảo mật");
    public static readonly AboutManagerEnum ChinhSachGiaoHang  = new(5, "Chính sách giao hàng");
    public static readonly AboutManagerEnum ChinhSachInNhanh   = new(6, "Chính sách in nhanh");
    public static readonly AboutManagerEnum CoCauToChuc        = new(7, "Cơ cấu tổ chức");
    public static readonly AboutManagerEnum ChinhSachBanHang   = new(8, "Chính sách bán hàng");

    public static readonly AboutManagerEnum LichSuHinhThanhVaPhatTrien = new(9, "Lịch sử hình thành và phát triển");

    public static readonly AboutManagerEnum SuMenhVaTamNhin      = new(10, "Sứ mệnh và tầm nhìn");
    public static readonly AboutManagerEnum DanhHieuVaGiaiThuong = new(11, "Danh hiệu và Giải thưởng");
    public static readonly AboutManagerEnum VanHoaDoanhNghiep    = new(12, "Văn hóa doanh nghiệp");

    protected AboutManagerEnum(int id, string name) : base(id, name)
    {
    }

    public AboutManagerEnum()
    {
    }

    /// <summary>
    ///     GetList
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<AboutManagerEnum> GetList()
    {
        return GetAll<AboutManagerEnum>();
    }

    /// <summary>
    ///     Lấy dữ liệu theo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static AboutManagerEnum GetById(int id)
    {
        return GetList().FirstOrDefault(x => x.Id == id);
    }
}