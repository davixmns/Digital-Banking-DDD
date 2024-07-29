using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalBankDDD.Infra.Migrations
{
    /// <inheritdoc />
    public partial class transaction_hash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Transactions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Transactions");
        }
    }
}
