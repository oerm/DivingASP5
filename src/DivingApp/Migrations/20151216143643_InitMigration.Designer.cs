using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using DivingApp.Models;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace DivingApp.Migrations
{
    [DbContext(typeof(EntityContext))]
    partial class InitMigration
    {
        public override string Id
        {
            get { return "20151216143643_InitMigration"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

            modelBuilder.Entity("DivingApp.Models.DataModel.Cert", b =>
                {
                    b.Property<string>("CertNumber");

                    b.Property<DateTime?>("DateArchieve");

                    b.Property<decimal?>("DicCertCertID");

                    b.Property<string>("Issuer");

                    b.Property<byte[]>("Photo");

                    b.Property<string>("UserId");

                    b.Key("CertNumber");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.DicCert", b =>
                {
                    b.Property<decimal>("CertID");

                    b.Property<string>("CertName");

                    b.Property<string>("Description");

                    b.Property<bool>("IsGeneral");

                    b.Property<byte>("Level");

                    b.Key("CertID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.DicCountry", b =>
                {
                    b.Property<int>("CountryKod");

                    b.Property<string>("Description");

                    b.Property<byte[]>("Flag");

                    b.Property<string>("FullName");

                    b.Property<string>("PhoneCode");

                    b.Property<string>("ShortName");

                    b.Property<string>("ShortName2");

                    b.Property<int?>("Tmp1");

                    b.Property<int?>("Tmp2");

                    b.Property<int?>("Tmp3");

                    b.Property<string>("ValueEU");

                    b.Property<string>("ValueRU");

                    b.Key("CountryKod");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.DicSuit", b =>
                {
                    b.Property<byte>("SuitID");

                    b.Property<string>("SuitValue");

                    b.Key("SuitID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.DicTank", b =>
                {
                    b.Property<byte>("TankId");

                    b.Property<string>("TankValue");

                    b.Key("TankId");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.DicWeightOk", b =>
                {
                    b.Property<byte>("WeightOkID");

                    b.Property<string>("WeightOkValue");

                    b.Key("WeightOkID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.Dive", b =>
                {
                    b.Property<decimal>("DiveID");

                    b.Property<double?>("AirTemperature");

                    b.Property<string>("Comments");

                    b.Property<int?>("CountriesCountryKod");

                    b.Property<decimal?>("Country");

                    b.Property<DateTime>("DiveDate");

                    b.Property<byte?>("DiveType");

                    b.Property<double?>("DiveX");

                    b.Property<double?>("DiveY");

                    b.Property<byte?>("FiveMetersMinutes");

                    b.Property<string>("Location");

                    b.Property<double?>("MaxDepth");

                    b.Property<bool>("Status");

                    b.Property<byte?>("SuitSuitID");

                    b.Property<byte?>("SuitType");

                    b.Property<byte?>("Tank");

                    b.Property<byte?>("TankEnd");

                    b.Property<byte?>("TankNameTankId");

                    b.Property<byte?>("TankStart");

                    b.Property<byte?>("TotalMinutes");

                    b.Property<DateTime?>("UpdDate");

                    b.Property<string>("UserId");

                    b.Property<byte?>("Visibility");

                    b.Property<double?>("WaterTemperature");

                    b.Property<double?>("Weight");

                    b.Property<byte?>("WeightIsOk");

                    b.Property<byte?>("WeightOkWeightOkID");

                    b.Key("DiveID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.PhotoImg", b =>
                {
                    b.Property<decimal>("PhotoID");

                    b.Property<byte[]>("PhotoVal");

                    b.Key("PhotoID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.Photos", b =>
                {
                    b.Property<decimal>("PhotoID");

                    b.Property<decimal>("DiveID");

                    b.Property<string>("PhotoComment");

                    b.Property<DateTime>("PhotoDate");

                    b.Property<string>("PhotoName");

                    b.Property<byte[]>("PhotoThumb");

                    b.Property<bool>("Status");

                    b.Key("PhotoID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.User", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Adress");

                    b.Property<DateTime?>("Birth");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .ConcurrencyToken();

                    b.Property<int?>("Country");

                    b.Property<int?>("DicCountryCountryKod");

                    b.Property<string>("Email")
                        .Annotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<byte[]>("Photo");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .Annotation("MaxLength", 256);

                    b.Key("Id");

                    b.Index("NormalizedEmail")
                        .Annotation("Relational:Name", "EmailIndex");

                    b.Index("NormalizedUserName")
                        .Annotation("Relational:Name", "UserNameIndex");

                    b.Annotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .ConcurrencyToken();

                    b.Property<string>("Name")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .Annotation("MaxLength", 256);

                    b.Key("Id");

                    b.Index("NormalizedName")
                        .Annotation("Relational:Name", "RoleNameIndex");

                    b.Annotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.Key("Id");

                    b.Annotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId");

                    b.Key("LoginProvider", "ProviderKey");

                    b.Annotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Key("UserId", "RoleId");

                    b.Annotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.Cert", b =>
                {
                    b.Reference("DivingApp.Models.DataModel.DicCert")
                        .InverseCollection()
                        .ForeignKey("DicCertCertID");

                    b.Reference("DivingApp.Models.DataModel.User")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.Dive", b =>
                {
                    b.Reference("DivingApp.Models.DataModel.DicCountry")
                        .InverseCollection()
                        .ForeignKey("CountriesCountryKod");

                    b.Reference("DivingApp.Models.DataModel.DicSuit")
                        .InverseCollection()
                        .ForeignKey("SuitSuitID");

                    b.Reference("DivingApp.Models.DataModel.DicTank")
                        .InverseCollection()
                        .ForeignKey("TankNameTankId");

                    b.Reference("DivingApp.Models.DataModel.User")
                        .InverseCollection()
                        .ForeignKey("UserId");

                    b.Reference("DivingApp.Models.DataModel.DicWeightOk")
                        .InverseCollection()
                        .ForeignKey("WeightOkWeightOkID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.Photos", b =>
                {
                    b.Reference("DivingApp.Models.DataModel.Dive")
                        .InverseCollection()
                        .ForeignKey("DiveID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.User", b =>
                {
                    b.Reference("DivingApp.Models.DataModel.DicCountry")
                        .InverseCollection()
                        .ForeignKey("DicCountryCountryKod");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Reference("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .InverseCollection()
                        .ForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Reference("DivingApp.Models.DataModel.User")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Reference("DivingApp.Models.DataModel.User")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Reference("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .InverseCollection()
                        .ForeignKey("RoleId");

                    b.Reference("DivingApp.Models.DataModel.User")
                        .InverseCollection()
                        .ForeignKey("UserId");
                });
        }
    }
}
