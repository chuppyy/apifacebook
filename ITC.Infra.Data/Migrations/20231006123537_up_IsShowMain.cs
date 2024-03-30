using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITC.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class upIsShowMain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShowMain",
                table: "NewsGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShowMain",
                table: "NewsGroups");
        }
    }
}
