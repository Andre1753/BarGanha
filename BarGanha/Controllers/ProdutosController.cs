﻿using System;
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

        public IList<Produto> Produtos { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task<IActionResult> Index(string? searchString, int? categoria)
        {
            if (User.Identity.Name != null)
            {
                Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

                ViewBag.produtos = await _context.Produtos.Where(p => p.UsuarioId != usuario.Id)
                                                          .Where(p => p.Troca == false)
                                                          .Where(p => p.Anunciar == true)
                                                          .ToListAsync();
                if (searchString != null)
                {
                    ViewBag.produtos = await _context.Produtos.Where(p => p.UsuarioId != usuario.Id)
                                                              .Where(p => p.Anunciar == true)
                                                              .Where(p => p.Troca == false)
                                                              .Where(p => p.NomeProduto.Contains(searchString))
                                                              .ToListAsync();

                    ViewBag.searchString = searchString;
                }

                if (categoria != null)
                {
                    ViewBag.produtos = await _context.Produtos.Where(p => p.UsuarioId != usuario.Id)
                                                              .Where(p => p.Anunciar == true)
                                                              .Where(p => p.Troca == false)
                                                              .Where(p => p.CategoriaId == categoria)
                                                              .ToListAsync();

                    ViewBag.categoria = categoria;
                }
            }
            else
            {
                ViewBag.produtos = await _context.Produtos.Where(p => p.Anunciar == true)
                                                         .Where(p => p.Troca == false)
                                                          .ToListAsync();
                if (searchString != null)
                {
                    ViewBag.produtos = await _context.Produtos.Where(p => p.Anunciar == true)
                                                              .Where(p => p.Troca == false)
                                                              .Where(p => p.NomeProduto.Contains(searchString))
                                                              .ToListAsync();

                    ViewBag.searchString = searchString;
                }

                if (categoria != null)
                {
                    ViewBag.produtos = await _context.Produtos.Where(p => p.Anunciar == true)
                                                              .Where(p => p.Troca == false)
                                                              .Where(p => p.CategoriaId == categoria)
                                                              .ToListAsync();

                    ViewBag.categoria = categoria;
                }
            }

            return View();
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { 
                return NotFound();
            }

            var produto = await _context.Produtos.FirstOrDefaultAsync(u => u.ProdutoId == id);

            Usuario dono = await _usuarioRepositorio.PegarUsuarioPeloId(produto.UsuarioId);

            ViewBag.produto = produto;
            ViewBag.dono = dono;

            if (produto == null)
            {
                return NotFound();
            }
            return View();
        }

        public async Task<IActionResult> MyProducts()
        {
            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

            var produtos = await _context.Produtos.Where(p => p.UsuarioId == usuario.Id).Where(p=>p.Troca == false)
                                                  .ToListAsync();
            ViewBag.meusProdutos = produtos;

            return View();
        }
        public async Task<IActionResult> Produtosconta(string id)
        {
            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloId(id);
            ViewBag.Id = usuario.Id;
            ViewBag.Nome = usuario.UserName;
            return View(await _context.Produtos.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categorias = _context.Categorias.ToList();

            ViewBag.estados = new string[] {
                "AC",
                "AL",
                "AP",
                "AM",
                "BA",
                "CE",
                "DF",
                "ES",
                "GO",
                "MA",
                "MT",
                "MS",
                "MG",
                "PA",
                "PB",
                "PR",
                "PE",
                "PI",
                "RJ",
                "RN",
                "RS",
                "RO",
                "RR",
                "SC",
                "SP",
                "SE",
                "TO"
            };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel model)
        {
            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = _context.Categorias.ToList();
                ViewBag.estados = new string[] {
                    "AC",
                    "AL",
                    "AP",
                    "AM",
                    "BA",
                    "CE",
                    "DF",
                    "ES",
                    "GO",
                    "MA",
                    "MT",
                    "MS",
                    "MG",
                    "PA",
                    "PB",
                    "PR",
                    "PE",
                    "PI",
                    "RJ",
                    "RN",
                    "RS",
                    "RO",
                    "RR",
                    "SC",
                    "SP",
                    "SE",
                    "TO"
                };
                return View();
            }
            string nomeUnicoArquivo = UploadedFile(model);
            Produto prod = new Produto
            {
                NomeProduto = model.NomeProduto,
                Preco = model.Preco,
                Anunciar = true,
                Troca = false,
                Descricao = model.Descricao,
                ImagemProduto = nomeUnicoArquivo,
                UsuarioId = usuario.Id,
                CategoriaId = model.CategoriaId,
                Cidade = model.Cidade,
                Estado = model.Estado
            };
            _context.Add(prod);
            await _context.SaveChangesAsync();
            return RedirectToAction("MyProducts");
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

            EditProdutoViewModel epvm = new EditProdutoViewModel(); //ViewModel
            epvm.ProdutoId = produto.ProdutoId;
            epvm.NomeProduto = produto.NomeProduto;
            epvm.Preco = produto.Preco;
            epvm.CategoriaId = produto.CategoriaId;
            epvm.Descricao = produto.Descricao;

            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.ImgProduto = produto.ImagemProduto;
            return View(epvm);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ProdutoId,NomeProduto,Preco,Descricao,CategoriaId")] EditProdutoViewModel produto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = _context.Categorias.ToList();
                var prod1 = _context.Produtos.Find(produto.ProdutoId);
                ViewBag.ImgProduto = prod1.ImagemProduto;
                return View();
            }

            var prod = _context.Produtos.Find(produto.ProdutoId);
            if (produto == null)
            {
                return NotFound();
            }
            prod.NomeProduto = produto.NomeProduto;
            prod.Preco = produto.Preco;
            prod.Descricao = produto.Descricao;
            prod.CategoriaId = produto.CategoriaId;

            _context.SaveChanges();

            return RedirectToAction("MyProducts");
        }

        public async Task<IActionResult> ChangeAnunciar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FirstOrDefaultAsync(m => m.ProdutoId == id);

            produto.Anunciar = !produto.Anunciar;

            _context.SaveChanges();

            return RedirectToAction("MyProducts");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyProducts");
        }

        public async Task<IActionResult> EditAdm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FirstOrDefaultAsync(m => m.ProdutoId == id);

            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        public async Task OnGetAsync()
        {
            var produtos = from m in _context.Produtos
                           select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                produtos = produtos.Where(s => s.NomeProduto.Contains(SearchString));
            }

            Produtos = await produtos.ToListAsync();
        }
    }

}

