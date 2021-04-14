﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using temperature_conversion_app_ef_core.Models;

namespace temperature_conversion_app_ef_core.Migrations
{
    [DbContext(typeof(TemperatureConversionAppDbContext))]
    [Migration("20210413011222_CreateTemperatureConversionAppDB")]
    partial class CreateTemperatureConversionAppDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("temperature_conversion_app_ef_core.Models.Conversion", b =>
                {
                    b.Property<int>("ConversionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FinalMetric")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("FinalValue")
                        .HasColumnType("real");

                    b.Property<string>("InitialMetric")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("InitialValue")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ConversionId");

                    b.HasIndex("UserId");

                    b.ToTable("Conversions");
                });

            modelBuilder.Entity("temperature_conversion_app_ef_core.Models.Metric", b =>
                {
                    b.Property<int>("MetricId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetricId");

                    b.ToTable("Metrics");

                    b.HasData(
                        new
                        {
                            MetricId = 1,
                            Title = "Celsius"
                        },
                        new
                        {
                            MetricId = 2,
                            Title = "Fahrenheit"
                        },
                        new
                        {
                            MetricId = 3,
                            Title = "Kelvin"
                        });
                });

            modelBuilder.Entity("temperature_conversion_app_ef_core.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("temperature_conversion_app_ef_core.Models.Conversion", b =>
                {
                    b.HasOne("temperature_conversion_app_ef_core.Models.User", "User")
                        .WithMany("Conversions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("temperature_conversion_app_ef_core.Models.User", b =>
                {
                    b.Navigation("Conversions");
                });
#pragma warning restore 612, 618
        }
    }
}
