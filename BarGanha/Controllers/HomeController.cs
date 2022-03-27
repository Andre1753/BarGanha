using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BarGanha.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarGanha.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SobreNos()
        {
            return View();
        }
        public IActionResult ContateNos()
        {
            return View();
        }
        public IActionResult Profilepage()
        {
            return View();
        }
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult Cadastroproduto()
        {
            return View();
        }
        public IActionResult Productpage()
        {
            return View();
        }
        public IActionResult MinhaConta()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
