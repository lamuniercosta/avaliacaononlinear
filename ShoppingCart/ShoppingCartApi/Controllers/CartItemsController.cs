using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using ShoppingCartCore.Infra;
using ShoppingCartCore.Models;
using ShoppingCartCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("SiteCorsPolicy")]
    public class CartItemsController : Controller
    {
        private BaseContext db;
        private GenericRepository<CartItem> repo;

        public CartItemsController(BaseContext _db)
        {
            this.db = _db;
            this.repo = new GenericRepository<CartItem>(db);
        }

        // GET: api/values
        [Route("/api/CartItem/GetCartItems/")]
        [HttpGet]
        public List<CartItem> GetCartItems()
        {
            return repo.GetAll().Include(c=>c.cart).Include(c=>c.product).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var retorno = repo.GetById(id);
            if (retorno == null)
            {
                return NotFound();
            }
            return Ok(retorno);
        }

        // GET: api/values
        [Route("/api/CartItem/GetByCart/")]
        [HttpGet]
        public IActionResult GetByCart(int cartId)
        {
            var retorno = repo.Where(c=>c.CartId == cartId);
            if (retorno == null)
            {
                return NotFound();
            }
            return Ok(retorno);
        }

        // POST api/values
        [HttpPost("[action]")]
        public IActionResult SaveCartItem([FromBody]CartItem item)
        {
            CartItem newItem = new CartItem();
            try
            {
                newItem.CartId = item.CartId;
                newItem.ProductId = item.ProductId;
                newItem.Quantity = item.Quantity;
                if (item.Id == 0)
                {
                    repo.Save(newItem);
                }
                else
                {
                    repo.Update(newItem);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return Ok(newItem);
        }

        // DELETE api/values/5
        [HttpPost("[action]")]
        public IActionResult DeleteCartItem([FromBody]int id)
        {
            try
            {
                var cartItem = repo.GetById(id);
                if (cartItem == null)
                {
                    return NotFound();
                }
                repo.Delete(cartItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return Ok("Ok");
        }
    }
}
