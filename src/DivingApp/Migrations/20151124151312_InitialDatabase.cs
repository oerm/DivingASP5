using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace DivingApp.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DicCert",
                columns: table => new
                {
                    CertID = table.Column<decimal>(isNullable: false),
                    CertName = table.Column<string>(isNullable: true),
                    Description = table.Column<string>(isNullable: true),
                    IsGeneral = table.Column<bool>(isNullable: false),
                    Level = table.Column<byte>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicCert", x => x.CertID);
                });
            migrationBuilder.CreateTable(
                name: "DicCountry",
                columns: table => new
                {
                    CountryKod = table.Column<decimal>(isNullable: false),
                    Description = table.Column<string>(isNullable: true),
                    Flag = table.Column<byte[]>(isNullable: true),
                    FullName = table.Column<string>(isNullable: true),
                    PhoneCode = table.Column<string>(isNullable: true),
                    ShortName = table.Column<string>(isNullable: true),
                    ShortName2 = table.Column<string>(isNullable: true),
                    Tmp1 = table.Column<int>(isNullable: true),
                    Tmp2 = table.Column<int>(isNullable: true),
                    Tmp3 = table.Column<int>(isNullable: true),
                    ValueEU = table.Column<string>(isNullable: true),
                    ValueRU = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicCountry", x => x.CountryKod);
                });
            migrationBuilder.CreateTable(
                name: "DicSuit",
                columns: table => new
                {
                    SuitID = table.Column<byte>(isNullable: false),
                    SuitValue = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicSuit", x => x.SuitID);
                });
            migrationBuilder.CreateTable(
                name: "DicTank",
                columns: table => new
                {
                    TankId = table.Column<byte>(isNullable: false),
                    TankValue = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicTank", x => x.TankId);
                });
            migrationBuilder.CreateTable(
                name: "DicWeightOk",
                columns: table => new
                {
                    WeightOkID = table.Column<byte>(isNullable: false),
                    WeightOkValue = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicWeightOk", x => x.WeightOkID);
                });
            migrationBuilder.CreateTable(
                name: "PhotoImg",
                columns: table => new
                {
                    PhotoID = table.Column<decimal>(isNullable: false),
                    PhotoVal = table.Column<byte[]>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoImg", x => x.PhotoID);
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(isNullable: false),
                    ConcurrencyStamp = table.Column<string>(isNullable: true),
                    Name = table.Column<string>(isNullable: true),
                    NormalizedName = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(isNullable: false),
                    AccessFailedCount = table.Column<int>(isNullable: false),
                    Adress = table.Column<string>(isNullable: true),
                    Birth = table.Column<DateTime>(isNullable: true),
                    City = table.Column<string>(isNullable: true),
                    ConcurrencyStamp = table.Column<string>(isNullable: true),
                    Country = table.Column<decimal>(isNullable: true),
                    DicCountryCountryKod = table.Column<decimal>(isNullable: true),
                    Email = table.Column<string>(isNullable: true),
                    EmailConfirmed = table.Column<bool>(isNullable: false),
                    FirstName = table.Column<string>(isNullable: true),
                    LastName = table.Column<string>(isNullable: true),
                    LockoutEnabled = table.Column<bool>(isNullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(isNullable: true),
                    NormalizedEmail = table.Column<string>(isNullable: true),
                    NormalizedUserName = table.Column<string>(isNullable: true),
                    PasswordHash = table.Column<string>(isNullable: true),
                    Phone = table.Column<string>(isNullable: true),
                    PhoneNumber = table.Column<string>(isNullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(isNullable: false),
                    Photo = table.Column<byte[]>(isNullable: true),
                    SecurityStamp = table.Column<string>(isNullable: true),
                    Status = table.Column<bool>(isNullable: false),
                    SurName = table.Column<string>(isNullable: true),
                    TwoFactorEnabled = table.Column<bool>(isNullable: false),
                    UserID = table.Column<int>(isNullable: false),
                    UserName = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_DicCountry_DicCountryCountryKod",
                        column: x => x.DicCountryCountryKod,
                        principalTable: "DicCountry",
                        principalColumn: "CountryKod");
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(isNullable: true),
                    ClaimValue = table.Column<string>(isNullable: true),
                    RoleId = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Cert",
                columns: table => new
                {
                    CertNumber = table.Column<string>(isNullable: false),
                    DateArchieve = table.Column<DateTime>(isNullable: true),
                    DicCertCertID = table.Column<decimal>(isNullable: true),
                    Issuer = table.Column<string>(isNullable: true),
                    Photo = table.Column<byte[]>(isNullable: true),
                    UserId = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cert", x => x.CertNumber);
                    table.ForeignKey(
                        name: "FK_Cert_DicCert_DicCertCertID",
                        column: x => x.DicCertCertID,
                        principalTable: "DicCert",
                        principalColumn: "CertID");
                    table.ForeignKey(
                        name: "FK_Cert_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Dive",
                columns: table => new
                {
                    DiveID = table.Column<decimal>(isNullable: false),
                    AirTemperature = table.Column<double>(isNullable: true),
                    Comments = table.Column<string>(isNullable: true),
                    CountriesCountryKod = table.Column<decimal>(isNullable: true),
                    Country = table.Column<decimal>(isNullable: true),
                    DiveDate = table.Column<DateTime>(isNullable: false),
                    DiveType = table.Column<byte>(isNullable: true),
                    DiveX = table.Column<double>(isNullable: true),
                    DiveY = table.Column<double>(isNullable: true),
                    FiveMetersMinutes = table.Column<byte>(isNullable: true),
                    Location = table.Column<string>(isNullable: true),
                    MaxDepth = table.Column<double>(isNullable: true),
                    Status = table.Column<bool>(isNullable: false),
                    SuitSuitID = table.Column<byte>(isNullable: true),
                    SuitType = table.Column<byte>(isNullable: true),
                    Tank = table.Column<byte>(isNullable: true),
                    TankEnd = table.Column<byte>(isNullable: true),
                    TankNameTankId = table.Column<byte>(isNullable: true),
                    TankStart = table.Column<byte>(isNullable: true),
                    TotalMinutes = table.Column<byte>(isNullable: true),
                    UpdDate = table.Column<DateTime>(isNullable: true),
                    UserId = table.Column<string>(isNullable: true),
                    Visibility = table.Column<byte>(isNullable: true),
                    WaterTemperature = table.Column<double>(isNullable: true),
                    Weight = table.Column<double>(isNullable: true),
                    WeightIsOk = table.Column<byte>(isNullable: true),
                    WeightOkWeightOkID = table.Column<byte>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dive", x => x.DiveID);
                    table.ForeignKey(
                        name: "FK_Dive_DicCountry_CountriesCountryKod",
                        column: x => x.CountriesCountryKod,
                        principalTable: "DicCountry",
                        principalColumn: "CountryKod");
                    table.ForeignKey(
                        name: "FK_Dive_DicSuit_SuitSuitID",
                        column: x => x.SuitSuitID,
                        principalTable: "DicSuit",
                        principalColumn: "SuitID");
                    table.ForeignKey(
                        name: "FK_Dive_DicTank_TankNameTankId",
                        column: x => x.TankNameTankId,
                        principalTable: "DicTank",
                        principalColumn: "TankId");
                    table.ForeignKey(
                        name: "FK_Dive_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Dive_DicWeightOk_WeightOkWeightOkID",
                        column: x => x.WeightOkWeightOkID,
                        principalTable: "DicWeightOk",
                        principalColumn: "WeightOkID");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(isNullable: true),
                    ClaimValue = table.Column<string>(isNullable: true),
                    UserId = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(isNullable: false),
                    ProviderKey = table.Column<string>(isNullable: false),
                    ProviderDisplayName = table.Column<string>(isNullable: true),
                    UserId = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(isNullable: false),
                    RoleId = table.Column<string>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoID = table.Column<decimal>(isNullable: false),
                    DiveID = table.Column<decimal>(isNullable: false),
                    PhotoComment = table.Column<string>(isNullable: true),
                    PhotoDate = table.Column<DateTime>(isNullable: false),
                    PhotoName = table.Column<string>(isNullable: true),
                    PhotoThumb = table.Column<byte[]>(isNullable: true),
                    Status = table.Column<bool>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoID);
                    table.ForeignKey(
                        name: "FK_Photos_Dive_DiveID",
                        column: x => x.DiveID,
                        principalTable: "Dive",
                        principalColumn: "DiveID");
                });
            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");
            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName");
            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Cert");
            migrationBuilder.DropTable("PhotoImg");
            migrationBuilder.DropTable("Photos");
            migrationBuilder.DropTable("AspNetRoleClaims");
            migrationBuilder.DropTable("AspNetUserClaims");
            migrationBuilder.DropTable("AspNetUserLogins");
            migrationBuilder.DropTable("AspNetUserRoles");
            migrationBuilder.DropTable("DicCert");
            migrationBuilder.DropTable("Dive");
            migrationBuilder.DropTable("AspNetRoles");
            migrationBuilder.DropTable("DicSuit");
            migrationBuilder.DropTable("DicTank");
            migrationBuilder.DropTable("AspNetUsers");
            migrationBuilder.DropTable("DicWeightOk");
            migrationBuilder.DropTable("DicCountry");
        }
    }
}
