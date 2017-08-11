using ShoppingCartCore.Infra;
using ShoppingCartCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Models
{
    public class Product : BaseEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Category category { get; set; }
        public List<CartItem> cartItems { get; set; }
    }
}
