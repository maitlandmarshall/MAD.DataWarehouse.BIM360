using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAD.DataWarehouse.BIM360.Database.Migrations
{
    public partial class AddTable_FolderItemDerivative : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FolderItemDerivative",
                columns: table => new
                {
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FolderItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RVTVersion = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "JSON_VALUE(Data, '$.derivatives[0].properties.\"Document Information\".RVTVersion')", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderItemDerivative", x => new { x.ProjectId, x.FolderItemId });
                    table.ForeignKey(
                        name: "FK_FolderItemDerivative_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolderItemDerivative");
        }
    }
}
