using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterMidia.App.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Contents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StorageUrl",
                table: "Contents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "StorageUrl",
                table: "Contents");
        }
    }
}
