using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITC.Infra.CrossCutting.Identity.Migrations
{
#pragma warning disable CS8981
    public partial class init : Migration
#pragma warning restore CS8981
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetModuleGroups",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetModuleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetModules",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Identity = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActivation = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GroupId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetPortals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Identity = table.Column<string>(maxLength: 450, nullable: false),
                    IsDepartmentOfEducation = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetPortals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Identity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsSuperAdmin = table.Column<bool>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(maxLength: 450, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    ModifyBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "getutcdate()"),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "getutcdate()"),
                    ManagementUnitId = table.Column<string>(nullable: true),
                    PortalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetFunctions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActivation = table.Column<bool>(nullable: false),
                    ModuleId = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetFunctions_AspNetModules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "AspNetModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetModuleRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ModuleId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetModuleRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetModuleRoles_AspNetModules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "AspNetModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetModuleDecentralizations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserTypeId = table.Column<string>(nullable: false),
                    ModuleId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetModuleDecentralizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetModuleDecentralizations_AspNetModules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "AspNetModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetModuleDecentralizations_AspNetUserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "AspNetUserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(maxLength: 255, nullable: true),
                    UserTypeId = table.Column<string>(nullable: true),
                    IsSuperAdmin = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreateBy = table.Column<string>(nullable: true),
                    ModifyBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "getutcdate()"),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "getutcdate()"),
                    Avatar = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Fax = table.Column<string>(maxLength: 20, nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    DistrictId = table.Column<int>(nullable: true),
                    WardId = table.Column<int>(nullable: true),
                    Website = table.Column<string>(maxLength: 100, nullable: true),
                    ManagementUnitId = table.Column<string>(maxLength: 40, nullable: true),
                    PortalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "AspNetUserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetFunctionDecentralizations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserTypeId = table.Column<string>(nullable: false),
                    FunctionId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetFunctionDecentralizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetFunctionDecentralizations_AspNetFunctions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "AspNetFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetFunctionDecentralizations_AspNetUserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "AspNetUserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetFunctionRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FunctionId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetFunctionRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetFunctionRoles_AspNetFunctions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "AspNetFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetFunctionDecentralizations_FunctionId",
                table: "AspNetFunctionDecentralizations",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetFunctionDecentralizations_UserTypeId",
                table: "AspNetFunctionDecentralizations",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetFunctionRoles_FunctionId",
                table: "AspNetFunctionRoles",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetFunctions_ModuleId",
                table: "AspNetFunctions",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetModuleDecentralizations_ModuleId",
                table: "AspNetModuleDecentralizations",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetModuleDecentralizations_UserTypeId",
                table: "AspNetModuleDecentralizations",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetModuleRoles_ModuleId",
                table: "AspNetModuleRoles",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTypeId",
                table: "AspNetUsers",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetFunctionDecentralizations");

            migrationBuilder.DropTable(
                name: "AspNetFunctionRoles");

            migrationBuilder.DropTable(
                name: "AspNetModuleDecentralizations");

            migrationBuilder.DropTable(
                name: "AspNetModuleGroups");

            migrationBuilder.DropTable(
                name: "AspNetModuleRoles");

            migrationBuilder.DropTable(
                name: "AspNetPortals");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetFunctions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetModules");

            migrationBuilder.DropTable(
                name: "AspNetUserTypes");
        }
    }
}
