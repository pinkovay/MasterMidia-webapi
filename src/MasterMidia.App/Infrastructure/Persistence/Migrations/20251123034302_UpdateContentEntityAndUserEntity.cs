using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterMidia.App.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContentEntityAndUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Creators_CreatorId",
                table: "Contents");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Contents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Contents",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Contents_UserId",
                table: "Contents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Creators_CreatorId",
                table: "Contents",
                column: "CreatorId",
                principalTable: "Creators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Users_UserId",
                table: "Contents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Creators_CreatorId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Users_UserId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_UserId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Contents");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Contents",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Creators_CreatorId",
                table: "Contents",
                column: "CreatorId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
