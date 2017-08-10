using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCartCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Infra
{
    public class CartItemMapping
    {
        public CartItemMapping(EntityTypeBuilder<CartItemModel> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CartId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();

            builder.HasOne(x => x.cart).WithMany(x => x.cartItems).HasForeignKey(x => x.CartId);
            builder.HasOne(x => x.product).WithMany(x => x.cartItems).HasForeignKey(x => x.ProductId);
        }
    }
}
