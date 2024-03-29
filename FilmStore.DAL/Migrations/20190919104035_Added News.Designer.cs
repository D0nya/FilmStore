﻿// <auto-generated />
using System;
using FilmStore.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FilmStore.DAL.Migrations
{
    [DbContext(typeof(FilmStoreContext))]
    [Migration("20190919104035_Added News")]
    partial class AddedNews
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FilmStore.DAL.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDay");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<string>("UserRef");

                    b.HasKey("Id");

                    b.HasIndex("UserRef")
                        .IsUnique()
                        .HasFilter("[UserRef] IS NOT NULL");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int?>("ProducerId");

                    b.Property<int>("QuantityInStock");

                    b.Property<float>("Rate");

                    b.Property<string>("Year");

                    b.HasKey("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.FilmCountry", b =>
                {
                    b.Property<int>("CountryId");

                    b.Property<int>("FilmId");

                    b.HasKey("CountryId", "FilmId");

                    b.HasIndex("FilmId");

                    b.ToTable("FilmCountry");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.FilmGenre", b =>
                {
                    b.Property<int>("GenreId");

                    b.Property<int>("FilmId");

                    b.HasKey("GenreId", "FilmId");

                    b.HasIndex("FilmId");

                    b.ToTable("FilmGenre");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.FilmPurchase", b =>
                {
                    b.Property<int>("FilmId");

                    b.Property<int>("PurchaseId");

                    b.Property<int>("Quantity");

                    b.HasKey("FilmId", "PurchaseId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("FilmPurchase");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body");

                    b.Property<string>("Header");

                    b.Property<string>("ImagePath");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.Producer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.Customer", b =>
                {
                    b.HasOne("FilmStore.DAL.Entities.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("FilmStore.DAL.Entities.Customer", "UserRef");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.Film", b =>
                {
                    b.HasOne("FilmStore.DAL.Entities.Producer", "Producer")
                        .WithMany("Films")
                        .HasForeignKey("ProducerId");
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.FilmCountry", b =>
                {
                    b.HasOne("FilmStore.DAL.Entities.Country", "Country")
                        .WithMany("Films")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FilmStore.DAL.Entities.Film", "Film")
                        .WithMany("Countries")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.FilmGenre", b =>
                {
                    b.HasOne("FilmStore.DAL.Entities.Film", "Film")
                        .WithMany("Genres")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FilmStore.DAL.Entities.Genre", "Genre")
                        .WithMany("Films")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.FilmPurchase", b =>
                {
                    b.HasOne("FilmStore.DAL.Entities.Film", "Film")
                        .WithMany("Purchases")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FilmStore.DAL.Entities.Purchase", "Purchase")
                        .WithMany("Films")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FilmStore.DAL.Entities.Purchase", b =>
                {
                    b.HasOne("FilmStore.DAL.Entities.Customer", "Customer")
                        .WithMany("Purchases")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FilmStore.DAL.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FilmStore.DAL.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FilmStore.DAL.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FilmStore.DAL.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
