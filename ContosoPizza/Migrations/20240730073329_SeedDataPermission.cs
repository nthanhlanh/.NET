using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoPizza.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm dữ liệu vào bảng Permission
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[,]
                {
                { 1, "/api/Permission", 0 },
                { 2, "/api/Role", 0 },
                { 3, "/api/Group", 0 },
                { 4, "/api/Auth", 0 },
                { 5, "/api/UserGroup", 0 },
                { 6, "/api/GroupPermission", 0 }
                }
            );
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                { 1, "Admin" }
                }
            );


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
