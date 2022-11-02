using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAD.DataWarehouse.BIM360.Database.Migrations
{
    public partial class UpdateTable_FolderItem_Item : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Relationships_Item_Data_Id",
                table: "FolderItem",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relationships_Item_Data_Type",
                table: "FolderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relationships_Item_Meta_Link_Href",
                table: "FolderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FolderItem_Relationships_Item_Data_Id",
                table: "FolderItem",
                column: "Relationships_Item_Data_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FolderItem_Relationships_Item_Data_Id",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Relationships_Item_Data_Id",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Relationships_Item_Data_Type",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Relationships_Item_Meta_Link_Href",
                table: "FolderItem");
        }
    }
}
