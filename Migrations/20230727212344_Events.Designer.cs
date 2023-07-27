﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SaintBarnabasHouse.Models;

#nullable disable

namespace SaintBarnabasHouse.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230727212344_Events")]
    partial class Events
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SaintBarnabasHouse.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Street2")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.BlogPost", b =>
                {
                    b.Property<int>("BlogPostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Creator")
                        .HasColumnType("int");

                    b.Property<string>("MainImgUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("BlogPostId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.CartProductAssoc", b =>
                {
                    b.Property<int>("CartProductAssocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CartProductAssocId");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartProductAssocs");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BlogPostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ParentComment")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("BlogPostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("OnSite")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Private")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RequesterEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RequesterName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BillingAddressId")
                        .HasColumnType("int");

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OrderComments")
                        .HasColumnType("longtext");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<bool>("PickUp")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ShippingAddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.OrderProductAssoc", b =>
                {
                    b.Property<int>("OrderProductAssocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("OrderProductAssocId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProductAssocs");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MainCat")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MainImgUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("PriceAsInt")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.ProductCategoryAssoc", b =>
                {
                    b.Property<int>("ProductCategoryAssocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ProductCategoryAssocId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategoryAssocs");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.ProductImageAssoc", b =>
                {
                    b.Property<int>("ProductImageAssocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ProductImageAssocId");

                    b.HasIndex("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImageAssocs");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Address", b =>
                {
                    b.HasOne("SaintBarnabasHouse.Models.User", "User")
                        .WithMany("UserAddresses")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.CartProductAssoc", b =>
                {
                    b.HasOne("SaintBarnabasHouse.Models.Cart", "Cart")
                        .WithMany("CartProductAssocs")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SaintBarnabasHouse.Models.Product", "Product")
                        .WithMany("CartProductAssocs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Comment", b =>
                {
                    b.HasOne("SaintBarnabasHouse.Models.BlogPost", null)
                        .WithMany("Comments")
                        .HasForeignKey("BlogPostId");

                    b.HasOne("SaintBarnabasHouse.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Order", b =>
                {
                    b.HasOne("SaintBarnabasHouse.Models.User", "User")
                        .WithMany("UserOrders")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.OrderProductAssoc", b =>
                {
                    b.HasOne("SaintBarnabasHouse.Models.Order", "Order")
                        .WithMany("OrderProductAssocs")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SaintBarnabasHouse.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.ProductCategoryAssoc", b =>
                {
                    b.HasOne("SaintBarnabasHouse.Models.Category", "Category")
                        .WithMany("ProductCategoryAssocs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SaintBarnabasHouse.Models.Product", "Product")
                        .WithMany("ProductCategoryAssocs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.ProductImageAssoc", b =>
                {
                    b.HasOne("SaintBarnabasHouse.Models.Image", "Image")
                        .WithMany("ProductImageAssocs")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SaintBarnabasHouse.Models.Product", "Product")
                        .WithMany("ProductImageAssocs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.BlogPost", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Cart", b =>
                {
                    b.Navigation("CartProductAssocs");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Category", b =>
                {
                    b.Navigation("ProductCategoryAssocs");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Image", b =>
                {
                    b.Navigation("ProductImageAssocs");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Order", b =>
                {
                    b.Navigation("OrderProductAssocs");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.Product", b =>
                {
                    b.Navigation("CartProductAssocs");

                    b.Navigation("ProductCategoryAssocs");

                    b.Navigation("ProductImageAssocs");
                });

            modelBuilder.Entity("SaintBarnabasHouse.Models.User", b =>
                {
                    b.Navigation("UserAddresses");

                    b.Navigation("UserOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
