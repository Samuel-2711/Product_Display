using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product_Display.Migrations
{
    /// <inheritdoc />
    public partial class AddTreasuryBP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Treasuries",
                columns: table => new
                {
                    SecurityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Maturity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenorDays = table.Column<int>(type: "int", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treasuries", x => x.SecurityId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Treasuries");
        }
    }
}
