using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartCore.Infra;
using ShoppingCartCore.Models;
using ShoppingCartCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCartTest.Core
{
    [TestClass]
    public class SaleTest
    {
        private BaseContext db;
        private GenericRepository<Payment> repoPayment;
        private GenericRepository<Product> repoProduct;
        private GenericRepository<Cart> repoCart;
        private GenericRepository<CartItem> repoCartItem;
        private GenericRepository<Sale> repoSale;

        [TestInitialize]
        public void SetUp()
        {
            var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AvaliacaoNonlinear;integrated security=True;");
            //var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=DESKTOP-NBVUSVN\\SQLEXPRESS;Database=AvaliacaoNonlinear;integrated security=True;");
            db = new BaseContext(builder.Options);
            this.repoProduct = new GenericRepository<Product>(db);
            this.repoSale = new GenericRepository<Sale>(db);
            this.repoPayment = new GenericRepository<Payment>(db);
            this.repoCart = new GenericRepository<Cart>(db);
            this.repoCartItem = new GenericRepository<CartItem>(db);
        }

        [TestMethod]
        public void InsertPayments()
        {
            var pay1 = new Payment { Name = "Pay1" };
            var pay2 = new Payment { Name = "Pay2" };

            repoPayment.Save(pay1);
            repoPayment.Save(pay2);

            Assert.AreNotEqual(pay1.Id, 0);
            Assert.AreNotEqual(pay2.Id, 0);
        }

        public int GetProductByName(string name)
        {
            return repoProduct.Where(c => c.Name == name).FirstOrDefault().Id;
        }

        public int GetPaymentByName(string name)
        {
            return repoPayment.Where(c => c.Name == name).FirstOrDefault().Id;
        }

        [TestMethod]
        public void SellItems()
        {
            var cart = new Cart();
            repoCart.Save(cart);
            
            var cartItem = new CartItem();
            cartItem.CartId = cart.Id;
            cartItem.ProductId = GetProductByName("Product1");
            cartItem.Quantity = 2;
            repoCartItem.Save(cartItem);

            var cartItem2 = new CartItem();
            cartItem2.CartId = cart.Id;
            cartItem2.ProductId = GetProductByName("Product2");
            cartItem2.Quantity = 1;
            repoCartItem.Save(cartItem2);

            var total = repoCartItem.Where(c => c.CartId == cart.Id).Sum(c => c.product.Price * c.Quantity);

            var sale = new Sale();
            sale.CartId = cart.Id;
            sale.PaymentId = GetPaymentByName("Pay1");
            sale.DtSale = DateTime.Now;
            sale.Total = total;
            repoSale.Save(sale);

            Assert.AreNotEqual(cart.Id, 0);
            Assert.AreNotEqual(cartItem.Id, 0);
            Assert.AreNotEqual(cartItem2.Id, 0);
            Assert.AreNotEqual(sale.Id, 0);

        }
    }
}
