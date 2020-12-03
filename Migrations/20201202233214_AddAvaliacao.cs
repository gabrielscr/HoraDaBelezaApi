using Microsoft.EntityFrameworkCore.Migrations;

namespace HoraDaBelezaApi.Migrations
{
    public partial class AddAvaliacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Avaliacao",
                table: "Salao",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avaliacao",
                table: "Salao");
        }
    }
}
