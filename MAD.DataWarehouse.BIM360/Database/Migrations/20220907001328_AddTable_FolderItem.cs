using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAD.DataWarehouse.BIM360.Database.Migrations
{
    public partial class AddTable_FolderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FolderItem",
                columns: table => new
                {
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_CreateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Attributes_CreateUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_CreateUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_LastModifiedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Attributes_LastModifiedUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_LastModifiedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_LastModifiedTimeRollup = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Attributes_ObjectCount = table.Column<long>(type: "bigint", nullable: false),
                    Attributes_Hidden = table.Column<bool>(type: "bit", nullable: false),
                    Attributes_Extension_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_Extension_Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_Extension_Data_VisibleTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_Extension_Data_Actions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_Extension_Data_AllowedTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes_Extension_Data_NamingStandardIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relationships_Parent_Data_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relationships_Parent_Data_Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderItem", x => new { x.Id, x.ProjectId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_FolderItem_Relationships_Parent_Data_Id",
                table: "FolderItem",
                column: "Relationships_Parent_Data_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolderItem");
        }
    }
}
