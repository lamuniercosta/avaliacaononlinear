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
            new CategoryMapping(modelBuilder.Entity<Category>());
            new PaymentMapping(modelBuilder.Entity<Payment>());
            new ProductMapping(modelBuilder.Entity<Product>());
            new CartMapping(modelBuilder.Entity<Cart>());
            new CartItemMapping(modelBuilder.Entity<CartItem>());
            new SaleMapping(modelBuilder.Entity<Sale>());
        }

        
    }
}
