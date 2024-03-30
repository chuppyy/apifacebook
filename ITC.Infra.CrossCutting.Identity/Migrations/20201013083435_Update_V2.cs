using Microsoft.EntityFrameworkCore.Migrations;

namespace ITC.Infra.CrossCutting.Identity.Migrations
{
    public partial class Update_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfficalProfileId",
                table: "AspNetUsers",
                nullable: true);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficalProfileId",
                table: "AspNetUsers");
        }
    }
}
