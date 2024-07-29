using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoPizza.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        private static string _USERID = Guid.NewGuid().ToString();
        private static string _ROLEADMINID = Guid.NewGuid().ToString();
        private static string _ROLEUSERID = Guid.NewGuid().ToString();
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<IdentityUser>();

            // Thêm dữ liệu vào bảng AspNetRoles
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                { _ROLEADMINID, "Admin", "ADMIN", Guid.NewGuid().ToString() },
                { _ROLEUSERID, "User", "USER", Guid.NewGuid().ToString() }
                }
            );
            // Thêm dữ liệu vào bảng ApplicationUser
            var user = new IdentityUser
            {
                Id = _USERID,
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            user.PasswordHash = hasher.HashPassword(user, "CustomPassword123!");
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount" },
                values: new object[] { user.Id, user.UserName, user.NormalizedUserName, user.Email, user.NormalizedEmail, user.EmailConfirmed, user.PasswordHash, user.SecurityStamp, user.ConcurrencyStamp, false, false, true, 0 }
            );
            // Thêm dữ liệu vào bảng AspNetUserRoles
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { _USERID, _ROLEADMINID }
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "AspNetUserRoles", keyColumns: new[] { "UserId", "RoleId" }, keyValues: new object[] { _USERID, _ROLEADMINID });
            migrationBuilder.DeleteData(table: "AspNetUsers", keyColumn: "Id", keyValue: _USERID);
            migrationBuilder.DeleteData(table: "AspNetRoles", keyColumn: "Id", keyValue: _ROLEADMINID);
            migrationBuilder.DeleteData(table: "AspNetRoles", keyColumn: "Id", keyValue: _ROLEUSERID);

        }
    }
}
