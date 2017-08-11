using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartApi.Controllers;
using ShoppingCartCore.Infra;
using ShoppingCartCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCartTest.Api
{
    [TestClass]
    public class ApiSaleTest
    {
        private BaseContext db;
        private PaymentsController controllerPayment;
        private ProductsController controllerProduct;
        private CartsController controllerCart;
        private CartItemsController controllerCartItem;
        private SalesController controllerSale;

        [TestInitialize]
        public void SetUp()
        {
            var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AvaliacaoNonlinear;integrated security=True;");
            //var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=DESKTOP-NBVUSVN\\SQLEXPRESS;Database=AvaliacaoNonlinear;integrated security=True;");
            db = new BaseContext(builder.Options);
            this.controllerProduct = new ProductsController(db);
            this.controllerSale = new SalesController(db);
            this.controllerPayment = new PaymentsController(db);
            this.controllerCart = new CartsController(db);
            this.controllerCartItem = new CartItemsController(db);
        }

        [TestMethod]
        public void InsertPayments()
        {
            var pay1 = new Payment { Name = "Pay1" };
            var pay2 = new Payment { Name = "Pay2" };
            
            var actionResult = controllerPayment.SavePayment(pay1);
            var okObjectResult = actionResult as OkObjectResult;
            pay1 = okObjectResult.Value as Payment;

            actionResult = controllerPayment.SavePayment(pay2);
            okObjectResult = actionResult as OkObjectResult;
            pay2 = okObjectResult.Value as Payment;            

            Assert.AreNotEqual(pay1.Id, 0);
            Assert.AreNotEqual(pay2.Id, 0);
        }

        public int GetProductByName(string name)
        {
            var actionResult = controllerProduct.GetByName(name);
            var okObjectResult = actionResult as OkObjectResult;
            var model = okObjectResult.Value as Product;
            return model.Id;
        }

        public int GetPaymentByName(string name)
        {
            var actionResult = controllerPayment.GetByName(name);
            var okObjectResult = actionResult as OkObjectResult;
            var model = okObjectResult.Value as Payment;
            return model.Id;
        }

        [TestMethod]
        public void SellItems()
        {
            var cart = new Cart();
            var actionResult = controllerCart.SaveCart(cart);
            var okObjectResult = actionResult as OkObjectResult;
            cart = okObjectResult.Value as Cart;

            var cartItem = new CartItem();
            cartItem.CartId = cart.Id;
            cartItem.ProductId = GetProductByName("Product1");
            cartItem.Quantity = 2;
            actionResult = controllerCartItem.SaveCartItem(cartItem);
            okObjectResult = actionResult as OkObjectResult;
            cartItem = okObjectResult.Value as CartItem;

            var cartItem2 = new CartItem();
            cartItem2.CartId = cart.Id;
            cartItem2.ProductId = GetProductByName("Product2");
            cartItem2.Quantity = 1;
            actionResult = controllerCartItem.SaveCartItem(cartItem2);
            okObjectResult = actionResult as OkObjectResult;
            cartItem2 = okObjectResult.Value as CartItem;

            actionResult = controllerCartItem.GetByCart(cart.Id);
            okObjectResult = actionResult as OkObjectResult;
            var model = okObjectResult.Value as List<CartItem>;
            var total = model.Sum(c => c.product.Price * c.Quantity);

            var sale = new Sale();
            sale.CartId = cart.Id;
            sale.PaymentId = GetPaymentByName("Pay1");
            sale.DtSale = DateTime.Now;
            sale.Total = total;
            actionResult = controllerSale.SaveSale(sale);
            okObjectResult = actionResult as OkObjectResult;
            sale = okObjectResult.Value as Sale;

            Assert.AreNotEqual(cart.Id, 0);
            Assert.AreNotEqual(cartItem.Id, 0);
            Assert.AreNotEqual(cartItem2.Id, 0);
            Assert.AreNotEqual(sale.Id, 0);

        }
    }
}
