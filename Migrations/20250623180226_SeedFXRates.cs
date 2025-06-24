using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Product_Display.Migrations
{
    /// <inheritdoc />
    public partial class SeedFXRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FXRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsUpwardTrend = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FXRates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FXRates",
                columns: new[] { "Id", "BuyRate", "CurrencyName", "IsUpwardTrend", "SellRate", "Symbol" },
                values: new object[,]
                {
                    { 1, 2010.52m, "US Dollar", true, 2090.34m, "USD" },
                    { 2, 1758.53m, "Pound Sterling", true, 1812.43m, "GBP" },
                    { 3, 1600m, "Euro", true, 1660m, "EUR" },
                    { 4, 1116.19m, "Canadian Dollar", true, 1174.12m, "CAD" },
                    { 5, 82.87m, "S/African Rand", true, 87.14m, "ZAR" },
                    { 6, 421.14m, "UAE Dirham", true, 443.48m, "AED" },
                    { 7, 0m, "Chinese Yuan", true, 0m, "CNY" },
                    { 8, 0m, "West African", true, 0m, "XAF" },
                    { 9, 0m, "Japanese Yen", true, 0m, "JPY" },
                    { 10, 0m, "Indian Rupee", true, 0m, "RUP" },
                    { 11, 0m, "Hong Kong Dollar", true, 0m, "HKD" },
                    { 12, 0m, "Singaporean Dollar", true, 0m, "SGD" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FXRates");
        }
    }
}
