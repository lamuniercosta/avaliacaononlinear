using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartCore.Infra;
using ShoppingCartCore.Models;
using ShoppingCartCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShoppingCartTest.Core
{
    [TestClass]
    public class ProductTest
    {
        private BaseContext db;
        private GenericRepository<Product> repoProduct;
        private GenericRepository<Category> repoCategory;

        [TestInitialize]
        public void SetUp()
        {
            //var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AvaliacaoNonlinear;integrated security=True;");
            var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=DESKTOP-NBVUSVN\\SQLEXPRESS;Database=AvaliacaoNonlinear;integrated security=True;");
            db = new BaseContext(builder.Options);
            this.repoProduct = new GenericRepository<Product>(db);
            this.repoCategory = new GenericRepository<Category>(db);
        }

        [TestMethod]
        public void InsertCategories()
        {
            var cat1 = new Category { Name = "Cat1"};
            var cat2 = new Category { Name = "Cat2" };
            repoCategory.Save(cat1);
            repoCategory.Save(cat2);

            Assert.AreNotEqual(cat1.Id, 0);
            Assert.AreNotEqual(cat2.Id, 0);
        }

        public int GetGategoryByName(string name)
        {
            return repoCategory.Where(c => c.Name == name).FirstOrDefault().Id;
        }

        [TestMethod]
        public void InsertProduct()
        {
            var product = new Product();
            product.CategoryId = GetGategoryByName("Cat1");
            product.Name = "Product1";
            product.Price = 99.9m;

            var product2 = new Product();
            product2.CategoryId = GetGategoryByName("Cat1");
            product2.Name = "Product2";
            product2.Price = 49.9m;

            var product3 = new Product();
            product3.CategoryId = GetGategoryByName("Cat2");
            product3.Name = "Product3";
            product3.Price = 199.9m;

            repoProduct.Save(product);
            repoProduct.Save(product2);
            repoProduct.Save(product3);

            Assert.AreNotEqual(product.Id, 0);
            Assert.AreNotEqual(product2.Id, 0);
            Assert.AreNotEqual(product3.Id, 0);
        }

        [TestMethod]
        public void GetProducts()
        {
            var products = repoProduct.GetAll();

            Assert.AreEqual(products.Count, 3);
        }
    }
}
