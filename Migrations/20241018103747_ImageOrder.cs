﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BouillonChanvre.Migrations
{
    /// <inheritdoc />
    public partial class ImageOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "ProductImages");
        }
    }
}
