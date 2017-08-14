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
    public class CartsController : Controller
    {
        private BaseContext db;
        private GenericRepository<Cart> repo;
        private GenericRepository<CartItem> repoI;

        public CartsController(BaseContext _db)
        {
            this.db = _db;
            this.repo = new GenericRepository<Cart>(db);
            this.repoI = new GenericRepository<CartItem>(db);
        }

        // GET: api/values
        [Route("/api/Cart/GetCarts/")]
        [HttpGet]
        public List<Cart> GetCarts()
        {
            return repo.GetAll().Include(c=>c.sale).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public List<CartItem> GetById(int id)
        {
            return repoI.GetAll().Include(c => c.product).Where(c => c.CartId == id).ToList();
        }

        // POST api/values
        [HttpPost("[action]")]
        public IActionResult SaveCart([FromBody]Cart cart)
        {
            try
            {
                foreach (var item in cart.cartItems)
                {
                    item.product = null;
                }
                if (cart.Id == 0)
                {                    
                    repo.Save(cart);
                }
                else
                {
                    foreach (var item in cart.cartItems)
                    {
                        if (item.Id == 0)
                        {
                            item.CartId = cart.Id;
                            repoI.Save(item);
                        }
                    }
                    var removed = repoI.Where(c => c.CartId == cart.Id && !cart.cartItems.Select(i => i.Id).Contains(c.Id)).ToList();
                    foreach (var item in removed)
                    {
                        repoI.Delete(item);
                    }
                    repo.Update(cart);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
            return Ok(cart);
        }
    }
}
