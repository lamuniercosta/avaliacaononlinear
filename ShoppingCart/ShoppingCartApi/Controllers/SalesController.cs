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
    public class SalesController : Controller
    {
        private BaseContext db;
        private GenericRepository<Sale> repo;
        private GenericRepository<Cart> repoC;

        public SalesController(BaseContext _db)
        {
            this.db = _db;
            this.repo = new GenericRepository<Sale>(db);
            this.repoC = new GenericRepository<Cart>(db);
        }

        // GET: api/values
        [Route("/api/Sale/GetSales/")]
        [HttpGet]
        public List<Sale> GetSales()
        {
            return repo.GetAll().Include(s=>s.cart).Include(s=>s.payment).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var retorno = repo.GetAll().Include(s => s.payment).Where(s=>s.Id == id).FirstOrDefault();
            if (retorno == null)
            {
                return NotFound();
            }
            return Ok(retorno);
        }

        // GET api/values/5
        [HttpGet]
        [Route("/api/Sale/GetCartById/{id}")]
        public IActionResult GetCartById(int id)
        {
            var retorno = repoC.GetById(id);
            if (retorno == null)
            {
                return NotFound();
            }
            return Ok(retorno);
        }

        // POST api/values
        [HttpPost("[action]")]
        public IActionResult SaveSale([FromBody]Sale item)
        {
            try
            {
                item.DtSale = DateTime.Now;
                repo.Save(item);
                var cart = repoC.GetById(item.CartId);
                cart.SaleId = item.Id;
                repoC.Update(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return Ok(item);
        }
    }
}
