using ShoppingCartCore.Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Models
{
    public class Payment : BaseEntity
    {
        public string Name { get; set; }

        public List<Sale> sales { get; set; }
    }
}
