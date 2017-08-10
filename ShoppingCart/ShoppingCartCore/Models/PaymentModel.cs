using ShoppingCartCore.Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Models
{
    public class PaymentModel : BaseEntity
    {
        public string Name { get; set; }

        public List<SaleModel> sales { get; set; }
    }
}
