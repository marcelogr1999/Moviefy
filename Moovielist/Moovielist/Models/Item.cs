using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moovielist.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        [Display(Name = "Nome Do Livro")]
        public Livro Livro { get; set; }
        public int LivroID { get; set; }
        public virtual String Estado { get; set; }
        public int UsuarioID { get; set; }

        //public Item()
        //{
        //    _Livro = new Livro();
        //}
    }
}