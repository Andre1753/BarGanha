﻿using BarGanha.BLL.Models;
using BarGanha.DAL;
using BarGanha.DAL.Interfaces;
using BarGanha.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BarGanha.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Contexto _context;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IFuncaoRepositorio _funcaoRepositorio;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsuariosController(Contexto context,IUsuarioRepositorio usuarioRepositorio, IFuncaoRepositorio funcaoRepositorio, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _usuarioRepositorio = usuarioRepositorio;
            _funcaoRepositorio = funcaoRepositorio;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _usuarioRepositorio.PegarTodos());
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AtualizarAdm(string id)
        {
            Usuario usuario = await _usuarioRepositorio.PegarPeloId(id);

            if (usuario == null)
                return NotFound();

            AtualizarViewModel model = new AtualizarViewModel
            {
                UsuarioId = usuario.Id,
                Nome = usuario.UserName,
                NomeCompleto = usuario.NomeCompleto,
                CPF = usuario.CPF,
                Email = usuario.Email,
                Telefone = usuario.PhoneNumber
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarAdm(AtualizarViewModel viewModel)
        {
            if (ModelState.IsValid)
            {


                Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloId(viewModel.UsuarioId);
                var usu = _context.Usuarios.Find(usuario.Id);

                usu.UserName = viewModel.Nome;
                usu.NomeCompleto = viewModel.NomeCompleto;
                usu.CPF = viewModel.CPF;
                usu.PhoneNumber = viewModel.Telefone;
                usu.Email = viewModel.Email;

                _context.SaveChanges();

                return RedirectToAction(nameof(MinhaConta));
            }

            return View(viewModel);
        }


        [Authorize(Roles = "Administrador")]
        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var produto = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registro()
        {
            ViewBag.estados = new string[] { 
                "Acre",
                "Alagoas",
                "Amapá",
                "Amazonas",
                "Bahia",
                "Ceará",
                "Espírito Santo",
                "Goiás",
                "Maranhão",
                "Mato Grosso",
                "Mato Grosso do Sul",
                "Minas Gerais",
                "Pará",
                "Paraíba",
                "Paraná",
                "Pernambuco",
                "Piauí",
                "Rio de Janeiro",
                "Rio Grande do Norte",
                "Rio Grande do Sul",
                "Rondônia",
                "Roraima",
                "Santa Catarina",
                "São Paulo",
                "Sergipe",
                "Tocantins"
            };

            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.estados = new string[] {
                    "Acre",
                    "Alagoas",
                    "Amapá",
                    "Amazonas",
                    "Bahia",
                    "Ceará",
                    "Espírito Santo",
                    "Goiás",
                    "Maranhão",
                    "Mato Grosso",
                    "Mato Grosso do Sul",
                    "Minas Gerais",
                    "Pará",
                    "Paraíba",
                    "Paraná",
                    "Pernambuco",
                    "Piauí",
                    "Rio de Janeiro",
                    "Rio Grande do Norte",
                    "Rio Grande do Sul",
                    "Rondônia",
                    "Roraima",
                    "Santa Catarina",
                    "São Paulo",
                    "Sergipe",
                    "Tocantins"
                };

                return View(model);
            }

            Usuario usuario = new Usuario();
            IdentityResult usuarioCriado;


            if (_usuarioRepositorio.VerificarSeExisteRegistro() == 0)
            {
                usuario.UserName = model.Nome;
                usuario.NomeCompleto = model.NomeCompleto;
                usuario.CPF = model.CPF;
                usuario.Email = model.Email;
                usuario.PhoneNumber = model.Telefone;
                usuario.Logradouro = model.Logradouro;
                usuario.Bairro = model.Bairro;
                usuario.Numero = model.Numero;
                usuario.Complemento = model.Complemento;
                usuario.Cidade = model.Cidade;
                usuario.Estado = model.Estado;
                usuario.CEP = model.CEP;

                usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuario, model.Senha);

                if (usuarioCriado.Succeeded)
                {
                    await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuario, "Administrador");
                    await _usuarioRepositorio.LogarUsuario(usuario, false);
                    return View("login");
                }
            }
            usuario.NomeCompleto = model.NomeCompleto;
            usuario.UserName = model.Nome;
            usuario.CPF = model.CPF;
            usuario.Email = model.Email;
            usuario.PhoneNumber = model.Telefone;
            usuario.Logradouro = model.Logradouro;
            usuario.Bairro = model.Bairro;
            usuario.Numero = model.Numero;
            usuario.Complemento = model.Complemento;
            usuario.Cidade = model.Cidade;
            usuario.Estado = model.Estado;
            usuario.CEP = model.CEP;


            usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuario, model.Senha);

            if (usuarioCriado.Succeeded)
            {
                await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuario, "Usuario");
                return View("login");
            }

            else
            {
                foreach (IdentityError erro in usuarioCriado.Errors)
                {
                    ModelState.AddModelError("", erro.Description);
                }
                return View(model);
            }
        }    

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
                await _usuarioRepositorio.DeslogarUsuario();

            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloEmail(model.Email);
                if (usuario != null)
                {
                    PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();

                    if (passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, model.Senha) != PasswordVerificationResult.Failed)
                    {
                        await _usuarioRepositorio.LogarUsuario(usuario, false);
                        return RedirectToAction("Index", "Home");
                    }

                    else
                    {
                        ModelState.AddModelError("", "Usuario e/ou senhas inválidos");
                        return View(model);

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuario não cadastrado");
                    return View(model);

                }       

            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _usuarioRepositorio.DeslogarUsuario();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult>Informacoes(string usuarioId)
        {
            if (usuarioId == null)
            {
                return NotFound();
            }
            var usuario = await _usuarioRepositorio.PegarPeloId(usuarioId);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        public async Task<IActionResult> EntreemContato(string usuarioId)
        {
            if (usuarioId == null)
            {
                return NotFound();
            }
            var usuario = await _usuarioRepositorio.PegarPeloId(usuarioId);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MinhaConta()
        {
            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

            if (usuario == null)
                return NotFound();

            AtualizarViewModel model = new AtualizarViewModel
            {
                UsuarioId = usuario.Id,
                Nome = usuario.UserName,
                NomeCompleto = usuario.NomeCompleto,
                CPF = usuario.CPF,
                Email = usuario.Email,
                Telefone = usuario.PhoneNumber,
                Logradouro = usuario.Logradouro,
                Bairro = usuario.Bairro,
                Numero = usuario.Numero,
                Complemento = usuario.Complemento,
                Cidade = usuario.Cidade,
                Estado = usuario.Estado,
                CEP = usuario.CEP
            };

            ViewBag.estados = new string[] {
                "Acre",
                "Alagoas",
                "Amapá",
                "Amazonas",
                "Bahia",
                "Ceará",
                "Espírito Santo",
                "Goiás",
                "Maranhão",
                "Mato Grosso",
                "Mato Grosso do Sul",
                "Minas Gerais",
                "Pará",
                "Paraíba",
                "Paraná",
                "Pernambuco",
                "Piauí",
                "Rio de Janeiro",
                "Rio Grande do Norte",
                "Rio Grande do Sul",
                "Rondônia",
                "Roraima",
                "Santa Catarina",
                "São Paulo",
                "Sergipe",
                "Tocantins"
            };


            return View(model);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(AtualizarViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloNome(User);

                usuario.UserName = viewModel.Nome;
                usuario.NomeCompleto = viewModel.NomeCompleto;
                usuario.CPF = viewModel.CPF;
                usuario.PhoneNumber = viewModel.Telefone;
                usuario.Email = viewModel.Email;
                usuario.Logradouro = viewModel.Logradouro;
                usuario.Bairro = viewModel.Bairro;
                usuario.Numero = viewModel.Numero;
                usuario.Complemento = viewModel.Complemento;
                usuario.Cidade = viewModel.Cidade;
                usuario.Estado = viewModel.Estado;
                usuario.CEP = viewModel.CEP;
                _context.SaveChanges();

                return RedirectToAction(nameof(MinhaConta));
            }

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RedefinirSenha(Usuario usuario)
        {
            LoginViewModel model = new LoginViewModel
            {
                Email = usuario.Email
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloEmail(model.Email);
                model.Senha = _usuarioRepositorio.CodificarSenha(usuario, model.Senha);
                usuario.PasswordHash = model.Senha;
                await _usuarioRepositorio.AtualizarUsuario(usuario);
                await _usuarioRepositorio.LogarUsuario(usuario, false);

                return RedirectToAction(nameof(MinhaConta));
            }

            return View(model);
        }
    }
}