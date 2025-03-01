using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiProject_02_01_2024.Migrations
{
    /// <inheritdoc />
    public partial class INitttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CusTypeCode",
                table: "Customers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CusTypeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.Id);
                    table.UniqueConstraint("AK_CustomerTypes_CusTypeCode", x => x.CusTypeCode);
                });

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "Id", "CusTypeCode", "CustomerTypeName" },
                values: new object[,]
                {
                    { 1, "01", "Dealer" },
                    { 2, "02", "Retailer" },
                    { 3, "03", "Corporate" },
                    { 4, "04", "Export" },
                    { 5, "05", "Online" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CusTypeCode",
                table: "Customers",
                column: "CusTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_CustomerTypeName",
                table: "CustomerTypes",
                column: "CustomerTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_CusTypeCode",
                table: "CustomerTypes",
                column: "CusTypeCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerTypes_CusTypeCode",
                table: "Customers",
                column: "CusTypeCode",
                principalTable: "CustomerTypes",
                principalColumn: "CusTypeCode",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerTypes_CusTypeCode",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "CustomerTypes");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CusTypeCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CusTypeCode",
                table: "Customers");
        }
    }
}
