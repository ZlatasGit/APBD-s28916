﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PROJECT.Context;

#nullable disable

namespace PROJECT.Migrations
{
    [DbContext(typeof(SystemDbContext))]
    partial class SystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PROJECT.Models.AbstractClient", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClient"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdClient");

                    b.ToTable("Clients");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AbstractClient");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("PROJECT.Models.Contract", b =>
                {
                    b.Property<int>("IdContract")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdContract"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<int?>("ExtendUpdatesBy")
                        .HasColumnType("int");

                    b.Property<int>("SoftwareSystemId")
                        .HasColumnType("int");

                    b.Property<string>("SoftwareVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("IdContract");

                    b.HasIndex("ClientId");

                    b.HasIndex("SoftwareSystemId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("PROJECT.Models.Discount", b =>
                {
                    b.Property<int>("IdDiscount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDiscount"));

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("IdDiscount");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("PROJECT.Models.Payment", b =>
                {
                    b.Property<int>("IdPayment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPayment"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<bool>("IsReturned")
                        .HasColumnType("bit");

                    b.Property<DateOnly>("PaymentDate")
                        .HasColumnType("date");

                    b.HasKey("IdPayment");

                    b.HasIndex("ContractId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("PROJECT.Models.SoftwareSystem", b =>
                {
                    b.Property<int>("IdSoftware")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSoftware"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSoftware");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("PROJECT.Models.Version", b =>
                {
                    b.Property<int>("IdVersion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdVersion"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("SoftwareId")
                        .HasColumnType("int");

                    b.HasKey("IdVersion");

                    b.HasIndex("SoftwareId");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("PROJECT.Models.CorporateClient", b =>
                {
                    b.HasBaseType("PROJECT.Models.AbstractClient");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Clients");

                    b.HasDiscriminator().HasValue("CorporateClient");
                });

            modelBuilder.Entity("PROJECT.Models.IndividualClient", b =>
                {
                    b.HasBaseType("PROJECT.Models.AbstractClient");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.ToTable("Clients");

                    b.HasDiscriminator().HasValue("IndividualClient");
                });

            modelBuilder.Entity("PROJECT.Models.Contract", b =>
                {
                    b.HasOne("PROJECT.Models.AbstractClient", "Client")
                        .WithMany("Contracts")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PROJECT.Models.SoftwareSystem", "SoftwareSystem")
                        .WithMany("Contracts")
                        .HasForeignKey("SoftwareSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("SoftwareSystem");
                });

            modelBuilder.Entity("PROJECT.Models.Payment", b =>
                {
                    b.HasOne("PROJECT.Models.Contract", "Contract")
                        .WithMany("Payments")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("PROJECT.Models.Version", b =>
                {
                    b.HasOne("PROJECT.Models.SoftwareSystem", "Software")
                        .WithMany("Versions")
                        .HasForeignKey("SoftwareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Software");
                });

            modelBuilder.Entity("PROJECT.Models.AbstractClient", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("PROJECT.Models.Contract", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("PROJECT.Models.SoftwareSystem", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Versions");
                });
#pragma warning restore 612, 618
        }
    }
}
