using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore_3._3.Migrations
{
    public partial class SetupDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Phones",
                type: "decimal(18,7)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Phones",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,7)");
        }
    }
}
