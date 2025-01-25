using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLog_Guid_nullableForEliminations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeLogs_TableOfBooks_BookId",
                table: "ChangeLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "ChangeLogs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeLogs_TableOfBooks_BookId",
                table: "ChangeLogs",
                column: "BookId",
                principalTable: "TableOfBooks",
                principalColumn: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeLogs_TableOfBooks_BookId",
                table: "ChangeLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "ChangeLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeLogs_TableOfBooks_BookId",
                table: "ChangeLogs",
                column: "BookId",
                principalTable: "TableOfBooks",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
