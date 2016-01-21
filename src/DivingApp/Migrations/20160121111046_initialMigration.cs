using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace DivingApp.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_User_DicCountry_DicCountryCountryKod", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "Country", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "DicCountryCountryKod", table: "AspNetUsers");
            migrationBuilder.AddColumn<int>(
                name: "DicCountryId",
                table: "AspNetUsers",
                isNullable: true);
            migrationBuilder.AlterColumn<long>(
                name: "DiveID",
                table: "Photos",
                isNullable: false);
            migrationBuilder.AlterColumn<long>(
                name: "PhotoID",
                table: "Photos",
                isNullable: false);
            migrationBuilder.AlterColumn<long>(
                name: "PhotoID",
                table: "PhotoImg",
                isNullable: false);
            migrationBuilder.AlterColumn<long>(
                name: "Country",
                table: "Dive",
                isNullable: true);
            migrationBuilder.AlterColumn<long>(
                name: "DiveID",
                table: "Dive",
                isNullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);
            migrationBuilder.AlterColumn<long>(
                name: "CertID",
                table: "DicCert",
                isNullable: false);
            migrationBuilder.AlterColumn<long>(
                name: "DicCertCertID",
                table: "Cert",
                isNullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_User_DicCountry_DicCountryId",
                table: "AspNetUsers",
                column: "DicCountryId",
                principalTable: "DicCountry",
                principalColumn: "CountryKod");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_User_DicCountry_DicCountryId", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "DicCountryId", table: "AspNetUsers");
            migrationBuilder.AddColumn<int>(
                name: "Country",
                table: "AspNetUsers",
                isNullable: true);
            migrationBuilder.AddColumn<int>(
                name: "DicCountryCountryKod",
                table: "AspNetUsers",
                isNullable: true);
            migrationBuilder.AlterColumn<decimal>(
                name: "DiveID",
                table: "Photos",
                isNullable: false);
            migrationBuilder.AlterColumn<decimal>(
                name: "PhotoID",
                table: "Photos",
                isNullable: false);
            migrationBuilder.AlterColumn<decimal>(
                name: "PhotoID",
                table: "PhotoImg",
                isNullable: false);
            migrationBuilder.AlterColumn<decimal>(
                name: "Country",
                table: "Dive",
                isNullable: true);
            migrationBuilder.AlterColumn<decimal>(
                name: "DiveID",
                table: "Dive",
                isNullable: false);
            migrationBuilder.AlterColumn<decimal>(
                name: "CertID",
                table: "DicCert",
                isNullable: false);
            migrationBuilder.AlterColumn<decimal>(
                name: "DicCertCertID",
                table: "Cert",
                isNullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_User_DicCountry_DicCountryCountryKod",
                table: "AspNetUsers",
                column: "DicCountryCountryKod",
                principalTable: "DicCountry",
                principalColumn: "CountryKod");
        }
    }
}
