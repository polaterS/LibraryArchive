﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryArchive.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDefaultToAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Addresses");
        }
    }
}
