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
    public class ProductsController : Controller
    {
        private BaseContext db;
        private GenericRepository<Product> repo;

        public ProductsController(BaseContext _db)
        {
            this.db = _db;
            this.repo = new GenericRepository<Product>(db);
        }

        // GET: api/values
        [Route("/api/Product/GetProducts/")]
        [HttpGet]
        public List<Product> GetProducts()
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
        [Route("/api/Product/GetByName/{name}")]
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
        public IActionResult SaveProduct([FromBody]Product product)
        {
            Product newProduct = new Product();
            try
            {
                newProduct.CategoryId = product.CategoryId;
                newProduct.Name = product.Name;
                newProduct.Price = product.Price;
                if (product.Id == 0)
                {
                    repo.Save(newProduct);
                }
                else
                {
                    repo.Update(newProduct);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return Ok(newProduct);
        }
    }
}
