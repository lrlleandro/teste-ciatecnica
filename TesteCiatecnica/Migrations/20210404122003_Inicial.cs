using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteCiatecnica.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ZipCode = table.Column<string>(type: "VARCHAR(8)", maxLength: 8, nullable: false),
                    Street = table.Column<string>(type: "VARCHAR(512)", nullable: false),
                    Number = table.Column<string>(type: "VARCHAR(8)", maxLength: 8, nullable: false),
                    Complement = table.Column<string>(type: "VARCHAR(512)", nullable: true),
                    Neighborhood = table.Column<string>(type: "VARCHAR(512)", nullable: false),
                    City = table.Column<string>(type: "VARCHAR(512)", nullable: false),
                    State = table.Column<string>(type: "CHAR(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerType = table.Column<int>(type: "int", nullable: false),
                    SSNorEIN = table.Column<string>(type: "VARCHAR(14)", maxLength: 14, nullable: false),
                    NameOrCompanyName = table.Column<string>(type: "VARCHAR(512)", nullable: false),
                    LastNameOrTradingName = table.Column<string>(type: "VARCHAR(512)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
