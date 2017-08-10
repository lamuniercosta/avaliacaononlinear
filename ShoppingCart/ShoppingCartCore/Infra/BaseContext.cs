using Microsoft.EntityFrameworkCore;
using ShoppingCartCore.Infra;
using ShoppingCartCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShoppingCartCore.Infra
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CategoryMapping(modelBuilder.Entity<CategoryModel>());
            new PaymentMapping(modelBuilder.Entity<PaymentModel>());
            new ProductMapping(modelBuilder.Entity<ProductModel>());
            new CartMapping(modelBuilder.Entity<CartModel>());
            new CartItemMapping(modelBuilder.Entity<CartItemModel>());
            new SaleMapping(modelBuilder.Entity<SaleModel>());
        }

        
    }
}
