﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop_DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddIsVisibleToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Products");
        }
    }
}
