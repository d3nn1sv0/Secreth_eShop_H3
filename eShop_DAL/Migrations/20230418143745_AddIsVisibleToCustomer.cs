using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop_DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddIsVisibleToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Customers");
        }
    }
}
