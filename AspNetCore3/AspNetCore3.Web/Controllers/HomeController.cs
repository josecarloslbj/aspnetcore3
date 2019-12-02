using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore3.Web.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
     
        public IActionResult Index()
        {
            return Ok(new { retorno = "Aplicação esta rodando" });
        }
    }
}