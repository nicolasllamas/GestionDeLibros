using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class Initialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogErrors",
                columns: table => new
                {
                    LogErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ErrorMessage = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogErrors", x => x.LogErrorId);
                });

            migrationBuilder.CreateTable(
                name: "TableOfBooks",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false),
                    Author = table.Column<string>(type: "varchar(255)", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableOfBooks", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "ChangeLogs",
                columns: table => new
                {
                    ChangeLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldName = table.Column<string>(type: "varchar(255)", nullable: false),
                    OldValue = table.Column<string>(type: "varchar(max)", nullable: false),
                    NewValue = table.Column<string>(type: "varchar(max)", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLogs", x => x.ChangeLogId);
                    table.ForeignKey(
                        name: "FK_ChangeLogs_TableOfBooks_BookId",
                        column: x => x.BookId,
                        principalTable: "TableOfBooks",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLogs_BookId",
                table: "ChangeLogs",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeLogs");

            migrationBuilder.DropTable(
                name: "LogErrors");

            migrationBuilder.DropTable(
                name: "TableOfBooks");
        }
    }
}
