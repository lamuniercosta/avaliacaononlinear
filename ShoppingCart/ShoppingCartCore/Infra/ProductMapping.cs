using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCartCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Infra
{
    public class ProductMapping
    {
        public ProductMapping(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CategoryId);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Price).IsRequired();
            builder.HasOne(x => x.category).WithMany(x => x.products).HasForeignKey(x => x.CategoryId);
            builder.HasMany(x => x.cartItems).WithOne();
        }
    }
}
