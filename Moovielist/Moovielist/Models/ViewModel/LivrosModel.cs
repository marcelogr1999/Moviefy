using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moovielist.Models.ViewModel
{
    public class LivrosModel
    {   
        [Display(Name = "Livro")]
        public Livro _Livro { get; set; }
        [Display(Name = "Ação")]
        public Boolean Acao { get; set; }

        public LivrosModel()
        {
            _Livro = new Livro();
        }
    }
}