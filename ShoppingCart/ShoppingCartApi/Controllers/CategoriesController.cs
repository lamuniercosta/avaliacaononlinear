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
    public class CategoriesController : Controller
    {
        private BaseContext db;
        private GenericRepository<Category> repo;

        public CategoriesController(BaseContext _db)
        {
            this.db = _db;
            this.repo = new GenericRepository<Category>(db);
        }

        // GET: api/values
        [Route("/api/Category/GetCategories/")]
        [HttpGet]
        public List<Category> GetCategories()
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
        [Route("/api/Category/GetByName/{name}")]
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
        public IActionResult SaveCategory([FromBody]Category cat)
        {
            try
            {
                Category newCat = new Category();
                newCat.Name = cat.Name;
                if (cat.Id == 0)
                {
                    repo.Save(newCat);
                }
                else
                {
                    repo.Update(newCat);
                }                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return Ok("Ok");
        }
    }
}
