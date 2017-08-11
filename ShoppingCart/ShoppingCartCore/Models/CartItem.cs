using ShoppingCartCore.Infra;
using ShoppingCartCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Models
{
    public class CartItem : BaseEntity
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Cart cart { get; set; }
        public Product product { get; set; }
    }
}
