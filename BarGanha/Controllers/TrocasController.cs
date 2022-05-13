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
                        
            var trocasEnviadas = await _context.Trocas.Where(t => t.UserOfertaId == usuario.Id).ToListAsync();

            foreach (Troca trocaEnv in trocasEnviadas)
            {
                _context.Ofertas.Where(o => o.OfertaId == trocaEnv.OfertaId).Include(o => o.produtosOfertados).Include(o => o.produtosSelecionados).Include(o => o.UserDono).Load();

                foreach (ProdutoOfertado pO in trocaEnv.Oferta.produtosOfertados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pO.ProdutoId).Load();
                }
                foreach (ProdutoSelecionado pS in trocaEnv.Oferta.produtosSelecionados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pS.ProdutoId).Load();
                }
            }

            var trocasRecebidas = await _context.Trocas.Where(t => t.UserDonoId == usuario.Id).ToListAsync();

            foreach (Troca trocaRec in trocasRecebidas)
            {
                _context.Ofertas.Where(o => o.OfertaId == trocaRec.OfertaId).Include(o => o.produtosOfertados).Include(o => o.produtosSelecionados).Include(o => o.UserOferta).Load();

                foreach (ProdutoOfertado pO1 in trocaRec.Oferta.produtosOfertados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pO1.ProdutoId).Load();
                }
                foreach (ProdutoSelecionado pS1 in trocaRec.Oferta.produtosSelecionados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pS1.ProdutoId).Load();
                }
            }
            /*
            var ofertas = await _context.Ofertas.Where(o => o.UserDonoId == usuario.Id).Where(o => o.Status == 2).Include(o => o.produtosOfertados).ToListAsync();
            foreach (Oferta oferta in ofertas)
            {
                //_context.Produtos.Where(p => p.ProdutoId == oferta.ProdutoId).Load();

                foreach (ProdutoOfertado pO in oferta.produtosOfertados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pO.ProdutoId).Load();
                }
            }
            */

            ViewBag.trocasEnviadas = trocasEnviadas;
            ViewBag.trocasRecebidas = trocasRecebidas;

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


        public async Task<ActionResult> Delete(int id)
        {
            
            var troca = await _context.Trocas
                        .FirstAsync(t => t.TrocaId == id);

            //_context.Ofertas.Where(o => o.OfertaId == troca.OfertaId).Include(o => o.Produto).Include(o => o.produtosOfertados).Load();

            troca.Oferta.Status = 3;
            //troca.Oferta.Produto.Troca = false;

            foreach (ProdutoOfertado pO in troca.Oferta.produtosOfertados)
            {
                _context.Produtos.Where(p => p.ProdutoId == pO.ProdutoId).Load();
                pO.produto.Troca = false;
            }

            _context.Trocas.Remove(troca);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult DeleteByOferta(int id)
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
