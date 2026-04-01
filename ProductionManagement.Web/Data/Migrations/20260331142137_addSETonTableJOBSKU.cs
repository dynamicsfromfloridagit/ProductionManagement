using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductionManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSETonTableJOBSKU : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobskus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jobskunumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CasesPerUnit = table.Column<int>(type: "int", nullable: false),
                    Flavor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryPackageSize = table.Column<int>(type: "int", nullable: false),
                    PrimaryPackageMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryPackageSize = table.Column<int>(type: "int", nullable: false),
                    SecondaryPackageMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPungent = table.Column<bool>(type: "bit", nullable: false),
                    IsSensitive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobskus", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobskus");
        }
    }
}
