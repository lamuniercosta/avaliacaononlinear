using ShoppingCartCore.Infra;
using ShoppingCartCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Models
{
    public class Cart : BaseEntity
    {
        public int SaleId { get; set; }

        public Sale sale { get; set; }
        public List<CartItem> cartItems { get; set; }
    }
}
