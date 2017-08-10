using ShoppingCartCore.Infra;
using ShoppingCartCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Models
{
    public class CartModel : BaseEntity
    {
        public int SaleId { get; set; }

        public SaleModel sale { get; set; }
        public List<CartItemModel> cartItems { get; set; }
    }
}
