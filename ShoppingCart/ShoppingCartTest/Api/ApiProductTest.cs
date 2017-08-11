using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartCore.Infra;
using ShoppingCartCore.Models;
using ShoppingCartCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ShoppingCartApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartTest.Api
{
    [TestClass]
    public class ApiProductTest
    {
        private BaseContext db;
        private ProductsController controllerProduct;
        private CategoriesController controllerCategory;

        [TestInitialize]
        public void SetUp()
        {
            var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AvaliacaoNonlinear;integrated security=True;");
            //var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=DESKTOP-NBVUSVN\\SQLEXPRESS;Database=AvaliacaoNonlinear;integrated security=True;");
            db = new BaseContext(builder.Options);
            this.controllerProduct = new ProductsController(db);
            this.controllerCategory = new CategoriesController(db);
        }

        [TestMethod]
        public void InsertCategories()
        {
            var cat1 = new Category { Name = "Cat3" };
            var cat2 = new Category { Name = "Cat4" };
            
            var actionResult = controllerCategory.SaveCategory(cat1);
            var okObjectResult = actionResult as OkObjectResult;
            cat1 = okObjectResult.Value as Category;

            actionResult = controllerCategory.SaveCategory(cat2);
            okObjectResult = actionResult as OkObjectResult;
            cat2 = okObjectResult.Value as Category;

            Assert.AreNotEqual(cat1.Id, 0);
            Assert.AreNotEqual(cat2.Id, 0);
        }

        public int GetGategoryByName(string name)
        {
            var actionResult = controllerCategory.GetByName(name);
            var okObjectResult = actionResult as OkObjectResult;
            var model = okObjectResult.Value as Category;
            return model.Id;
        }

        [TestMethod]
        public void InsertProduct()
        {
            var product = new Product();
            product.CategoryId = GetGategoryByName("Cat3");
            product.Name = "Product4";
            product.Price = 99.9m;

            var product2 = new Product();
            product2.CategoryId = GetGategoryByName("Cat4");
            product2.Name = "Product5";
            product2.Price = 49.9m;

            var product3 = new Product();
            product3.CategoryId = GetGategoryByName("Cat4");
            product3.Name = "Product6";
            product3.Price = 199.9m;

            var actionResult = controllerProduct.SaveProduct(product);
            var okObjectResult = actionResult as OkObjectResult;
            product = okObjectResult.Value as Product;

            actionResult = controllerProduct.SaveProduct(product2);
            okObjectResult = actionResult as OkObjectResult;
            product2 = okObjectResult.Value as Product;

            actionResult = controllerProduct.SaveProduct(product3);
            okObjectResult = actionResult as OkObjectResult;
            product3 = okObjectResult.Value as Product;

            Assert.AreNotEqual(product.Id, 0);
            Assert.AreNotEqual(product2.Id, 0);
            Assert.AreNotEqual(product3.Id, 0);
        }

        [TestMethod]
        public void GetProducts()
        {
            var products = controllerProduct.GetProducts();

            Assert.AreEqual(products.Count, 6);
        }

    }
}
