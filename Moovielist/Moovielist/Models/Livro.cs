using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moovielist.Models
{
    public class Livro
    {
        public int LivroID { get; set; }
        public String Livro_nome { get; set; }
        [Display(Name = "Gênero")]
        public virtual Genero _Genero { get; set; }

        //public Livro()
        //{
        //    _Genero = new Genero();
        //}
    }

}