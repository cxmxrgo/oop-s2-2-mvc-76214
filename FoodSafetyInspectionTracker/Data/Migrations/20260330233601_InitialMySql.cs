using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodSafetyInspectionTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMySql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Premises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Town = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    RiskRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premises", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inspections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PremisesId = table.Column<int>(type: "int", nullable: false),
                    InspectionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Outcome = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspections_Premises_PremisesId",
                        column: x => x.PremisesId,
                        principalTable: "Premises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FollowUps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InspectionId = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowUps_Inspections_InspectionId",
                        column: x => x.InspectionId,
                        principalTable: "Inspections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Premises",
                columns: new[] { "Id", "Address", "Name", "RiskRating", "Town" },
                values: new object[,]
                {
                    { 1, "10 Dock Lane", "Harbour Deli", 0, "Riverton" },
                    { 2, "22 Market St", "Town Bakery", 1, "Riverton" },
                    { 3, "8 Green Rd", "Sunrise Cafe", 2, "Riverton" },
                    { 4, "14 Hill Ave", "North Grill", 1, "Oakford" },
                    { 5, "2 River Walk", "Oakford Sushi", 2, "Oakford" },
                    { 6, "40 Cedar Dr", "Family Pizza", 0, "Oakford" },
                    { 7, "7 Lake View", "Meadow Bistro", 1, "Kingswell" },
                    { 8, "18 Mill Road", "Kingswell Tacos", 2, "Kingswell" },
                    { 9, "31 Station Rd", "Noodle House", 0, "Kingswell" },
                    { 10, "9 Orchard Way", "Garden Eatery", 1, "Riverton" },
                    { 11, "5 Quay St", "Riverside Pub", 2, "Oakford" },
                    { 12, "1 College Green", "Campus Canteen", 1, "Kingswell" }
                });

            migrationBuilder.InsertData(
                table: "Inspections",
                columns: new[] { "Id", "InspectionDate", "Notes", "Outcome", "PremisesId", "Score" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good hygiene.", 0, 1, 92 },
                    { 2, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minor improvements needed.", 0, 2, 71 },
                    { 3, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Storage issue.", 1, 3, 49 },
                    { 4, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compliant.", 0, 4, 80 },
                    { 5, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cross contamination controls weak.", 1, 5, 55 },
                    { 6, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solid process.", 0, 6, 88 },
                    { 7, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Temperature log missing.", 1, 7, 64 },
                    { 8, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adequate.", 0, 8, 76 },
                    { 9, new DateTime(2026, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Very clean.", 0, 9, 91 },
                    { 10, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training reminder.", 0, 10, 70 },
                    { 11, new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pest control action required.", 1, 11, 45 },
                    { 12, new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good standard.", 0, 12, 86 },
                    { 13, new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Routine check.", 0, 1, 90 },
                    { 14, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cleanliness issue.", 1, 2, 52 },
                    { 15, new DateTime(2026, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Record keeping poor.", 1, 3, 61 },
                    { 16, new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Improved.", 0, 4, 82 },
                    { 17, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Equipment sanitization required.", 1, 5, 59 },
                    { 18, new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Satisfactory.", 0, 6, 84 },
                    { 19, new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minor notes.", 0, 7, 79 },
                    { 20, new DateTime(2026, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cleaning schedule not followed.", 1, 8, 47 },
                    { 21, new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Excellent.", 0, 9, 94 },
                    { 22, new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Food storage issue.", 1, 10, 66 },
                    { 23, new DateTime(2026, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corrective actions in place.", 0, 11, 74 },
                    { 24, new DateTime(2026, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good condition.", 0, 12, 83 },
                    { 25, new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Staff hygiene retraining.", 1, 5, 69 }
                });

            migrationBuilder.InsertData(
                table: "FollowUps",
                columns: new[] { "Id", "ClosedDate", "DueDate", "InspectionId", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0 },
                    { 2, new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1 },
                    { 3, null, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 0 },
                    { 4, new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 1 },
                    { 5, null, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 0 },
                    { 6, null, new DateTime(2026, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 0 },
                    { 7, new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, 1 },
                    { 8, null, new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 0 },
                    { 9, null, new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, 0 },
                    { 10, null, new DateTime(2026, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_InspectionId",
                table: "FollowUps",
                column: "InspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_PremisesId",
                table: "Inspections",
                column: "PremisesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FollowUps");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Inspections");

            migrationBuilder.DropTable(
                name: "Premises");
        }
    }
}
