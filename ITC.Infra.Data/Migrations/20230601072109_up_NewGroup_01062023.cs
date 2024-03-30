using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITC.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class upNewGroup01062023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "NewsGroups",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "NewsGroups",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domain",
                table: "NewsGroups");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "NewsGroups");
        }
    }
}
