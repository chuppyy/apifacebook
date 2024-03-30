namespace ITC.Application.Helpers;

/// <summary>
///     Class khai báo các quyền hệ thống
/// </summary>
public class ModuleIdentity
{
    public const string RoomManager           = "RoomManager";           // Phòng ban
    public const string CustomerManager       = "CustomerManager";       // Khách hàng
    public const string CustomerTypeManager   = "CustomerTypeManager";   // Loại khách hàng
    public const string StaffManager          = "StaffManager";          // Nhân viên
    public const string StaffTypeManager      = "StaffTypeManager";      // Loại nhân viên
    public const string AuthorityManager      = "AuthorityManager";      // Quyền sử dụng
    public const string NewsGroup             = "NewsGroup";             // Quản lý nhóm tin
    public const string NewsGroupType         = "NewsGroupType";         // Quản lý loại nhóm tin
    public const string NewsContent           = "NewsContent";           // Quản lý bài viết
    public const string NewsSeoKeyWord        = "NewsSeoKeyWord";        // Quản lý từ khóa SEO
    public const string Helpers               = "Helpers";               // Controller hỗ trợ
    public const string HomeMenu              = "HomeMenu";              // Quản lý menu trang chủ
    public const string HomeNewsGroupView     = "HomeNewsGroupView";     // Quản lý các bài viê hiển thị trên trang chủ
    public const string ServerFileManager     = "ServerFileManager";     // ServerFileManager
    public const string SubjectManager        = "SubjectManager";        // Môn học
    public const string SubjectTypeManager    = "SubjectTypeManager";    // Loại môn học
    public const string QuestionManager       = "QuestionManager";       // Câu hỏi
    public const string UserTypeManager       = "UserTypeManager";       // Kiểu người dùng
    public const string UserManager           = "UserManager";           // Người dùng
    public const string NotificationManager   = "NotificationManager";   // Thông báo hệ thống
    public const string PositionManager       = "PositionManager";       // Chức vụ
    public const string MissionManager        = "MissionManager";        // Nhiệm vụ
    public const string PriorityManager       = "PriorityManager";       // Độ ưu tiên
    public const string ProductTypeManager    = "ProductTypeManager";    // Loại sản phẩm
    public const string DeliveryMethodManager = "DeliveryMethodManager"; // Hình thức giao hàng
    public const string CommentManager        = "CommentManager";        // Đánh giá
    public const string NewsVia               = "NewsVia";               // Đánh giá
    public const string NewsGithub            = "NewsGithub";            // Đánh giá
    public const string NewsDomain            = "NewsDomain";            // Đánh giá

    public const string FormsOfTrainingManager  = "FormsOfTrainingManager";  // Hình thức đào tạo
    public const string TrainingSystemManager   = "TrainingSystemManager";   // Hệ đào tạo
    public const string SchoolYearManager       = "SchoolYearManager";       // Năm học
    public const string ViewDegreeManager       = "ViewDegreeManager";       // Xem văn bằng
    public const string ViewDegreeDetailManager = "ViewDegreeDetailManager"; // Xem văn bằng chi tiết

    public const string ProjectManager        = "ProjectManager";        // Quản lý dự án
    public const string ProjectAccountManager = "ProjectAccountManager"; // Quản lý tài khoản quản trị đơn vị
    public const string ManagementManager     = "ManagementManager";     // Quản lý đơn vị
    public const string SortMenuManager       = "SortMenuManager";       // Quản lý sắp xếp menu

    public const string AuthorityManagerAllSystem = "AuthorityManagerAllSystem"; // Quyền sử dụng toàn hệ thống
    public const string SportSubjectTypeManager   = "SportSubjectTypeManager";   // Môn thi đấu
    public const string SportSubjectManager       = "SportSubjectManager";       // Nội dung thi đấu
    public const string SportSubmitDocument       = "SportSubmitDocument";       // Nộp hồ sơ
    public const string Athletes                  = "Athletes";                  // Vận động viên
    public const string TeamLeader                = "TeamLeader";                // Lãnh đạo - cán bộ đoàn
    public const string DrawManager               = "DrawManager";               // Bốc thăm
    public const string ResultManager             = "ResultManager";             // Kết quả
    public const string InputCompetitionResult    = "InputCompetitionResult";    // Nhập kết quả thi đấu
    public const string ChatManager               = "ChatManager";               // ChatManager
    public const string ScheduleManager           = "ScheduleManager";           // Lịch thi đấu

    // Loại sản phẩm hiển thị trên website bán hàng
    public const string SaleProductTypeManager    = "SaleProductTypeManager";
    public const string SaleProductManager        = "SaleProductManager"; // Sản phẩm hiển thị trên website bán hàng
    public const string AboutManager              = "AboutManager"; // Giới thiệu
    public const string ContactManager            = "ContactManager"; // Liên hệ
    public const string ContactCustomerManager    = "ContactCustomerManager"; // Khách hàng Liên hệ
    public const string PartnerManager            = "PartnerManager"; // đối tác - nhà cung cấp
    public const string SlideManager              = "SlideManager"; // Slide
    public const string QuestionAskManager        = "QuestionAskManager"; // Hỏi đáp
    public const string NewsRecruitment           = "NewsRecruitment"; // Tuyển dụng
    public const string NewsRecruitmentQuote      = "NewsRecruitmentQuote"; // Báo giá
    public const string HealthHandbookManager     = "HealthHandbookManager"; // Cẩm nang sức khỏe
    public const string ImageLibraryManager       = "ImageLibraryManager"; // Thư viện hình ảnh
    public const string ImageLibraryDetailManager = "ImageLibraryDetailManager"; // Thư viện hình ảnh - chi tiết

    public const string FunctionSystemManager = "FunctionSystemManager"; // Chức năng hệ thống
    public const string MinusWord             = "MinusWord";             // Từ loại trừ
}