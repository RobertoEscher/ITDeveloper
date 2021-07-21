using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cooperchip.ITDeveloper.Mvc.Migrations
{
    public partial class AddUserAndRoleWithExtensionMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "762818A5-EC7E-4CFF-A74E-B9103E92F021", "01e5ff13-58f2-4add-b899-6d3255d3dc0c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apelido", "ConcurrencyStamp", "DataNascimento", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NomeCompleto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0D39D367-3E94-4AD8-A49F-519B2A3F358D", 0, "rfranca", "eac20b15-b664-4952-bf3e-99caebcfa474", new DateTime(2021, 7, 21, 15, 30, 18, 785, DateTimeKind.Local).AddTicks(3629), "rfranca@gmail.com", true, false, null, "Roberto França", "RFRANCA@GMAIL.COM", "RFRANCA@GMAIL.COM", "AQAAAAEAACcQAAAAEBB+qzfr9H8U5FeYSkTFLSJWkT/f4mvjDryhQLjo+FWbRuA3JojG4xNohyXCDTk8kA==", null, false, "759cb784-44e5-42d2-a1b8-e67eaf2c536b", false, "rfranca@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "0D39D367-3E94-4AD8-A49F-519B2A3F358D", "762818A5-EC7E-4CFF-A74E-B9103E92F021" });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
