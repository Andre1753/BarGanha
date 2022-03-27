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
using Microsoft.EntityFrameworkCore;

namespace BarGanha.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly Contexto _context;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProdutosController(Contexto context, IWebHostEnvironment hostEnvironment, IUsuarioRepositorio usuarioRepositorio)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IActionResult> TodosProdutos()
        {
            return View(await _context.Produtos.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(u => u.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        public IActionResult Cadastrar()
        {
            ViewBag.Categorias = _context.Categorias;
            return View();
        }

        public async Task<IActionResult> Productpage(string searchString)
        {
            ViewBag.Ss = searchString;
            return View(await _context.Produtos.ToListAsync());
        }
        public async Task<IActionResult> MeusProdutos()
        {
            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);
            ViewBag.Id = usuario.Id;
            return View(await _context.Produtos.ToListAsync());
        }
        public async Task<IActionResult> Produtosconta(string id)
        {
            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloId(id);
            ViewBag.Id = usuario.Id;
            ViewBag.Nome = usuario.UserName;
            return View(await _context.Produtos.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(ProdutoViewModel model)
        {
            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

            if (ModelState.IsValid)
            {
                string nomeUnicoArquivo = UploadedFile(model);

                Produto prod = new Produto
                {
                    NomeProduto = model.NomeProduto,
                    Preco = model.Preco,
                    Descricao = model.Descricao,
                    ImagemProduto = nomeUnicoArquivo,
                    UsuarioId = usuario.Id,
                    CategoriaId = model.CategoriaId,
                };
                _context.Add(prod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string UploadedFile(ProdutoViewModel model)
        {
            string nomeUnicoArquivo = null;

            if (model.ImagemProduto != null)
            {
                string pastaFotos = Path.Combine(webHostEnvironment.WebRootPath, "images");
                nomeUnicoArquivo = Guid.NewGuid().ToString() + "_" + model.ImagemProduto.FileName;
                string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo);
                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    model.ImagemProduto.CopyTo(fileStream);
                }
            }
            return nomeUnicoArquivo;
        }
        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);

            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ProdutoId,NomeProduto,Preco,Descricao,")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                var prod = _context.Produtos.Find(produto.ProdutoId);
                if (produto == null)
                {
                    return NotFound();
                }
                prod.NomeProduto = produto.NomeProduto;
                prod.Preco = produto.Preco;
                prod.Descricao = produto.Descricao;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(produto);
        }
        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        public IActionResult Shop()
        {
            return View();
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }
        public async Task<IActionResult> EditAdm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);

            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }        
    }

}

