using ShoppingCartCore.Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Models
{
    public class Sale : BaseEntity
    {
        public int CartId { get; set; }
        public int PaymentId { get; set; }
        public DateTime DtSale { get; set; }
        public decimal Total { get; set; }

        public Cart cart { get; set; }
        public Payment payment { get; set; }
    }
}
