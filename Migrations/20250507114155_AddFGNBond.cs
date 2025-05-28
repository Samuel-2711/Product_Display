using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product_Display.Migrations
{
    /// <inheritdoc />
    public partial class AddFGNBond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FGNBonds",
                columns: table => new
                {
                    ISIN = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Maturity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YearsToMaturity = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    YieldToMaturity = table.Column<double>(type: "float", nullable: false),
                    Coupon = table.Column<double>(type: "float", nullable: false),
                    Security = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FGNBonds", x => x.ISIN);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FGNBonds");
        }
    }
}
