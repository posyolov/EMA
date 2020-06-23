﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.EF;

namespace Repository.Migrations
{
    [DbContext(typeof(EquipmentContext))]
    [Migration("20200619204913_entries")]
    partial class entries
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Repository.EF.CatalogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("GlobalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VendorId");

                    b.ToTable("Catalog");
                });

            modelBuilder.Entity("Repository.EF.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EntryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Repository.EF.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContinuationCriteriaId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("EstimatedResources")
                        .HasColumnType("real");

                    b.Property<int?>("NextId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OccurDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PlannedStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<int?>("PrevId")
                        .HasColumnType("int");

                    b.Property<int?>("Priority")
                        .HasColumnType("int");

                    b.Property<int?>("ReasonId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContinuationCriteriaId");

                    b.HasIndex("NextId");

                    b.HasIndex("PositionId");

                    b.HasIndex("PrevId");

                    b.HasIndex("ReasonId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("Repository.EF.EntryContinuationCriteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EntryContinuationCriterias");
                });

            modelBuilder.Entity("Repository.EF.EntryReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EntryReasons");
                });

            modelBuilder.Entity("Repository.EF.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CatalogItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogItemId");

                    b.HasIndex("ParentId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Repository.EF.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("Repository.EF.CatalogItem", b =>
                {
                    b.HasOne("Repository.EF.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Repository.EF.Department", b =>
                {
                    b.HasOne("Repository.EF.Entry", null)
                        .WithMany("AttachedDepartments")
                        .HasForeignKey("EntryId");
                });

            modelBuilder.Entity("Repository.EF.Entry", b =>
                {
                    b.HasOne("Repository.EF.EntryContinuationCriteria", "ContinuationCriteria")
                        .WithMany()
                        .HasForeignKey("ContinuationCriteriaId");

                    b.HasOne("Repository.EF.Entry", "Next")
                        .WithMany()
                        .HasForeignKey("NextId");

                    b.HasOne("Repository.EF.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");

                    b.HasOne("Repository.EF.Entry", "Prev")
                        .WithMany()
                        .HasForeignKey("PrevId");

                    b.HasOne("Repository.EF.EntryReason", "Reason")
                        .WithMany()
                        .HasForeignKey("ReasonId");
                });

            modelBuilder.Entity("Repository.EF.Position", b =>
                {
                    b.HasOne("Repository.EF.CatalogItem", "CatalogItem")
                        .WithMany()
                        .HasForeignKey("CatalogItemId");

                    b.HasOne("Repository.EF.Position", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}