using Moovielist.Models;
using Moovielist.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Moovielist.Models.ViewModel;

namespace Moovielist.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            List<Item> lista = new List<Item>();
            int idUsuario = Convert.ToInt32(Session["UserID"]);
            using (MeuContexto ctx = new MeuContexto())
            {
                if (Session["UserID"] != null)
                {
                    //a = ctx.Itens.Include(i => i.Livro).ToList();
                    lista = ctx.Itens.Where(i => i.UsuarioID == idUsuario).Include(l => l.Livro).ToList();
                }
            }
            return View(lista);
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
            return RedirectToAction("Login");
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

        public ActionResult Logout(Usuario usuario)
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        //[HttpPost]
        //public ActionResult Logout()
        //{
        //    Session.Clear();

        //    return RedirectToAction("Login");
        //}

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

        public ActionResult Edit(int? id)
        {
            MeuContexto ctx = new MeuContexto();
            List<Item> itens = ctx.Itens.Include(i => i.Livro).ToList();
            Item itemEncontrado = new Item();
            foreach (var item in itens)
            {
                if(item.ItemID == id)
                {
                    itemEncontrado = item;
                }
            }
            EditarViewModel editarViewModel = new EditarViewModel();
            editarViewModel._item.Estado = itemEncontrado.Estado;
            editarViewModel._item.ItemID = itemEncontrado.ItemID;
            editarViewModel._item.Livro = itemEncontrado.Livro;
            editarViewModel._item.LivroID = itemEncontrado.LivroID;
            editarViewModel.Post();
            return View(editarViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditarViewModel item)
        {
            if (ModelState.IsValid)
            {
                if (Int32.Parse(item.SelectedValue) != 0)
                {
                    using (MeuContexto ctx = new MeuContexto())
                    {
                        item._item.Estado = item.ItemsInDropDown[Int32.Parse(item.SelectedValue)].Text.ToString();
                        //ctx.Entry(item._item).State = System.Data.Entity.EntityState.Modified;
                        int a = Convert.ToInt32(Session["UserID"]);
                        var result = ctx.Usuarios.Find(a).ListaUsuario.Where(i => i.ItemID == item._item.ItemID).FirstOrDefault();
                        if (result != null)
                        {
                            item._item.LivroID = item._item.Livro.LivroID;
                            //item._item.Livro = null;
                            result.UsuarioID = a;
                            item._item.UsuarioID = a;
                            ctx.Entry(result).CurrentValues.SetValues(item._item);
                        }
                        ctx.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            //ADICIONAR MENSAGEM DE ERRO COM OPÇÃO SELECIONE!
            return View(item);
        }

        public ActionResult Add(int? id)
        {
            Livro l = new Livro();
            using (MeuContexto ctx = new MeuContexto())
            {
                l = ctx.Livros.Find(id);
            }
            EditarViewModel editarViewModel = new EditarViewModel();
            Item i = new Item();
            i.Livro = l;
            editarViewModel._item = i;

            return View(editarViewModel);
        }

        [HttpPost]
        public ActionResult Add(EditarViewModel item)
        {
            if (ModelState.IsValid)
            {
                if (Int32.Parse(item.SelectedValue) != 0)
                {
                    using (MeuContexto ctx = new MeuContexto())
                    {
                        item._item.Estado = item.ItemsInDropDown[Int32.Parse(item.SelectedValue)].Text.ToString();
                        //item._item.UsuarioID = Convert.ToInt32(Session["UserID"]);
                        //ctx.Usuarios.Find(Convert.ToInt32(Session["UserID"])).ListaUsuario.Add(item._item);
                        //ctx.SaveChanges();

                        //ctx.Livros.Add(new Livro { Livro_nome = "AAA", _Genero = ctx.Generos.FirstOrDefault() });
                        ctx.Usuarios.Find(Convert.ToInt32(Session["UserID"])).ListaUsuario.Add(new Item { LivroID = item._item.Livro.LivroID, Estado = item._item.Estado });
                        ctx.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            //ADICIONAR MENSAGEM DE ERRO COM OPÇÃO SELECIONE!
            return View(item);
        }

        public ActionResult Delete(int? id)
        {
            Livro l = new Livro();
            using (MeuContexto ctx = new MeuContexto())
            {
                l = ctx.Livros.Find(id);
            }
            EditarViewModel editarViewModel = new EditarViewModel();
            Item i = new Item();
            i.Livro = l;
            editarViewModel._item = i;

            return View(editarViewModel);

            //MeuContexto ctx = new MeuContexto();
            //List<Item> itens = ctx.Itens.Include(i => i.Livro).ToList();
            //Item itemEncontrado = new Item();
            //foreach (var item in itens)
            //{
            //    if (item.ItemID == id)
            //    {
            //        itemEncontrado = item;
            //    }
            //}
            //EditarViewModel editarViewModel = new EditarViewModel();
            //editarViewModel._item.Estado = itemEncontrado.Estado;
            //editarViewModel._item.ItemID = itemEncontrado.ItemID;
            //editarViewModel._item.Livro = itemEncontrado.Livro;
            //editarViewModel._item.LivroID = itemEncontrado.LivroID;
            //editarViewModel.Post();
            //return View(editarViewModel);
        }

        [HttpPost]
        public ActionResult Delete(EditarViewModel item)
        {
            if (ModelState.IsValid)
            {
                using (MeuContexto ctx = new MeuContexto())
                {
                    var result = ctx.Itens.Where(i => i.Livro.LivroID == item._item.Livro.LivroID).FirstOrDefault();
                    ctx.Itens.Remove(result);
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(item);
        }
    }
}