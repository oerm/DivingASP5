using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace DivingApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DicCert",
                columns: table => new
                {
                    CertID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CertName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsGeneral = table.Column<bool>(nullable: false),
                    Level = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicCert", x => x.CertID);
                });
            migrationBuilder.CreateTable(
                name: "DicCountry",
                columns: table => new
                {
                    CountryKod = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Flag = table.Column<byte[]>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    PhoneCode = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    ShortName2 = table.Column<string>(nullable: true),
                    Tmp1 = table.Column<int>(nullable: true),
                    Tmp2 = table.Column<int>(nullable: true),
                    Tmp3 = table.Column<int>(nullable: true),
                    ValueEU = table.Column<string>(nullable: true),
                    ValueRU = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicCountry", x => x.CountryKod);
                });
            migrationBuilder.CreateTable(
                name: "DicDiveTime",
                columns: table => new
                {
                    TimeId = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicDiveTime", x => x.TimeId);
                });
            migrationBuilder.CreateTable(
                name: "DicSuit",
                columns: table => new
                {
                    SuitID = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SuitValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicSuit", x => x.SuitID);
                });
            migrationBuilder.CreateTable(
                name: "DicTank",
                columns: table => new
                {
                    TankId = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TankValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicTank", x => x.TankId);
                });
            migrationBuilder.CreateTable(
                name: "DicWeightOk",
                columns: table => new
                {
                    WeightOkID = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WeightOkValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicWeightOk", x => x.WeightOkID);
                });
            migrationBuilder.CreateTable(
                name: "PhotoImg",
                columns: table => new
                {
                    PhotoID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhotoVal = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoImg", x => x.PhotoID);
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Adress = table.Column<string>(nullable: true),
                    Birth = table.Column<DateTime>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    DicCountryId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    Photo = table.Column<byte[]>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_DicCountry_DicCountryId",
                        column: x => x.DicCountryId,
                        principalTable: "DicCountry",
                        principalColumn: "CountryKod",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Cert",
                columns: table => new
                {
                    CertNumber = table.Column<string>(nullable: false),
                    DateArchieve = table.Column<DateTime>(nullable: true),
                    DicCertCertID = table.Column<long>(nullable: true),
                    Issuer = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cert", x => x.CertNumber);
                    table.ForeignKey(
                        name: "FK_Cert_DicCert_DicCertCertID",
                        column: x => x.DicCertCertID,
                        principalTable: "DicCert",
                        principalColumn: "CertID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cert_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Dive",
                columns: table => new
                {
                    DiveID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AirTemperature = table.Column<double>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    CountriesCountryKod = table.Column<int>(nullable: true),
                    Country = table.Column<long>(nullable: true),
                    DicDiveTimeTimeId = table.Column<byte>(nullable: true),
                    DiveDate = table.Column<DateTime>(nullable: false),
                    DiveTime = table.Column<byte>(nullable: true),
                    DiveX = table.Column<double>(nullable: true),
                    DiveY = table.Column<double>(nullable: true),
                    FiveMetersMinutes = table.Column<byte>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    MaxDepth = table.Column<double>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    SuitSuitID = table.Column<byte>(nullable: true),
                    SuitType = table.Column<byte>(nullable: true),
                    Tank = table.Column<byte>(nullable: true),
                    TankEnd = table.Column<byte>(nullable: true),
                    TankNameTankId = table.Column<byte>(nullable: true),
                    TankStart = table.Column<byte>(nullable: true),
                    TotalMinutes = table.Column<byte>(nullable: true),
                    UpdDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Visibility = table.Column<byte>(nullable: true),
                    WaterTemperature = table.Column<double>(nullable: true),
                    Weight = table.Column<double>(nullable: true),
                    WeightIsOk = table.Column<byte>(nullable: true),
                    WeightOkWeightOkID = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dive", x => x.DiveID);
                    table.ForeignKey(
                        name: "FK_Dive_DicCountry_CountriesCountryKod",
                        column: x => x.CountriesCountryKod,
                        principalTable: "DicCountry",
                        principalColumn: "CountryKod",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dive_DicDiveTime_DicDiveTimeTimeId",
                        column: x => x.DicDiveTimeTimeId,
                        principalTable: "DicDiveTime",
                        principalColumn: "TimeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dive_DicSuit_SuitSuitID",
                        column: x => x.SuitSuitID,
                        principalTable: "DicSuit",
                        principalColumn: "SuitID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dive_DicTank_TankNameTankId",
                        column: x => x.TankNameTankId,
                        principalTable: "DicTank",
                        principalColumn: "TankId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dive_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dive_DicWeightOk_WeightOkWeightOkID",
                        column: x => x.WeightOkWeightOkID,
                        principalTable: "DicWeightOk",
                        principalColumn: "WeightOkID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiveID = table.Column<long>(nullable: false),
                    PhotoComment = table.Column<string>(nullable: true),
                    PhotoDate = table.Column<DateTime>(nullable: false),
                    PhotoName = table.Column<string>(nullable: true),
                    PhotoThumb = table.Column<byte[]>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoID);
                    table.ForeignKey(
                        name: "FK_Photos_Dive_DiveID",
                        column: x => x.DiveID,
                        principalTable: "Dive",
                        principalColumn: "DiveID",
                        onDelete: ReferentialAction.Cascade);
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
            migrationBuilder.DropTable("DicDiveTime");
            migrationBuilder.DropTable("DicSuit");
            migrationBuilder.DropTable("DicTank");
            migrationBuilder.DropTable("AspNetUsers");
            migrationBuilder.DropTable("DicWeightOk");
            migrationBuilder.DropTable("DicCountry");
        }
    }
}
