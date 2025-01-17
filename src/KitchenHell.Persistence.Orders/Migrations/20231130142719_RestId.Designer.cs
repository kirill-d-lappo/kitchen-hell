﻿// <auto-generated />
using System;
using KitchenHell.Persistence.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KitchenHell.Persistence.Orders.Migrations
{
    [DbContext(typeof(OrdersDbContext))]
    [Migration("20231130142719_RestId")]
    partial class RestId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("orders")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KitchenHell.Persistence.Orders.Models.OrderEfEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("RestaurantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Orders", "orders");
                });
#pragma warning restore 612, 618
        }
    }
}
