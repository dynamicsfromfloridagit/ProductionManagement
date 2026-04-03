using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductionManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedIsRecyclabletoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IspPrimaryPackageRecyclable",
                table: "Jobskus",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IspPrimaryPackageRecyclable",
                table: "Jobskus");
        }
    }
}
