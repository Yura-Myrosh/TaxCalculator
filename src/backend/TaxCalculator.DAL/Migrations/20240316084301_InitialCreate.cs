using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaxCalculator.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxBands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LowerBound = table.Column<int>(type: "int", nullable: false),
                    UpperBound = table.Column<int>(type: "int", nullable: false),
                    RateInPercents = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxBands", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TaxBands",
                columns: new[] { "Id", "LowerBound", "RateInPercents", "UpperBound" },
                values: new object[,]
                {
                    { new Guid("d0606ca2-2337-436c-80e9-ced465be3a8c"), 5000, 20, 20000 },
                    { new Guid("d71f3efe-f490-4fdd-8bbb-83c319654874"), 20000, 40, 2147483647 },
                    { new Guid("f7474843-f83c-432a-920b-3149e32a8847"), 0, 0, 5000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxBands");
        }
    }
}
