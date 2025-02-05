﻿// <auto-generated />
using System;
using Kreislauf.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kreislauf.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240718132852_UpdateBarcodeModel")]
    partial class UpdateBarcodeModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Kreislauf.Models.Barcode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LaufId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("LaufId");

                    b.HasIndex("PersonId");

                    b.ToTable("BarcodeNew");
                });

            modelBuilder.Entity("Kreislauf.Models.Klasse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Schule_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Schule_Id");

                    b.ToTable("Klassen");
                });

            modelBuilder.Entity("Kreislauf.Models.Lauf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double?>("RundenAnzahl")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Laeufe");
                });

            modelBuilder.Entity("Kreislauf.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("Geschlecht")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("Klasse_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Lauf_Id")
                        .HasColumnType("int");

                    b.Property<int>("Lebensalter")
                        .HasColumnType("int");

                    b.Property<string>("Nachname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Klasse_Id");

                    b.HasIndex("Lauf_Id");

                    b.ToTable("Personen");
                });

            modelBuilder.Entity("Kreislauf.Models.Schule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Stadt")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Schulen");
                });

            modelBuilder.Entity("Kreislauf.Models.Barcode", b =>
                {
                    b.HasOne("Kreislauf.Models.Lauf", "Lauf")
                        .WithMany()
                        .HasForeignKey("LaufId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kreislauf.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lauf");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Kreislauf.Models.Klasse", b =>
                {
                    b.HasOne("Kreislauf.Models.Schule", "Schule")
                        .WithMany("Klassen")
                        .HasForeignKey("Schule_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schule");
                });

            modelBuilder.Entity("Kreislauf.Models.Person", b =>
                {
                    b.HasOne("Kreislauf.Models.Klasse", "Klasse")
                        .WithMany("Personen")
                        .HasForeignKey("Klasse_Id");

                    b.HasOne("Kreislauf.Models.Lauf", "Lauf")
                        .WithMany("Personen")
                        .HasForeignKey("Lauf_Id");

                    b.Navigation("Klasse");

                    b.Navigation("Lauf");
                });

            modelBuilder.Entity("Kreislauf.Models.Klasse", b =>
                {
                    b.Navigation("Personen");
                });

            modelBuilder.Entity("Kreislauf.Models.Lauf", b =>
                {
                    b.Navigation("Personen");
                });

            modelBuilder.Entity("Kreislauf.Models.Schule", b =>
                {
                    b.Navigation("Klassen");
                });
#pragma warning restore 612, 618
        }
    }
}
