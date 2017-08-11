using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCartCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Infra
{
    public class PaymentMapping
    {
        public PaymentMapping(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(x => x.sales).WithOne();
        }
    }
}
