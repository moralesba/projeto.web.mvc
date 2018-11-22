using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Financas.Web.Mvc.Models;
using Senai_Financas_Web_Mvc_Manha_master.Repositorio;

namespace Senai.Financas.Web.Mvc.Controllers {
    public class UsuarioController : Controller {
        [HttpGet]
        public ActionResult Cadastrar () {
            return View ();
        }

        [HttpPost]
        public ActionResult Cadastrar (IFormCollection form) {
            UsuarioModel usuario = new UsuarioModel ();
            usuario.Nome = form["nome"];
            usuario.Email = form["email"];
            usuario.Senha = form["senha"];
            usuario.DataNascimento = DateTime.Parse (form["dataNascimento"]);

            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio ();
            usuarioRepositorio.Cadastrar (usuario);

            ViewBag.Mensagem = "Usuário Cadastrado";

            return RedirectToAction ("Index", "Transacao");
        }

        [HttpGet]
        public IActionResult Login () {
            return View ();
        }

        [HttpPost]
        public IActionResult Login (IFormCollection form) {
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio ();
            UsuarioModel usuario = usuarioRepositorio.Login (form["email"], form["senha"]);

            if (usuario != null) {
                HttpContext.Session.SetString ("idUsuario", usuario.Id.ToString ());
                return RedirectToAction ("Cadastrar", "Transacao");
            }

            ViewBag.Mensagem = "Usuário inválido";

            return View ();
        }

        [HttpGet]
        public IActionResult Listar () {
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio ();

            ViewData["Usuarios"] = usuarioRepositorio.Listar ();

            return View ();
        }

        [HttpGet]
        public IActionResult Excluir (int id) {
            //Pega os dados do arquivo usuario.csv
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio ();
            usuarioRepositorio.Excluir (id);

            TempData["Mensagem"] = "Usuário excluído";

            return RedirectToAction ("Listar");
        }

        [HttpGet]
        public IActionResult Editar (int id) {

            if (id == 0) {
                TempData["Mensagem"] = "Informe um usuário pra editar";
                return View ("Listar");
            }

            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio ();
            UsuarioModel usuario = usuarioRepositorio.BuscarPorId (id);

            if (usuario != null) {
                ViewBag.Usuario = usuario;
            } else {
                TempData["Mensagem"] = "Usuário não encontrado";
                return RedirectToAction ("Listar");
            }
            return View ();
        }

        [HttpPost]
        public IActionResult Editar (IFormCollection form) {
            UsuarioModel usuario = new UsuarioModel {
                Id = int.Parse (form["id"]),
                Nome = form["nome"],
                Email = form["email"],
                Senha = form["senha"],
                DataNascimento = DateTime.Parse (form["dataNascimento"])
            };

            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio ();
            usuarioRepositorio.Editar (usuario);

            TempData["Mensagem"] = "Usuário editado";
            return RedirectToAction ("Listar");
        }
    }
}