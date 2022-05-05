using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BarGanha.BLL.Models;

using BarGanha.DAL;
using BarGanha.DAL.Interfaces;
using BarGanha.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BarGanha.Controllers
{
    public class TrocasController : Controller
    {
        private readonly Contexto _context;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TrocasController(Contexto context, IWebHostEnvironment hostEnvironment, IUsuarioRepositorio usuarioRepositorio)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
            _usuarioRepositorio = usuarioRepositorio;
        }

        // GET: TrocasController
        public async Task<ActionResult> Index()
        {
            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

            var trocas = await _context.Trocas.Where(t => t.UsuarioId == usuario.Id).ToListAsync();

            foreach (Troca troca in trocas)
            {
                _context.Ofertas.Where(o => o.OfertaId == troca.OfertaId).Include(o => o.produtosOfertados).Load();
                _context.Produtos.Where(p => p.ProdutoId == troca.Oferta.ProdutoId).Load();

                foreach (ProdutoOfertado pO in troca.Oferta.produtosOfertados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pO.ProdutoId).Load();
                }
            }

            var ofertas = await _context.Ofertas.Where(o => o.UsuarioId == usuario.Id).Where(o => o.Status == 2).Include(o => o.produtosOfertados).ToListAsync();
            foreach (Oferta oferta in ofertas)
            {
                _context.Produtos.Where(p => p.ProdutoId == oferta.ProdutoId).Load();

                foreach (ProdutoOfertado pO in oferta.produtosOfertados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pO.ProdutoId).Load();
                }
            }

            ViewBag.trocas = trocas;
            ViewBag.ofertas = ofertas;

            return View();
        }

        // GET: TrocasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrocasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrocasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: TrocasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrocasController/Edit/5
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

        // GET: TrocasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrocasController/Delete/5
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
