using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartCore.Infra;
using ShoppingCartCore.Models;
using ShoppingCartCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartTest.Core
{
    [TestClass]
    public class ProductTest
    {
        private BaseContext db;
        private GenericRepository<ProductModel> repoProduct;
        private GenericRepository<CategoryModel> repoCategory;

        [TestInitialize]
        public void SetUp()
        {
            //var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AvaliacaoCedro;integrated security=True;");
            var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=DESKTOP-NBVUSVN\\SQLEXPRESS;Database=AvaliacaoCedro;integrated security=True;");
            db = new BaseContext(builder.Options);
            this.repoProduct = new GenericRepository<ProductModel>(db);
            this.repoCategory = new GenericRepository<CategoryModel>(db);
        }

        [TestMethod]
        public void InsertCategories()
        {
            var cat1 = new CategoryModel { Name = "Cat1"};
            var cat2 = new CategoryModel { Name = "Cat2" };
            repoCategory.Save(cat1);
            repoCategory.Save(cat2);

        }
    }
}
