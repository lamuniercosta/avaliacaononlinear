using ShoppingCartCore.Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public List<Product> products { get; set; }
    }
}
