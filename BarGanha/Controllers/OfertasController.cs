using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BarGanha.BLL.Models;
using BarGanha.DAL;
using BarGanha.DAL.Interfaces;
using BarGanha.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BarGanha.Controllers
{
    public class OfertasController : Controller
    {
        private readonly Contexto _context;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IWebHostEnvironment webHostEnvironment;

        public OfertasController(Contexto context, IWebHostEnvironment hostEnvironment, IUsuarioRepositorio usuarioRepositorio)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
            _usuarioRepositorio = usuarioRepositorio;
        }
        // GET: SolicitacoesTrocasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SolicitacoesTrocasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SolicitacoesTrocasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SolicitacoesTrocasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfertaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Oferta solicitacaoTroca = new Oferta
                {
                    ProdutoId = model.ProdutoId,
                    ProdutoOfertadoId = model.ProdutoOfertadoId
                };
                _context.Add(solicitacaoTroca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

        // GET: SolicitacoesTrocasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SolicitacoesTrocasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SolicitacoesTrocasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SolicitacoesTrocasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
