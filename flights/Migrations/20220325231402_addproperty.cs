using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flights.Migrations
{
    public partial class addproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "card_number",
                table: "AspNetUsers",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "gender",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "passport",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRole", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectRole");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "User_Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "card_number",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "passport",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "AspNetUsers");
        }
    }
}
