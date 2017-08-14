using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingCartWeb.Controllers
{
    public class SaleController : Controller
    {
        // GET: /<controller>/
        [Route("/Sale/{id}")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/Sale/Success/{id}")]
        public IActionResult Success()
        {
            return View();
        }
    }
}
