using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITC.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class upNewContentMetatitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "NewsContents",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "NewsContents");
        }
    }
}
