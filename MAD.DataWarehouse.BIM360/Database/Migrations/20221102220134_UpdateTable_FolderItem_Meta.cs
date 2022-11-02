using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAD.DataWarehouse.BIM360.Database.Migrations
{
    public partial class UpdateTable_FolderItem_Meta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attributes_FileType",
                table: "FolderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attributes_MimeType",
                table: "FolderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attributes_StorageSize",
                table: "FolderItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attributes_VersionNumber",
                table: "FolderItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relationships_Parent_Meta_Link_Href",
                table: "FolderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relationships_Storage_Data_Id",
                table: "FolderItem",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relationships_Storage_Data_Type",
                table: "FolderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relationships_Storage_Meta_Link_Href",
                table: "FolderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relationships_Tip_Meta_Link_Href",
                table: "FolderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FolderItem_Relationships_Storage_Data_Id",
                table: "FolderItem",
                column: "Relationships_Storage_Data_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FolderItem_Relationships_Storage_Data_Id",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Attributes_FileType",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Attributes_MimeType",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Attributes_StorageSize",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Attributes_VersionNumber",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Relationships_Parent_Meta_Link_Href",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Relationships_Storage_Data_Id",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Relationships_Storage_Data_Type",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Relationships_Storage_Meta_Link_Href",
                table: "FolderItem");

            migrationBuilder.DropColumn(
                name: "Relationships_Tip_Meta_Link_Href",
                table: "FolderItem");
        }
    }
}
