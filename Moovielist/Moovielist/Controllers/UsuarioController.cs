using Moovielist.Models;
using Moovielist.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moovielist.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            using(MeuContexto ctx = new MeuContexto())
            {
                return View(ctx.Usuarios.ToList());
            }
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario u)
        {
            if (ModelState.IsValid)
            {
                using (MeuContexto ctx = new MeuContexto())
                {
                    ctx.Usuarios.Add(u);
                    ctx.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = u.Username + "foi cadastrado";
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            using(MeuContexto ctx = new MeuContexto())
            {
                var user = ctx.Usuarios.Where(u => u.Username == usuario.Username && u.Pass == 
                usuario.Pass).FirstOrDefault();
                if (user != null)
                {
                    Session["UserID"] = user.UsuarioID.ToString();
                    Session["UserName"] = user.Username.ToString();
                    return RedirectToAction("Logado");
                }
                else
                {
                    ModelState.AddModelError("", "Usuario ou senha incorretos.");
                }
            }
            return View();
        }

        public ActionResult Logado()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}