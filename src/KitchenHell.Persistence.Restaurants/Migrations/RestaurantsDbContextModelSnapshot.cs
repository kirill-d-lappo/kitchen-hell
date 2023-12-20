﻿// <auto-generated />
using KitchenHell.Persistence.Restaurants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KitchenHell.Persistence.Restaurants.Migrations
{
    [DbContext(typeof(RestaurantsDbContext))]
    partial class RestaurantsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("restaurants")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KitchenHell.Persistence.Restaurants.Models.RestaurantEfEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("FullAddress")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Restaurants", "restaurants");
                });

            modelBuilder.Entity("KitchenHell.Persistence.Restaurants.Models.RestaurantOrderEfEntity", b =>
                {
                    b.Property<long>("RestaurantId")
                        .HasColumnType("bigint");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.HasKey("RestaurantId", "OrderId");

                    b.ToTable("RestaurantOrders", "restaurants");
                });

            modelBuilder.Entity("KitchenHell.Persistence.Restaurants.Models.RestaurantOrderEfEntity", b =>
                {
                    b.HasOne("KitchenHell.Persistence.Restaurants.Models.RestaurantEfEntity", "Restaurant")
                        .WithMany("RestaurantOrders")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("KitchenHell.Persistence.Restaurants.Models.RestaurantEfEntity", b =>
                {
                    b.Navigation("RestaurantOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
