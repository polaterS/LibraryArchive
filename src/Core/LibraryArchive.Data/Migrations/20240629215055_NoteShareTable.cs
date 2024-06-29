using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryArchive.Data.Migrations
{
    /// <inheritdoc />
    public partial class NoteShareTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteShare_AspNetUsers_SharedWithUserId",
                table: "NoteShare");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteShare_Notes_NoteId",
                table: "NoteShare");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteShare",
                table: "NoteShare");

            migrationBuilder.RenameTable(
                name: "NoteShare",
                newName: "NoteShares");

            migrationBuilder.RenameIndex(
                name: "IX_NoteShare_SharedWithUserId",
                table: "NoteShares",
                newName: "IX_NoteShares_SharedWithUserId");

            migrationBuilder.RenameIndex(
                name: "IX_NoteShare_NoteId",
                table: "NoteShares",
                newName: "IX_NoteShares_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteShares",
                table: "NoteShares",
                column: "NoteShareId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteShares_AspNetUsers_SharedWithUserId",
                table: "NoteShares",
                column: "SharedWithUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteShares_Notes_NoteId",
                table: "NoteShares",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteShares_AspNetUsers_SharedWithUserId",
                table: "NoteShares");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteShares_Notes_NoteId",
                table: "NoteShares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteShares",
                table: "NoteShares");

            migrationBuilder.RenameTable(
                name: "NoteShares",
                newName: "NoteShare");

            migrationBuilder.RenameIndex(
                name: "IX_NoteShares_SharedWithUserId",
                table: "NoteShare",
                newName: "IX_NoteShare_SharedWithUserId");

            migrationBuilder.RenameIndex(
                name: "IX_NoteShares_NoteId",
                table: "NoteShare",
                newName: "IX_NoteShare_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteShare",
                table: "NoteShare",
                column: "NoteShareId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteShare_AspNetUsers_SharedWithUserId",
                table: "NoteShare",
                column: "SharedWithUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteShare_Notes_NoteId",
                table: "NoteShare",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
