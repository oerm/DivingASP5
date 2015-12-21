using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace DivingApp.Migrations
{
    public partial class DicCountryMigration : Migration
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
            migrationBuilder.AddForeignKey(
                name: "FK_User_DicCountry_DicCountryCountryKod",
                table: "AspNetUsers",
                column: "DicCountryCountryKod",
                principalTable: "DicCountry",
                principalColumn: "CountryKod");
        }
    }
}
