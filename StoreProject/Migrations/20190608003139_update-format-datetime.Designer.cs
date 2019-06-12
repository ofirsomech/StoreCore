﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreProject.DataContext;

namespace StoreProject.Migrations
{
    [DbContext(typeof(StoreDBContext))]
    [Migration("20190608003139_update-format-datetime")]
    partial class updateformatdatetime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StoreProject.Models.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<byte[]>("Img1");

                    b.Property<byte[]>("Img2");

                    b.Property<byte[]>("Img3");

                    b.Property<string>("LongDescription")
                        .IsRequired();

                    b.Property<long?>("OwnerId");

                    b.Property<decimal>("Price");

                    b.Property<string>("ShortDescription")
                        .IsRequired();

                    b.Property<int>("State");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("StoreProject.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<Guid>("Guid");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("Role");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StoreProject.Models.Product", b =>
                {
                    b.HasOne("StoreProject.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("StoreProject.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
