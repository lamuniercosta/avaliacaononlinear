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
    public class SalesController : Controller
    {
        private BaseContext db;
        private GenericRepository<Sale> repo;

        public SalesController(BaseContext _db)
        {
            this.db = _db;
            this.repo = new GenericRepository<Sale>(db);
        }

        // GET: api/values
        [Route("/api/Sale/GetSales/")]
        [HttpGet]
        public List<Sale> GetSales()
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
        public IActionResult SaveSale([FromBody]Sale item)
        {
            Sale newItem = new Sale();
            try
            {
                newItem.CartId = item.CartId;
                newItem.PaymentId = item.PaymentId;
                newItem.DtSale = item.DtSale;
                newItem.Total = item.Total;
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
    }
}
