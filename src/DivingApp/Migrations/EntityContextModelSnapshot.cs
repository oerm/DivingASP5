using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using DivingApp.Models;

namespace DivingApp.Migrations
{
    [DbContext(typeof(EntityContext))]
    partial class EntityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DivingApp.Models.DataModel.Cert", b =>
                {
                    b.Property<string>("CertNumber");

                    b.Property<DateTime?>("DateArchieve");

                    b.Property<long?>("DicCertCertID");

                    b.Property<string>("Issuer");

                    b.Property<byte[]>("Photo");

                    b.Property<string>("UserId");

                    b.HasKey("CertNumber");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.DicCert", b =>
                {
                    b.Property<long>("CertID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CertName");

                    b.Property<string>("Description");

                    b.Property<bool>("IsGeneral");

                    b.Property<byte>("Level");

                    b.HasKey("CertID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.DicCountry", b =>
                {
                    b.Property<int>("CountryKod")
                        .ValueGeneratedOnAdd();

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

                    b.HasKey("CountryKod");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.DicDiveTime", b =>
                {
                    b.Property<byte>("TimeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TimeValue");

                    b.HasKey("TimeId");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.DicSuit", b =>
                {
                    b.Property<byte>("SuitID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SuitValue");

                    b.HasKey("SuitID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.DicTank", b =>
                {
                    b.Property<byte>("TankId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TankValue");

                    b.HasKey("TankId");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.DicWeightOk", b =>
                {
                    b.Property<byte>("WeightOkID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("WeightOkValue");

                    b.HasKey("WeightOkID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.Dive", b =>
                {
                    b.Property<long>("DiveID")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("AirTemperature");

                    b.Property<string>("Comments");

                    b.Property<int?>("CountriesCountryKod");

                    b.Property<long?>("Country");

                    b.Property<byte?>("DicDiveTimeTimeId");

                    b.Property<DateTime>("DiveDate");

                    b.Property<byte?>("DiveTime");

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

                    b.HasKey("DiveID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.PhotoImg", b =>
                {
                    b.Property<long>("PhotoID")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("PhotoVal");

                    b.HasKey("PhotoID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.Photos", b =>
                {
                    b.Property<long>("PhotoID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("DiveID");

                    b.Property<string>("PhotoComment");

                    b.Property<DateTime>("PhotoDate");

                    b.Property<string>("PhotoName");

                    b.Property<byte[]>("PhotoThumb");

                    b.Property<bool>("Status");

                    b.HasKey("PhotoID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.User", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Adress");

                    b.Property<DateTime?>("Birth");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int?>("DicCountryId");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<byte[]>("Photo");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.Cert", b =>
                {
                    b.HasOne("DivingApp.Models.DataModel.DicCert")
                        .WithMany()
                        .HasForeignKey("DicCertCertID");

                    b.HasOne("DivingApp.Models.DataModel.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.Dive", b =>
                {
                    b.HasOne("DivingApp.Models.DataModel.DicCountry")
                        .WithMany()
                        .HasForeignKey("CountriesCountryKod");

                    b.HasOne("DivingApp.Models.DataModel.DicDiveTime")
                        .WithMany()
                        .HasForeignKey("DicDiveTimeTimeId");

                    b.HasOne("DivingApp.Models.DataModel.DicSuit")
                        .WithMany()
                        .HasForeignKey("SuitSuitID");

                    b.HasOne("DivingApp.Models.DataModel.DicTank")
                        .WithMany()
                        .HasForeignKey("TankNameTankId");

                    b.HasOne("DivingApp.Models.DataModel.User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("DivingApp.Models.DataModel.DicWeightOk")
                        .WithMany()
                        .HasForeignKey("WeightOkWeightOkID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.Photos", b =>
                {
                    b.HasOne("DivingApp.Models.DataModel.Dive")
                        .WithMany()
                        .HasForeignKey("DiveID");
                });

            modelBuilder.Entity("DivingApp.Models.DataModel.User", b =>
                {
                    b.HasOne("DivingApp.Models.DataModel.DicCountry")
                        .WithMany()
                        .HasForeignKey("DicCountryId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DivingApp.Models.DataModel.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DivingApp.Models.DataModel.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("DivingApp.Models.DataModel.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
