﻿// <auto-generated />
using System;
using ApiProject_02_01_2024.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiProject_02_01_2024.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20250227092838_INitttt")]
    partial class INitttt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiProject_02_01_2024.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BankCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("LDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LIP")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LMAC")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("BankCode")
                        .IsUnique();

                    b.HasIndex("BankName")
                        .IsUnique();

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("ApiProject_02_01_2024.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CusTypeCode")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("LDate")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.Property<string>("LIP")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LMAC")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("ModifyDate")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("CusTypeCode");

                    b.HasIndex("CustomerCode")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ApiProject_02_01_2024.Models.CustomerDeliveryAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CPPhoneNo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CusDelAddCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("Id");

                    b.HasIndex("CusDelAddCode")
                        .IsUnique();

                    b.HasIndex("CustomerCode");

                    b.ToTable("CustomerDeliveryAddresses");
                });

            modelBuilder.Entity("ApiProject_02_01_2024.Models.CustomerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CusTypeCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CustomerTypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CusTypeCode")
                        .IsUnique();

                    b.HasIndex("CustomerTypeName")
                        .IsUnique();

                    b.ToTable("CustomerTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CusTypeCode = "01",
                            CustomerTypeName = "Dealer"
                        },
                        new
                        {
                            Id = 2,
                            CusTypeCode = "02",
                            CustomerTypeName = "Retailer"
                        },
                        new
                        {
                            Id = 3,
                            CusTypeCode = "03",
                            CustomerTypeName = "Corporate"
                        },
                        new
                        {
                            Id = 4,
                            CusTypeCode = "04",
                            CustomerTypeName = "Export"
                        },
                        new
                        {
                            Id = 5,
                            CusTypeCode = "05",
                            CustomerTypeName = "Online"
                        });
                });

            modelBuilder.Entity("ApiProject_02_01_2024.Models.Designation", b =>
                {
                    b.Property<int>("DesignationAutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DesignationAutoId"));

                    b.Property<string>("DesignationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DesignationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("LDate")
                        .HasColumnType("datetime");

                    b.Property<string>("LIP")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LMAC")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DesignationAutoId");

                    b.ToTable("Designations");
                });

            modelBuilder.Entity("ApiProject_02_01_2024.Models.HrmEmpDigitalSignature", b =>
                {
                    b.Property<int>("AutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AutoId"));

                    b.Property<int>("DesignationAutoId")
                        .HasColumnType("int");

                    b.Property<byte[]>("DigitalSignature")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("ImgSize")
                        .HasColumnType("bigint");

                    b.Property<string>("ImgType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AutoId");

                    b.ToTable("HrmEmpDigitalSignatures");
                });

            modelBuilder.Entity("ApiProject_02_01_2024.Models.Customer", b =>
                {
                    b.HasOne("ApiProject_02_01_2024.Models.CustomerType", "CustomerType")
                        .WithMany("Customers")
                        .HasForeignKey("CusTypeCode")
                        .HasPrincipalKey("CusTypeCode")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CustomerType");
                });

            modelBuilder.Entity("ApiProject_02_01_2024.Models.CustomerDeliveryAddress", b =>
                {
                    b.HasOne("ApiProject_02_01_2024.Models.Customer", "Customer")
                        .WithMany("CustomerDeliveryAddresses")
                        .HasForeignKey("CustomerCode")
                        .HasPrincipalKey("CustomerCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ApiProject_02_01_2024.Models.Customer", b =>
                {
                    b.Navigation("CustomerDeliveryAddresses");
                });

            modelBuilder.Entity("ApiProject_02_01_2024.Models.CustomerType", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
