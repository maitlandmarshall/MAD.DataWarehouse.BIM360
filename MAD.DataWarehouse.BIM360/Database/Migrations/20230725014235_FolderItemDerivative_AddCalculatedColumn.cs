using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAD.DataWarehouse.BIM360.Database.Migrations
{
    public partial class FolderItemDerivative_AddCalculatedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RVTVersion",
                table: "FolderItemDerivative",
                type: "nvarchar(max)",
                nullable: true,
                computedColumnSql: "JSON_VALUE(Data, '$.derivatives[0].properties.\"Document Information\".RVTVersion')",
                stored: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RVTVersion",
                table: "FolderItemDerivative");
        }
    }
}
