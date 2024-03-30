using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITC.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class addNewsConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 40, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenGit = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OwnerGit = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProjectDefaultGit = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TeamId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TokenVercel = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Host = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsVercels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 40, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsVercels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsConfigs");

            migrationBuilder.DropTable(
                name: "NewsVercels");
        }
    }
}
