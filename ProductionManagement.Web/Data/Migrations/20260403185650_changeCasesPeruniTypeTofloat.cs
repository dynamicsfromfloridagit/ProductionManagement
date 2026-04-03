using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductionManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeCasesPeruniTypeTofloat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Jobskunumber",
                table: "Jobskus",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<float>(
                name: "CasesPerUnit",
                table: "Jobskus",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Jobskunumber",
                table: "Jobskus",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<int>(
                name: "CasesPerUnit",
                table: "Jobskus",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
