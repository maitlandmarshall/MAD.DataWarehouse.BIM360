﻿// <auto-generated />
using System;
using MAD.DataWarehouse.BIM360.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MAD.DataWarehouse.BIM360.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221103022858_UpdateTable_ReportRunCheck_Index")]
    partial class UpdateTable_ReportRunCheck_Index
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MAD.DataWarehouse.BIM360.Api.Accounts.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessUnitId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConstructionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("JobNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastSignIn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("StateOrProvince")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Timezone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("Value")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("MAD.DataWarehouse.BIM360.Api.Data.FolderItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "ProjectId");

                    b.ToTable("FolderItem");
                });

            modelBuilder.Entity("MAD.DataWarehouse.BIM360.Api.Project.Hub", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hub");
                });

            modelBuilder.Entity("MAD.DataWarehouse.BIM360.Database.ReportRun", b =>
                {
                    b.Property<string>("WorkItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("FolderItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResultObjectKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkItemId");

                    b.ToTable("ReportRun");
                });

            modelBuilder.Entity("MAD.DataWarehouse.BIM360.Database.ReportRunCheck", b =>
                {
                    b.Property<string>("WorkItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CheckId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FailureMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResultMessage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkItemId", "CheckId");

                    b.ToTable("ReportRunCheck");
                });

            modelBuilder.Entity("MAD.DataWarehouse.BIM360.Api.Data.FolderItem", b =>
                {
                    b.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.FolderItemAttribute", "Attributes", b1 =>
                        {
                            b1.Property<string>("FolderItemId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("FolderItemProjectId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTimeOffset>("CreateTime")
                                .HasColumnType("datetimeoffset");

                            b1.Property<string>("CreateUserId")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("CreateUserName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("DisplayName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FileType")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<bool>("Hidden")
                                .HasColumnType("bit");

                            b1.Property<DateTimeOffset>("LastModifiedTime")
                                .HasColumnType("datetimeoffset");

                            b1.Property<DateTimeOffset>("LastModifiedTimeRollup")
                                .HasColumnType("datetimeoffset");

                            b1.Property<string>("LastModifiedUserId")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LastModifiedUserName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("MimeType")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<long>("ObjectCount")
                                .HasColumnType("bigint");

                            b1.Property<int?>("StorageSize")
                                .HasColumnType("int");

                            b1.Property<int?>("VersionNumber")
                                .HasColumnType("int");

                            b1.HasKey("FolderItemId", "FolderItemProjectId");

                            b1.ToTable("FolderItem");

                            b1.WithOwner()
                                .HasForeignKey("FolderItemId", "FolderItemProjectId");

                            b1.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.FolderItemAttributeExtension", "Extension", b2 =>
                                {
                                    b2.Property<string>("FolderItemAttributeFolderItemId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<string>("FolderItemAttributeFolderItemProjectId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<string>("Type")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Version")
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("FolderItemAttributeFolderItemId", "FolderItemAttributeFolderItemProjectId");

                                    b2.ToTable("FolderItem");

                                    b2.WithOwner()
                                        .HasForeignKey("FolderItemAttributeFolderItemId", "FolderItemAttributeFolderItemProjectId");

                                    b2.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.FolderItemAttributeExtensionData", "Data", b3 =>
                                        {
                                            b3.Property<string>("FolderItemAttributeExtensionFolderItemAttributeFolderItemId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("FolderItemAttributeExtensionFolderItemAttributeFolderItemProjectId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("Actions")
                                                .HasColumnType("nvarchar(max)");

                                            b3.Property<string>("AllowedTypes")
                                                .HasColumnType("nvarchar(max)");

                                            b3.Property<string>("NamingStandardIds")
                                                .HasColumnType("nvarchar(max)");

                                            b3.Property<string>("VisibleTypes")
                                                .HasColumnType("nvarchar(max)");

                                            b3.HasKey("FolderItemAttributeExtensionFolderItemAttributeFolderItemId", "FolderItemAttributeExtensionFolderItemAttributeFolderItemProjectId");

                                            b3.ToTable("FolderItem");

                                            b3.WithOwner()
                                                .HasForeignKey("FolderItemAttributeExtensionFolderItemAttributeFolderItemId", "FolderItemAttributeExtensionFolderItemAttributeFolderItemProjectId");
                                        });

                                    b2.Navigation("Data");
                                });

                            b1.Navigation("Extension")
                                .IsRequired();
                        });

                    b.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.FolderItemRelationships", "Relationships", b1 =>
                        {
                            b1.Property<string>("FolderItemId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("FolderItemProjectId")
                                .HasColumnType("nvarchar(450)");

                            b1.HasKey("FolderItemId", "FolderItemProjectId");

                            b1.ToTable("FolderItem");

                            b1.WithOwner()
                                .HasForeignKey("FolderItemId", "FolderItemProjectId");

                            b1.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainer", "Item", b2 =>
                                {
                                    b2.Property<string>("FolderItemRelationshipsFolderItemId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<string>("FolderItemRelationshipsFolderItemProjectId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.HasKey("FolderItemRelationshipsFolderItemId", "FolderItemRelationshipsFolderItemProjectId");

                                    b2.ToTable("FolderItem");

                                    b2.WithOwner()
                                        .HasForeignKey("FolderItemRelationshipsFolderItemId", "FolderItemRelationshipsFolderItemProjectId");

                                    b2.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerData", "Data", b3 =>
                                        {
                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("Id")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("Type")
                                                .HasColumnType("nvarchar(max)");

                                            b3.HasKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.HasIndex("Id");

                                            b3.ToTable("FolderItem");

                                            b3.WithOwner()
                                                .HasForeignKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");
                                        });

                                    b2.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerMeta", "Meta", b3 =>
                                        {
                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.HasKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.ToTable("FolderItem");

                                            b3.WithOwner()
                                                .HasForeignKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerMetaLink", "Link", b4 =>
                                                {
                                                    b4.Property<string>("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId")
                                                        .HasColumnType("nvarchar(450)");

                                                    b4.Property<string>("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                        .HasColumnType("nvarchar(450)");

                                                    b4.Property<string>("Href")
                                                        .HasColumnType("nvarchar(max)");

                                                    b4.HasKey("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                                    b4.ToTable("FolderItem");

                                                    b4.WithOwner()
                                                        .HasForeignKey("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId");
                                                });

                                            b3.Navigation("Link");
                                        });

                                    b2.Navigation("Data");

                                    b2.Navigation("Meta")
                                        .IsRequired();
                                });

                            b1.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainer", "Parent", b2 =>
                                {
                                    b2.Property<string>("FolderItemRelationshipsFolderItemId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<string>("FolderItemRelationshipsFolderItemProjectId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.HasKey("FolderItemRelationshipsFolderItemId", "FolderItemRelationshipsFolderItemProjectId");

                                    b2.ToTable("FolderItem");

                                    b2.WithOwner()
                                        .HasForeignKey("FolderItemRelationshipsFolderItemId", "FolderItemRelationshipsFolderItemProjectId");

                                    b2.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerData", "Data", b3 =>
                                        {
                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("Id")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("Type")
                                                .HasColumnType("nvarchar(max)");

                                            b3.HasKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.HasIndex("Id");

                                            b3.ToTable("FolderItem");

                                            b3.WithOwner()
                                                .HasForeignKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");
                                        });

                                    b2.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerMeta", "Meta", b3 =>
                                        {
                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.HasKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.ToTable("FolderItem");

                                            b3.WithOwner()
                                                .HasForeignKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerMetaLink", "Link", b4 =>
                                                {
                                                    b4.Property<string>("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId")
                                                        .HasColumnType("nvarchar(450)");

                                                    b4.Property<string>("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                        .HasColumnType("nvarchar(450)");

                                                    b4.Property<string>("Href")
                                                        .HasColumnType("nvarchar(max)");

                                                    b4.HasKey("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                                    b4.ToTable("FolderItem");

                                                    b4.WithOwner()
                                                        .HasForeignKey("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId");
                                                });

                                            b3.Navigation("Link");
                                        });

                                    b2.Navigation("Data");

                                    b2.Navigation("Meta")
                                        .IsRequired();
                                });

                            b1.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainer", "Storage", b2 =>
                                {
                                    b2.Property<string>("FolderItemRelationshipsFolderItemId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<string>("FolderItemRelationshipsFolderItemProjectId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.HasKey("FolderItemRelationshipsFolderItemId", "FolderItemRelationshipsFolderItemProjectId");

                                    b2.ToTable("FolderItem");

                                    b2.WithOwner()
                                        .HasForeignKey("FolderItemRelationshipsFolderItemId", "FolderItemRelationshipsFolderItemProjectId");

                                    b2.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerData", "Data", b3 =>
                                        {
                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("Id")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("Type")
                                                .HasColumnType("nvarchar(max)");

                                            b3.HasKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.HasIndex("Id");

                                            b3.ToTable("FolderItem");

                                            b3.WithOwner()
                                                .HasForeignKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");
                                        });

                                    b2.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerMeta", "Meta", b3 =>
                                        {
                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.HasKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.ToTable("FolderItem");

                                            b3.WithOwner()
                                                .HasForeignKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerMetaLink", "Link", b4 =>
                                                {
                                                    b4.Property<string>("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId")
                                                        .HasColumnType("nvarchar(450)");

                                                    b4.Property<string>("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                        .HasColumnType("nvarchar(450)");

                                                    b4.Property<string>("Href")
                                                        .HasColumnType("nvarchar(max)");

                                                    b4.HasKey("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                                    b4.ToTable("FolderItem");

                                                    b4.WithOwner()
                                                        .HasForeignKey("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId");
                                                });

                                            b3.Navigation("Link");
                                        });

                                    b2.Navigation("Data");

                                    b2.Navigation("Meta")
                                        .IsRequired();
                                });

                            b1.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainer", "Tip", b2 =>
                                {
                                    b2.Property<string>("FolderItemRelationshipsFolderItemId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<string>("FolderItemRelationshipsFolderItemProjectId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.HasKey("FolderItemRelationshipsFolderItemId", "FolderItemRelationshipsFolderItemProjectId");

                                    b2.ToTable("FolderItem");

                                    b2.WithOwner()
                                        .HasForeignKey("FolderItemRelationshipsFolderItemId", "FolderItemRelationshipsFolderItemProjectId");

                                    b2.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerData", "Data", b3 =>
                                        {
                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("Id")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("Type")
                                                .HasColumnType("nvarchar(max)");

                                            b3.HasKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.HasIndex("Id");

                                            b3.ToTable("FolderItem");

                                            b3.WithOwner()
                                                .HasForeignKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");
                                        });

                                    b2.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerMeta", "Meta", b3 =>
                                        {
                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<string>("RelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.HasKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.ToTable("FolderItem");

                                            b3.WithOwner()
                                                .HasForeignKey("RelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                            b3.OwnsOne("MAD.DataWarehouse.BIM360.Api.Data.RelationshipContainerMetaLink", "Link", b4 =>
                                                {
                                                    b4.Property<string>("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId")
                                                        .HasColumnType("nvarchar(450)");

                                                    b4.Property<string>("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId")
                                                        .HasColumnType("nvarchar(450)");

                                                    b4.Property<string>("Href")
                                                        .HasColumnType("nvarchar(max)");

                                                    b4.HasKey("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId");

                                                    b4.ToTable("FolderItem");

                                                    b4.WithOwner()
                                                        .HasForeignKey("RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemId", "RelationshipContainerMetaRelationshipContainerFolderItemRelationshipsFolderItemProjectId");
                                                });

                                            b3.Navigation("Link");
                                        });

                                    b2.Navigation("Data");

                                    b2.Navigation("Meta")
                                        .IsRequired();
                                });

                            b1.Navigation("Item")
                                .IsRequired();

                            b1.Navigation("Parent")
                                .IsRequired();

                            b1.Navigation("Storage")
                                .IsRequired();

                            b1.Navigation("Tip")
                                .IsRequired();
                        });

                    b.Navigation("Attributes")
                        .IsRequired();

                    b.Navigation("Relationships")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
