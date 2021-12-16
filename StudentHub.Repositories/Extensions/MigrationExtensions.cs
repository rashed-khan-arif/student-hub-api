using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentHub.Repositories.Extensions
{
    public static class MigrationExtensions
    {
        public static MigrationBuilder AddSeedData(this MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Users",
                new[]
                {
                    "Id",
                    "UserName",
                    "NormalizedUserName",
                    "Email",
                    "NormalizedEmail",
                    "EmailConfirmed",
                    "SecurityStamp",
                    "ConcurrencyStamp",
                    "PhoneNumberConfirmed",
                    "TwoFactorEnabled",
                    "LockoutEnabled",
                    "AccessFailedCount",
                    "FirstName",
                    "LastName",
                    "Sex",
                    "CreateDate"
                }, new object[]
                {
                    -1,
                    "admin@xeon.tech",
                    "ADMIN@XEON.TECH",
                    "admin@xeon.tech",
                    "ADMIN@XEON.TECH",
                    false,
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    false,
                    false,
                    0,
                    "Xeon", "Admin",
                    "M",
                    DateTime.UtcNow
                });

            migrationBuilder.InsertData(
                "OrgUnits",
                new[] { "Id", "Name", "CreatedBy" },
                new object[] { -1, "Head Office", -1 });

            return migrationBuilder;
        }
    }
}