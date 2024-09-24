﻿// <auto-generated />
using System;
using BouillonChanvre.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BouillonChanvre.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240912142243_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BouillonChanvre.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BouillonChanvre.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("SubCategoryID")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("SubCategoryID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BouillonChanvre.Models.ProductDescription", b =>
                {
                    b.Property<int>("ProductDescriptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductDescriptionID"));

                    b.Property<string>("Benefits")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Ingredients")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ProducerInformation")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("ProductVariantID")
                        .HasColumnType("int");

                    b.Property<string>("UsageInstructions")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.HasKey("ProductDescriptionID");

                    b.HasIndex("ProductVariantID")
                        .IsUnique();

                    b.ToTable("ProductDescriptions");
                });

            modelBuilder.Entity("BouillonChanvre.Models.ProductImage", b =>
                {
                    b.Property<int>("ProductImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductImageID"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductVariantID")
                        .HasColumnType("int");

                    b.HasKey("ProductImageID");

                    b.HasIndex("ProductVariantID");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("BouillonChanvre.Models.ProductSize", b =>
                {
                    b.Property<int>("ProductSizeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductSizeID"));

                    b.Property<decimal>("CBDConcentration")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductVariantID")
                        .HasColumnType("int");

                    b.Property<decimal>("Size")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SizeUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductSizeID");

                    b.HasIndex("ProductVariantID");

                    b.ToTable("ProductSizes");
                });

            modelBuilder.Entity("BouillonChanvre.Models.ProductVariant", b =>
                {
                    b.Property<int>("ProductVariantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductVariantID"));

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<string>("VariantName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VariantType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ProductVariantID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductVariants");
                });

            modelBuilder.Entity("BouillonChanvre.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CookingTime")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ingredients")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PreparationTime")
                        .HasColumnType("int");

                    b.Property<int>("Servings")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("BouillonChanvre.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoryID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SubCategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("BouillonChanvre.Models.Product", b =>
                {
                    b.HasOne("BouillonChanvre.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BouillonChanvre.Models.SubCategory", "Subcategory")
                        .WithMany("Products")
                        .HasForeignKey("SubCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("BouillonChanvre.Models.ProductDescription", b =>
                {
                    b.HasOne("BouillonChanvre.Models.ProductVariant", "ProductVariant")
                        .WithOne("Description")
                        .HasForeignKey("BouillonChanvre.Models.ProductDescription", "ProductVariantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("BouillonChanvre.Models.ProductImage", b =>
                {
                    b.HasOne("BouillonChanvre.Models.ProductVariant", "ProductVariant")
                        .WithMany("Images")
                        .HasForeignKey("ProductVariantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("BouillonChanvre.Models.ProductSize", b =>
                {
                    b.HasOne("BouillonChanvre.Models.ProductVariant", "ProductVariant")
                        .WithMany("Sizes")
                        .HasForeignKey("ProductVariantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("BouillonChanvre.Models.ProductVariant", b =>
                {
                    b.HasOne("BouillonChanvre.Models.Product", "Product")
                        .WithMany("Variants")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BouillonChanvre.Models.SubCategory", b =>
                {
                    b.HasOne("BouillonChanvre.Models.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BouillonChanvre.Models.Category", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("BouillonChanvre.Models.Product", b =>
                {
                    b.Navigation("Variants");
                });

            modelBuilder.Entity("BouillonChanvre.Models.ProductVariant", b =>
                {
                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Images");

                    b.Navigation("Sizes");
                });

            modelBuilder.Entity("BouillonChanvre.Models.SubCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
