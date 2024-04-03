﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NBDGreenerGrass.Data;

#nullable disable

namespace NBDGreenerGrass.Data.NBDMigrations
{
    [DbContext(typeof(NBDContext))]
    [Migration("20240331221246_AddedApprovedReasonToBids")]
    partial class AddedApprovedReasonToBids
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.17");

            modelBuilder.Entity("NBDGreenerGrass.Models.Bid", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApprovedClientReason")
                        .HasColumnType("TEXT");

                    b.Property<string>("ApprovedManagerReason")
                        .HasColumnType("TEXT");

                    b.Property<int?>("BidLabourBidID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BidLabourLabourID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BidMaterialBidID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BidMaterialInventoryID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateMade")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeniedClientReason")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeniedManagerReason")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(1500)
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Stage")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("BidLabourBidID", "BidLabourLabourID");

                    b.HasIndex("BidMaterialBidID", "BidMaterialInventoryID");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.BidLabour", b =>
                {
                    b.Property<int>("BidID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LabourID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HoursWorked")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("LabourCost")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("LabourPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("LabourType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BidID", "LabourID");

                    b.HasIndex("LabourID");

                    b.ToTable("BidLabours");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.BidMaterial", b =>
                {
                    b.Property<int>("BidID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InventoryID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InventoryCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("InventoryDesc")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("InventoryListPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("InventorySize")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("BidID", "InventoryID");

                    b.HasIndex("InventoryID");

                    b.ToTable("BidMaterials");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("ClientRoleID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactFirst")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactLast")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Postal")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ClientRoleID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.ClientRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("ClientRoles");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Inventory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("InventoryCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("InventoryDesc")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("InventoryListPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("InventorySize")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Labour", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("LabourCost")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("LabourPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("LabourType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Labours");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("ClientID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Desc")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<string>("Postal")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Staff", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("StaffFirst")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("StaffLast")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("StaffRoleID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("StaffRoleID");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.StaffRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("StaffRoles");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Bid", b =>
                {
                    b.HasOne("NBDGreenerGrass.Models.Project", "Project")
                        .WithMany("Bids")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NBDGreenerGrass.Models.BidLabour", null)
                        .WithMany("Bids")
                        .HasForeignKey("BidLabourBidID", "BidLabourLabourID");

                    b.HasOne("NBDGreenerGrass.Models.BidMaterial", null)
                        .WithMany("Bids")
                        .HasForeignKey("BidMaterialBidID", "BidMaterialInventoryID");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.BidLabour", b =>
                {
                    b.HasOne("NBDGreenerGrass.Models.Bid", "Bid")
                        .WithMany("BidLabours")
                        .HasForeignKey("BidID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NBDGreenerGrass.Models.Labour", "Labour")
                        .WithMany("BidLabours")
                        .HasForeignKey("LabourID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bid");

                    b.Navigation("Labour");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.BidMaterial", b =>
                {
                    b.HasOne("NBDGreenerGrass.Models.Bid", "Bid")
                        .WithMany("BidMaterials")
                        .HasForeignKey("BidID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NBDGreenerGrass.Models.Inventory", "Inventory")
                        .WithMany("BidMaterials")
                        .HasForeignKey("InventoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bid");

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Client", b =>
                {
                    b.HasOne("NBDGreenerGrass.Models.ClientRole", "ClientRole")
                        .WithMany("Clients")
                        .HasForeignKey("ClientRoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientRole");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Project", b =>
                {
                    b.HasOne("NBDGreenerGrass.Models.Client", "Client")
                        .WithMany("Projects")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Staff", b =>
                {
                    b.HasOne("NBDGreenerGrass.Models.StaffRole", "StaffRole")
                        .WithMany("Staffs")
                        .HasForeignKey("StaffRoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StaffRole");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Bid", b =>
                {
                    b.Navigation("BidLabours");

                    b.Navigation("BidMaterials");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.BidLabour", b =>
                {
                    b.Navigation("Bids");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.BidMaterial", b =>
                {
                    b.Navigation("Bids");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Client", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.ClientRole", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Inventory", b =>
                {
                    b.Navigation("BidMaterials");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Labour", b =>
                {
                    b.Navigation("BidLabours");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.Project", b =>
                {
                    b.Navigation("Bids");
                });

            modelBuilder.Entity("NBDGreenerGrass.Models.StaffRole", b =>
                {
                    b.Navigation("Staffs");
                });
#pragma warning restore 612, 618
        }
    }
}