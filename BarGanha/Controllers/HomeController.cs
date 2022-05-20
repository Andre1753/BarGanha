using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BarGanha.DAL;
using BarGanha.DAL.Interfaces;
using BarGanha.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BarGanha.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Contexto _context;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, Contexto context, IWebHostEnvironment hostEnvironment, IUsuarioRepositorio usuarioRepositorio)
        {
            _logger = logger;
            _context = context;
            webHostEnvironment = hostEnvironment;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.produtos = await _context.Produtos.Where(p => p.Anunciar == true).Where(p => p.Troca == false).Take(4).ToListAsync();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
