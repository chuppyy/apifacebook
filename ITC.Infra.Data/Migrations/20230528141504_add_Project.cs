using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITC.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class addProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutAttackManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AboutManagerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutAttackManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoKeyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ViewEye = table.Column<int>(type: "int", nullable: false),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    MetaLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authorities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    UrlHomePage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorityUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    AuthorityId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorityUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    SecrectKey = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PLeft = table.Column<int>(type: "int", nullable: false),
                    PRight = table.Column<int>(type: "int", nullable: false),
                    UserAgree = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    StartAgree = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactCustomerManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HandlerTypeId = table.Column<int>(type: "int", nullable: false),
                    HandlerUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandlerContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandlerDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactCustomerManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zalo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsShowHomePage = table.Column<bool>(type: "bit", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoogleMap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoogleMapLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hotline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Youtube = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Linkedin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageLibraryDetailManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    ImageLibraryManagerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageLibraryDetailManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageLibraryManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageLibraryManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 40, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NumberOf = table.Column<int>(type: "int", nullable: false),
                    Positon = table.Column<int>(type: "int", nullable: false),
                    ManagerIconId = table.Column<int>(type: "int", nullable: false),
                    LinkDefault = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuManager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    ManagerICon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MenuGroupId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Router = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    MLeft = table.Column<int>(type: "int", nullable: false),
                    MRight = table.Column<int>(type: "int", nullable: false),
                    PermissionValue = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuManager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    NewsContentId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PLeft = table.Column<int>(type: "int", nullable: false),
                    PRight = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfStar = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsComments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsGroupTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsGroupTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsRecruitments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SeoKeyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    DateTimeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewEye = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsRecruitments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsSeoKeyWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsSeoKeyWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionDefaults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 40, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionDefaults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileNameRoot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSize = table.Column<double>(type: "float", nullable: false),
                    FileType = table.Column<int>(type: "int", maxLength: 40, nullable: false),
                    VideoType = table.Column<int>(type: "int", nullable: false),
                    Folder = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFolder = table.Column<bool>(type: "bit", nullable: false),
                    IsLocal = table.Column<bool>(type: "bit", nullable: false),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PLeft = table.Column<int>(type: "int", nullable: false),
                    PRight = table.Column<int>(type: "int", nullable: false),
                    CloudId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CloudFolder = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ManagementId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: true),
                    AvatarLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    IsRoot = table.Column<bool>(type: "bit", nullable: false),
                    GroupFile = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagementId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    RoomManagerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    UserTypeManagerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsStaff = table.Column<bool>(type: "bit", nullable: false),
                    TimeConnectStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeConnectEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolClass = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SchoolName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CitizenIdentification = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    IsSportAthletics = table.Column<bool>(type: "bit", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    MissionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    IdentityNumberStudy = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    NationPeople = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SexId = table.Column<int>(type: "int", nullable: false),
                    IsOwnerManagement = table.Column<bool>(type: "bit", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTypeManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTypeManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    ActionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SystemLogType = table.Column<int>(type: "int", nullable: false),
                    UserCreateId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    UserCreateName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NameFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: true),
                    DataOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableDeleteManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableDeleteManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorityDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    AuthorityId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    MenuManagerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    HistoryPosition = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorityDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorityDetail_Authorities_AuthorityId",
                        column: x => x.AuthorityId,
                        principalTable: "Authorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PLeft = table.Column<int>(type: "int", nullable: false),
                    PRight = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NewsGroupTypeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsGroups_NewsGroupTypes_NewsGroupTypeId",
                        column: x => x.NewsGroupTypeId,
                        principalTable: "NewsGroupTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportRegister",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    SubjectDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    IsDraw = table.Column<bool>(type: "bit", nullable: false),
                    DrawDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportRegister_StaffManagers_StaffManagerId",
                        column: x => x.StaffManagerId,
                        principalTable: "StaffManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffAttackManager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAttackManager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffAttackManager_StaffManagers_StaffManagerId",
                        column: x => x.StaffManagerId,
                        principalTable: "StaffManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PLeft = table.Column<int>(type: "int", nullable: false),
                    PRight = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    SubjectTypeManagerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    IsShowMenuHomePage = table.Column<bool>(type: "bit", nullable: false),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectManagers_SubjectTypeManagers_SubjectTypeManagerId",
                        column: x => x.SubjectTypeManagerId,
                        principalTable: "SubjectTypeManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Author = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UrlRootLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsGroupId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    SeoKeyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    AvatarLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarLocal = table.Column<bool>(type: "bit", nullable: false),
                    DateTimeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewsGroupTypeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    AttackViewId = table.Column<int>(type: "int", nullable: false),
                    ContentJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewEye = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsContents_NewsGroupTypes_NewsGroupTypeId",
                        column: x => x.NewsGroupTypeId,
                        principalTable: "NewsGroupTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsContents_NewsGroups_NewsGroupId",
                        column: x => x.NewsGroupId,
                        principalTable: "NewsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SportRegisterResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SportRegisterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: false),
                    Competitions = table.Column<int>(type: "int", nullable: false),
                    Land = table.Column<int>(type: "int", nullable: false),
                    Violate = table.Column<bool>(type: "bit", nullable: false),
                    ViolateContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Achievement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Point = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    PointDraw = table.Column<int>(type: "int", nullable: false),
                    TotalSecond = table.Column<int>(type: "int", nullable: false),
                    TotalSecondPercent = table.Column<int>(type: "int", nullable: false),
                    NumberOfTurn = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportRegisterResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportRegisterResult_SportRegister_SportRegisterId",
                        column: x => x.SportRegisterId,
                        principalTable: "SportRegister",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsAttacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    NewsContentId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 40, nullable: false),
                    AttackDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDownload = table.Column<bool>(type: "bit", nullable: false),
                    HistoryPosition = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsAttacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsAttacks_NewsContents_NewsContentId",
                        column: x => x.NewsContentId,
                        principalTable: "NewsContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorityDetail_AuthorityId",
                table: "AuthorityDetail",
                column: "AuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsAttacks_NewsContentId",
                table: "NewsAttacks",
                column: "NewsContentId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsContents_NewsGroupId",
                table: "NewsContents",
                column: "NewsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsContents_NewsGroupTypeId",
                table: "NewsContents",
                column: "NewsGroupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsGroups_NewsGroupTypeId",
                table: "NewsGroups",
                column: "NewsGroupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SportRegister_StaffManagerId",
                table: "SportRegister",
                column: "StaffManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_SportRegisterResult_SportRegisterId",
                table: "SportRegisterResult",
                column: "SportRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAttackManager_StaffManagerId",
                table: "StaffAttackManager",
                column: "StaffManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectManagers_SubjectTypeManagerId",
                table: "SubjectManagers",
                column: "SubjectTypeManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutAttackManagers");

            migrationBuilder.DropTable(
                name: "AboutManagers");

            migrationBuilder.DropTable(
                name: "AuthorityDetail");

            migrationBuilder.DropTable(
                name: "AuthorityUsers");

            migrationBuilder.DropTable(
                name: "CommentManagers");

            migrationBuilder.DropTable(
                name: "ContactCustomerManagers");

            migrationBuilder.DropTable(
                name: "ContactManagers");

            migrationBuilder.DropTable(
                name: "ImageLibraryDetailManagers");

            migrationBuilder.DropTable(
                name: "ImageLibraryManagers");

            migrationBuilder.DropTable(
                name: "MenuGroups");

            migrationBuilder.DropTable(
                name: "MenuManager");

            migrationBuilder.DropTable(
                name: "NewsAttacks");

            migrationBuilder.DropTable(
                name: "NewsComments");

            migrationBuilder.DropTable(
                name: "NewsRecruitments");

            migrationBuilder.DropTable(
                name: "NewsSeoKeyWords");

            migrationBuilder.DropTable(
                name: "PermissionDefaults");

            migrationBuilder.DropTable(
                name: "ServerFiles");

            migrationBuilder.DropTable(
                name: "SportRegisterResult");

            migrationBuilder.DropTable(
                name: "StaffAttackManager");

            migrationBuilder.DropTable(
                name: "SubjectManagers");

            migrationBuilder.DropTable(
                name: "SystemLogs");

            migrationBuilder.DropTable(
                name: "TableDeleteManagers");

            migrationBuilder.DropTable(
                name: "Authorities");

            migrationBuilder.DropTable(
                name: "NewsContents");

            migrationBuilder.DropTable(
                name: "SportRegister");

            migrationBuilder.DropTable(
                name: "SubjectTypeManagers");

            migrationBuilder.DropTable(
                name: "NewsGroups");

            migrationBuilder.DropTable(
                name: "StaffManagers");

            migrationBuilder.DropTable(
                name: "NewsGroupTypes");
        }
    }
}
