using ShoppingCartCore.Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartCore.Models
{
    public class CategoryModel : BaseEntity
    {
        public string Name { get; set; }

        public List<ProductModel> products { get; set; }
    }
}
