using ShoppingCartCore.Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Models
{
    public class SaleModel : BaseEntity
    {
        public int CartId { get; set; }
        public int PaymentId { get; set; }
        public DateTime DtSale { get; set; }
        public decimal Total { get; set; }

        public CartModel cart { get; set; }
        public PaymentModel payment { get; set; }
    }
}
