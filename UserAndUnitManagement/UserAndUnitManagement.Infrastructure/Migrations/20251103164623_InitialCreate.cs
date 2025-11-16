using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserAndUnitManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OptInToDirectory = table.Column<bool>(type: "bit", nullable: false),
                    ShowEmailInDirectory = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUnits",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnassignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUnits", x => new { x.UserId, x.UnitId });
                    table.ForeignKey(
                        name: "FK_UserUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUnits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Address", "City", "CreatedDate", "LastModifiedDate", "Name", "State", "Status", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcde0"), "123 Main St", "Anytown", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Unit 101", "CA", 0, "12345" },
                    { new Guid("fedcba09-8765-4321-0987-654321fedcb0"), "456 Oak Ave", "Someville", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Unit 202", "NY", 0, "54321" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsActive", "LastModifiedDate", "LastName", "OptInToDirectory", "PasswordHash", "Role", "Salt", "ShowEmailInDirectory" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "superadmin@example.com", "Super", true, null, "Admin", true, "db9219e7a968d9f7453af0c2c8116e99b00e154893f5285874c1f17ff88f8ae3", 0, "c02fb818-2f56-4508-b1ed-718aa121f16b", true },
                    { new Guid("b2c3d4e5-f6a1-0987-5432-10fedcba9876"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "propertymanager@example.com", "Property", true, null, "Manager", true, "db9219e7a968d9f7453af0c2c8116e99b00e154893f5285874c1f17ff88f8ae3", 1, "c02fb818-2f56-4508-b1ed-718aa121f16b", true },
                    { new Guid("c3d4e5f6-a1b2-7890-1234-567890abcdef"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "securityguard@example.com", "Security", true, null, "Guard", true, "db9219e7a968d9f7453af0c2c8116e99b00e154893f5285874c1f17ff88f8ae3", 2, "c02fb818-2f56-4508-b1ed-718aa121f16b", true },
                    { new Guid("d4e5f6a1-b2c3-0987-5432-10fedcba9876"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "owner@example.com", "Owner", true, null, "User", true, "db9219e7a968d9f7453af0c2c8116e99b00e154893f5285874c1f17ff88f8ae3", 3, "c02fb818-2f56-4508-b1ed-718aa121f16b", true },
                    { new Guid("e5f6a1b2-c3d4-7890-1234-567890abcdef"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tenant@example.com", "Tenant", true, null, "User", true, "db9219e7a968d9f7453af0c2c8116e99b00e154893f5285874c1f17ff88f8ae3", 4, "c02fb818-2f56-4508-b1ed-718aa121f16b", true }
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "Breed", "Name", "PhotoUrl", "RegistrationDate", "Species", "UserId" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcde1"), "Golden Retriever", "Buddy", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dog", new Guid("d4e5f6a1-b2c3-0987-5432-10fedcba9876") },
                    { new Guid("fedcba09-8765-4321-0987-654321fedcb1"), "Siamese", "Whiskers", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cat", new Guid("d4e5f6a1-b2c3-0987-5432-10fedcba9876") }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Color", "ExpirationDate", "Make", "Model", "PlateNumber", "RegistrationDate", "UserId", "Year" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcde2"), "Silver", null, "Toyota", "Camry", "123-ABC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e5f6a1b2-c3d4-7890-1234-567890abcdef"), 0 },
                    { new Guid("fedcba09-8765-4321-0987-654321fedcb2"), "Black", null, "Honda", "Civic", "456-DEF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e5f6a1b2-c3d4-7890-1234-567890abcdef"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnits_UnitId",
                table: "UserUnits",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "UserUnits");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
