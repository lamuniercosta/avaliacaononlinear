using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ShoppingCartCore.Infra;

namespace ShoppingCartCore.Migrations
{
    [DbContext(typeof(BaseContext))]
    partial class BaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShoppingCartCore.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SaleId");

                    b.HasKey("Id");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("ShoppingCartCore.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CartId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("ShoppingCartCore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ShoppingCartCore.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("ShoppingCartCore.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ShoppingCartCore.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CartId");

                    b.Property<DateTime>("DtSale");

                    b.Property<int>("PaymentId");

                    b.Property<decimal>("Total");

                    b.HasKey("Id");

                    b.HasIndex("CartId")
                        .IsUnique();

                    b.HasIndex("PaymentId");

                    b.ToTable("Sale");
                });

            modelBuilder.Entity("ShoppingCartCore.Models.CartItem", b =>
                {
                    b.HasOne("ShoppingCartCore.Models.Cart", "cart")
                        .WithMany("cartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShoppingCartCore.Models.Product", "product")
                        .WithMany("cartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShoppingCartCore.Models.Product", b =>
                {
                    b.HasOne("ShoppingCartCore.Models.Category", "category")
                        .WithMany("products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShoppingCartCore.Models.Sale", b =>
                {
                    b.HasOne("ShoppingCartCore.Models.Cart", "cart")
                        .WithOne("sale")
                        .HasForeignKey("ShoppingCartCore.Models.Sale", "CartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShoppingCartCore.Models.Payment", "payment")
                        .WithMany("sales")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
