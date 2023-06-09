﻿// <auto-generated />
using DataAccessLayers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DeviceAPI.Migrations
{
    [DbContext(typeof(DeviceDbContext))]
    [Migration("20230330072421_Initial11")]
    partial class Initial11
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.2.23128.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayers.Models.Device", b =>
                {
                    b.Property<string>("MACID")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.ToTable("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
