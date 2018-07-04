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
    public class LivrosController : Controller
    {
        // GET: Livros
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<LivrosModel> livrosModel = new List<LivrosModel>();
            List<Item> listaItens = new List<Item>();
            List<Livro> listaLivros = new List<Livro>();

            using (MeuContexto ctx = new MeuContexto())
            {
                listaLivros = ctx.Livros.ToList();
                if (Session["UserID"]!= null)
                {
                    listaItens = retornaListaUsuario();
                    for (int i = 0; i < listaLivros.Count; i++)
                    {
                        livrosModel.Add(new LivrosModel { Acao = false, _Livro = listaLivros[i]});
                        for (int j = 0; j < listaItens.Count; j++)
                        {
                            if (listaItens[j].Livro.LivroID == listaLivros[i].LivroID)
                            {
                                livrosModel[i].Acao = true;
                            }
                        }
                    }
                    return View(livrosModel);
                }
                else
                {
                    foreach (var livro in listaLivros)
                    {
                        livrosModel.Add(new LivrosModel { Acao = false, _Livro = livro });
                    }
                    return View(livrosModel);
                }
                
            }

        }

        public List<Item> retornaListaUsuario()
        {
            List<Item> listaItens = new List<Item>();
            using(MeuContexto ctx = new MeuContexto())
            {
                if (Session["UserID"] != null)
                {
                    List<Usuario> usuarios = ctx.Usuarios.Include(i => i.ListaUsuario).Include(i => i.ListaUsuario.Select(s => s.Livro)).ToList();
                    foreach (var usuario in usuarios)
                        if (usuario.UsuarioID == Convert.ToInt32(Session["UserID"]))
                        {
                            listaItens = usuario.ListaUsuario;
                            return listaItens;
                        }
                }
                return null;
            }
        }

        public ActionResult Edit(int? id)
        {
            List<Item> listaItens = retornaListaUsuario();
            foreach (var item in listaItens)
            {
                if (item.Livro.LivroID == id)
                {
                    return RedirectToAction("Edit", "Usuario", new { id = item.ItemID});
                }
            }
            return View();
        }


        public ActionResult Add(int? id)
        {
            return RedirectToAction("Add", "Usuario", new { id });
        }

        public ActionResult Delete(int? id)
        {
            return RedirectToAction("Delete", "Usuario", new { id });
        }
    }
}