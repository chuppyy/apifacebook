using Microsoft.EntityFrameworkCore.Migrations;

namespace ITC.Infra.CrossCutting.Identity.Migrations
{
    public partial class Update_ApplicationDbContextV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AspNetModules");

            migrationBuilder.AddColumn<string>(
                name: "ModuleGroupId",
                table: "AspNetModules",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetModules_ModuleGroupId",
                table: "AspNetModules",
                column: "ModuleGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetModules_AspNetModuleGroups_ModuleGroupId",
                table: "AspNetModules",
                column: "ModuleGroupId",
                principalTable: "AspNetModuleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetModules_AspNetModuleGroups_ModuleGroupId",
                table: "AspNetModules");

            migrationBuilder.DropIndex(
                name: "IX_AspNetModules_ModuleGroupId",
                table: "AspNetModules");

            migrationBuilder.DropColumn(
                name: "ModuleGroupId",
                table: "AspNetModules");

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "AspNetModules",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
