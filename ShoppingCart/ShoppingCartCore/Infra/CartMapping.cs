using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCartCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Infra
{
    public class CartMapping
    {
        public CartMapping(EntityTypeBuilder<CartModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.SaleId);
            builder.HasOne(x => x.sale).WithOne(x => x.cart).HasForeignKey<CartModel>(x => x.SaleId);
            builder.HasMany(x => x.cartItems).WithOne();
        }
    }
}
