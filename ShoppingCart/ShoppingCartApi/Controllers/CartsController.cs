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

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("SiteCorsPolicy")]
    public class CartsController : Controller
    {
        private BaseContext db;
        private GenericRepository<Cart> repo;

        public CartsController(BaseContext _db)
        {
            this.db = _db;
            this.repo = new GenericRepository<Cart>(db);
        }

        // GET: api/values
        [Route("/api/Cart/GetCarts/")]
        [HttpGet]
        public List<Cart> GetCarts()
        {
            return repo.GetAll();
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

        // POST api/values
        [HttpPost("[action]")]
        public IActionResult SaveCart([FromBody]Cart cart)
        {
            Cart newCart = new Cart();
            try
            {
                newCart.SaleId = cart.SaleId;
                if (cart.Id == 0)
                {
                    repo.Save(newCart);
                }
                else
                {
                    repo.Update(newCart);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return Ok(newCart);
        }
    }
}
