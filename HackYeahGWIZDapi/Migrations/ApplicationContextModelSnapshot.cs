﻿// <auto-generated />
using System;
using HackYeahGWIZDapi.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HackYeahGWIZDapi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("HackYeahGWIZDapi.Model.Animal", b =>
                {
                    b.Property<long>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AnimalId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("HackYeahGWIZDapi.Model.Event", b =>
                {
                    b.Property<long?>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("AnimalId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("EventPhotosPhotoId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ExpiredTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LocalizationId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("EventId");

                    b.HasIndex("EventPhotosPhotoId");

                    b.HasIndex("LocalizationId");

                    b.HasIndex("UserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("HackYeahGWIZDapi.Model.EventPhoto", b =>
                {
                    b.Property<long?>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhotoId");

                    b.ToTable("EventPhotos");
                });

            modelBuilder.Entity("HackYeahGWIZDapi.Model.Localization", b =>
                {
                    b.Property<long?>("LocalizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("LocalizationId");

                    b.ToTable("Localizations");
                });

            modelBuilder.Entity("HackYeahGWIZDapi.Model.PredictionEvent", b =>
                {
                    b.Property<long>("PredictionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("LocalizationId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PhotoId")
                        .HasColumnType("bigint");

                    b.HasKey("PredictionId");

                    b.HasIndex("LocalizationId");

                    b.HasIndex("PhotoId");

                    b.ToTable("PredictionEvents");
                });

            modelBuilder.Entity("HackYeahGWIZDapi.Model.User", b =>
                {
                    b.Property<long?>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HackYeahGWIZDapi.Model.Event", b =>
                {
                    b.HasOne("HackYeahGWIZDapi.Model.EventPhoto", "EventPhotos")
                        .WithMany()
                        .HasForeignKey("EventPhotosPhotoId");

                    b.HasOne("HackYeahGWIZDapi.Model.Localization", "Localization")
                        .WithMany()
                        .HasForeignKey("LocalizationId");

                    b.HasOne("HackYeahGWIZDapi.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("EventPhotos");

                    b.Navigation("Localization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HackYeahGWIZDapi.Model.PredictionEvent", b =>
                {
                    b.HasOne("HackYeahGWIZDapi.Model.Localization", "Localization")
                        .WithMany()
                        .HasForeignKey("LocalizationId");

                    b.HasOne("HackYeahGWIZDapi.Model.EventPhoto", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId");

                    b.Navigation("Localization");

                    b.Navigation("Photo");
                });
#pragma warning restore 612, 618
        }
    }
}
