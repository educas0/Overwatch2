using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Overwatch2.Models;
using Overwatch2.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Overwatch2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpContextAccessor _contextoHttp;
        private IDBAccess _servicioSqlServer;


        public HomeController(ILogger<HomeController> logger,
                                Models.Interfaces.IDBAccess servicioInyectado,
                                IHttpContextAccessor contextoHttp)
        {
            _logger = logger;
            _servicioSqlServer = servicioInyectado;
            _contextoHttp = contextoHttp;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult Pagina2()
        {
            return View();
        }



    }
}
