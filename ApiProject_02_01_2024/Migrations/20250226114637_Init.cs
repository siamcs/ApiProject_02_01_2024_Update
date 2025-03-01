using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProject_02_01_2024.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LMAC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(8)", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LMAC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.UniqueConstraint("AK_Customers_CustomerCode", x => x.CustomerCode);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    DesignationAutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignationCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DesignationName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LUser = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LIP = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LMAC = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.DesignationAutoId);
                });

            migrationBuilder.CreateTable(
                name: "HrmEmpDigitalSignatures",
                columns: table => new
                {
                    AutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignationAutoId = table.Column<int>(type: "int", nullable: false),
                    DigitalSignature = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImgType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImgSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrmEmpDigitalSignatures", x => x.AutoId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDeliveryAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CusDelAddCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CPPhoneNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDeliveryAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDeliveryAddresses_Customers_CustomerCode",
                        column: x => x.CustomerCode,
                        principalTable: "Customers",
                        principalColumn: "CustomerCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_BankCode",
                table: "Banks",
                column: "BankCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_BankName",
                table: "Banks",
                column: "BankName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDeliveryAddresses_CusDelAddCode",
                table: "CustomerDeliveryAddresses",
                column: "CusDelAddCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDeliveryAddresses_CustomerCode",
                table: "CustomerDeliveryAddresses",
                column: "CustomerCode");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerCode",
                table: "Customers",
                column: "CustomerCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "CustomerDeliveryAddresses");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "HrmEmpDigitalSignatures");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
