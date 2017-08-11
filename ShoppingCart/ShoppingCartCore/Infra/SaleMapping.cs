using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCartCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Infra
{
    public class SaleMapping
    {
        public SaleMapping(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CartId).IsRequired();
            builder.Property(x => x.PaymentId).IsRequired();
            builder.Property(x => x.DtSale).IsRequired();
            builder.Property(x => x.Total).IsRequired();
            builder.HasOne(x => x.cart).WithOne(x => x.sale).HasForeignKey<Sale>(x => x.CartId);
            builder.HasOne(x => x.payment).WithMany(x => x.sales).HasForeignKey(x => x.PaymentId);
        }
    }
}
