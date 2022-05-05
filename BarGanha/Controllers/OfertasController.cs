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
        public async Task<ActionResult> Index()
        {
            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

            var ofertas = await _context.Ofertas.Where(o => o.UsuarioId == usuario.Id).Include(o => o.produtosOfertados).ToListAsync();
            foreach (Oferta oferta in ofertas)
            {
                foreach (ProdutoOfertado pO in oferta.produtosOfertados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pO.ProdutoId).Load();
                }

                var prod = await _context.Produtos
                    .FirstOrDefaultAsync(m => m.ProdutoId == oferta.ProdutoId);
            }

            _context.Produtos.Where(p => p.UsuarioId == usuario.Id).Load();

            if (usuario.Produtos != null)
            {
                foreach (Produto meuProduto in usuario.Produtos)
                {
                    var pt = meuProduto;
                    _context.Ofertas.Where(o => o.ProdutoId == meuProduto.ProdutoId).Load();

                    if (meuProduto.Ofertas != null)
                    {
                        foreach (Oferta oferta in meuProduto.Ofertas)
                        {
                            _context.ProdutosOfertados.Where(pO => pO.OfertaId == oferta.OfertaId).Load();

                            foreach (ProdutoOfertado prodOferta in oferta.produtosOfertados)
                            {
                                _context.Produtos.Where(p => p.ProdutoId == prodOferta.ProdutoId).Load();
                            }
                        }
                    }
                }
            }

            ViewBag.ofertas = ofertas;
            ViewBag.usuario = usuario;

            return View();
        }

        // GET: SolicitacoesTrocasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SolicitacoesTrocasController/Create
        public async Task<ActionResult> Create(int id)
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);

            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

            var produtos = await _context.Produtos.Where(p => p.UsuarioId == usuario.Id).ToListAsync();

            ViewBag.produto = produto;
            ViewBag.meusProdutos = produtos;

            return View();
        }

        // POST: SolicitacoesTrocasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfertaViewModel model)
        {

            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

            var produtos = await _context.Produtos.Where(p => p.UsuarioId == usuario.Id).ToListAsync();


            Oferta ofer = new Oferta
            {
                ProdutoId = model.produtoId,
                Status = 1,
                UsuarioId = usuario.Id
            };
            _context.Add(ofer);
            await _context.SaveChangesAsync();

            int i = 0;
            foreach (var produto in produtos)
            {
                if (model.Ofertas[i].Selected)
                {
                    ProdutoOfertado pdf = new ProdutoOfertado
                    {
                        OfertaId = ofer.OfertaId,
                        ProdutoId = produto.ProdutoId
                    };
                    _context.Add(pdf);
                    await _context.SaveChangesAsync();
                }
                i++;
            }

            return RedirectToAction("Index");
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


        public async Task<IActionResult> Delete(int? id)
        {
            var oferta = await _context.Ofertas.FindAsync(id);

            var produtosOfertados = await _context.ProdutosOfertados.Where(pO => pO.OfertaId == oferta.OfertaId).ToListAsync();

            foreach (ProdutoOfertado prodOfertado in produtosOfertados)
            {
                _context.ProdutosOfertados.Remove(prodOfertado);
            }

            _context.Ofertas.Remove(oferta);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Aprovar(int? id)
        {
            var ofertas = await _context.Ofertas.Where(o => o.OfertaId == id).Include(o => o.produtosOfertados).ToListAsync();
            var oferta = ofertas[0];

            if (oferta == null)
            {
                return NotFound();
            }

            foreach (ProdutoOfertado prodOferta in oferta.produtosOfertados)
            {
                var prod = await _context.Produtos.Where(p => p.ProdutoId == prodOferta.ProdutoId).ToListAsync();
                prod[0].Troca = true;
            }

            oferta.Status = 2;

            var produto = await _context.Produtos
                            .FirstOrDefaultAsync(m => m.ProdutoId == oferta.ProdutoId);

            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

            produto.Troca = true;

            Troca troc = new Troca
            {
                OfertaId = oferta.OfertaId,
                UsuarioId = usuario.Id
            };
            _context.Add(troc);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Reprovar(int? id)
        {
            var oferta = await _context.Ofertas.FindAsync(id);

            if (oferta == null)
            {
                return NotFound();
            }

            oferta.Status = 3;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
