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

            var ofertasEnviadas = await _context.Ofertas.Where(o => o.UserOfertaId == usuario.Id).Where(o => o.Status != 2).Include(o => o.produtosOfertados).Include(o => o.produtosSelecionados).Include(o => o.UserDono).ToListAsync();
            foreach (Oferta oferta in ofertasEnviadas)
            {
                foreach (ProdutoOfertado pO in oferta.produtosOfertados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pO.ProdutoId).Load();
                }

                foreach (ProdutoSelecionado pS in oferta.produtosSelecionados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pS.ProdutoId).Load();
                }
            }

            var ofertasRecebidas = await _context.Ofertas.Where(o => o.UserDonoId == usuario.Id).Where(o => o.Status != 2).Include(o => o.produtosOfertados).Include(o => o.produtosSelecionados).Include(o => o.UserOferta).ToListAsync();
            foreach (Oferta oferta1 in ofertasRecebidas)
            {
                foreach (ProdutoOfertado pO1 in oferta1.produtosOfertados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pO1.ProdutoId).Load();
                }

                foreach (ProdutoSelecionado pS1 in oferta1.produtosSelecionados)
                {
                    _context.Produtos.Where(p => p.ProdutoId == pS1.ProdutoId).Load();
                }
            }

            ViewBag.ofertasEnviadas = ofertasEnviadas;
            ViewBag.ofertasRecebidas = ofertasRecebidas;

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

            var meusProdutos = await _context.Produtos.Where(p => p.UsuarioId == usuario.Id).ToListAsync();

            var produtos = await _context.Produtos.Where(p => p.UsuarioId == produto.UsuarioId).Where(p => p.Anunciar == true).ToListAsync();

            var user = await _usuarioRepositorio.PegarUsuarioPeloId(produto.UsuarioId);

            ViewBag.user = user;
            ViewBag.produtos = produtos;
            ViewBag.meusProdutos = meusProdutos;

            return View();
        }

        // POST: SolicitacoesTrocasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfertaViewModel model)
        {
            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);
            var meusprodutos = await _context.Produtos.Where(p => p.UsuarioId == usuario.Id).ToListAsync();

            Usuario dono = await _usuarioRepositorio.PegarUsuarioPeloId(model.usuarioId);
            var produtosDono = await _context.Produtos.Where(p => p.UsuarioId == dono.Id).Where(p => p.Anunciar == true).ToListAsync();

            Oferta ofer = new Oferta
            {
                //ProdutoId = model.produtoId,
                Status = 1,
                UserDonoId = dono.Id,
                UserOfertaId = usuario.Id
            };
            _context.Add(ofer);
            await _context.SaveChangesAsync();

            int i = 0;
            foreach (var produto in meusprodutos)
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

            int j = 0;
            foreach (var prod in produtosDono)
            {
                if (model.Selecionados[j].Selected)
                {
                    ProdutoSelecionado pds = new ProdutoSelecionado
                    {
                        OfertaId = ofer.OfertaId,
                        ProdutoId = prod.ProdutoId
                    };
                    _context.Add(pds);
                    await _context.SaveChangesAsync();
                }
                j++;
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
            var ofertas = await _context.Ofertas.Where(o => o.OfertaId == id).Include(o => o.produtosOfertados).Include(o => o.produtosSelecionados).ToListAsync();
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
            foreach (ProdutoSelecionado prodSel in oferta.produtosSelecionados)
            {
                var prod1 = await _context.Produtos.Where(p => p.ProdutoId == prodSel.ProdutoId).ToListAsync();
                prod1[0].Troca = true;
            }

            oferta.Status = 2;

            //var produto = await _context.Produtos.FirstOrDefaultAsync(m => m.ProdutoId == oferta.ProdutoId);

            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

            //produto.Troca = true;

            Troca troc = new Troca
            {
                OfertaId = oferta.OfertaId,
                UserDonoId = oferta.UserDonoId,
                UserOfertaId = oferta.UserOfertaId
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
