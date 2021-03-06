﻿// <auto-generated />
using System;
using Library.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library.DAL.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20200807102320_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("Library.DAL.Entities.CategoryEF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Library.DAL.Entities.InstrumentEF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Instruments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Direction"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Flûte/Piccolo"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Hautbois"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Trompette/Cornet"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Bugle"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Clarinette"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Saxophone"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Cor"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Baryton/Euphonium"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Trombone/Buccin/Serpent"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Clarinette Basse"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Basson"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Tuba"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Basse Ut"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Percussions"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Autre"
                        });
                });

            modelBuilder.Entity("Library.DAL.Entities.LibUserEF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdentityUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("LibraryUsers");
                });

            modelBuilder.Entity("Library.DAL.Entities.SheetEF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Arranger")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Composer")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCurrent")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsGarde")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsIndependance")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Sheets");
                });

            modelBuilder.Entity("Library.DAL.Entities.SheetPartEF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InstrumentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Part")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<int>("SheetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId");

                    b.HasIndex("SheetId");

                    b.ToTable("SheetParts");
                });

            modelBuilder.Entity("Library.DAL.Entities.UserInstruEF", b =>
                {
                    b.Property<int>("LibUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InstrumentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LibUserId", "InstrumentId");

                    b.HasIndex("InstrumentId");

                    b.ToTable("UserInstruments");
                });

            modelBuilder.Entity("Library.DAL.Entities.SheetEF", b =>
                {
                    b.HasOne("Library.DAL.Entities.CategoryEF", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("Library.DAL.Entities.SheetPartEF", b =>
                {
                    b.HasOne("Library.DAL.Entities.InstrumentEF", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId");

                    b.HasOne("Library.DAL.Entities.SheetEF", "Sheet")
                        .WithMany()
                        .HasForeignKey("SheetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Library.DAL.Entities.UserInstruEF", b =>
                {
                    b.HasOne("Library.DAL.Entities.InstrumentEF", "Instrument")
                        .WithMany("UserInstruments")
                        .HasForeignKey("InstrumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.DAL.Entities.LibUserEF", "LibUser")
                        .WithMany("UserInstruments")
                        .HasForeignKey("LibUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
