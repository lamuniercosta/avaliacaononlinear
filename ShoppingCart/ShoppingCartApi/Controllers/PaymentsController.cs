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
    public class PaymentsController : Controller
    {
        private BaseContext db;
        private GenericRepository<Payment> repo;

        public PaymentsController(BaseContext _db)
        {
            this.db = _db;
            this.repo = new GenericRepository<Payment>(db);
        }

        // GET: api/values
        [Route("/api/Payment/GetPayments/")]
        [HttpGet]
        public List<Payment> GetPayments()
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

        // GET api/values/5
        [Route("/api/Payment/GetByName/{name}")]
        [HttpGet]
        public IActionResult GetByName(string name)
        {
            var retorno = repo.Where(x => x.Name == name).FirstOrDefault();
            if (retorno == null)
            {
                return NotFound();
            }
            return Ok(retorno);
        }

        // POST api/values
        [HttpPost("[action]")]
        public IActionResult SavePayment([FromBody]Payment pay)
        {
            Payment newPay = new Payment();
            try
            {
                newPay.Name = pay.Name;                
                if (pay.Id == 0)
                {
                    repo.Save(newPay);
                }
                else
                {
                    repo.Update(newPay);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return Ok(newPay);
        }
    }
}
