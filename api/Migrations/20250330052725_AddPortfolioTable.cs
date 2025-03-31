using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddPortfolioTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Shares",
                table: "Portfolios",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Portfolios",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_AppUserId1",
                table: "Portfolios",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_AspNetUsers_AppUserId1",
                table: "Portfolios",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_AspNetUsers_AppUserId1",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_AppUserId1",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Portfolios");

            migrationBuilder.AlterColumn<decimal>(
                name: "Shares",
                table: "Portfolios",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");
        }
    }
}
