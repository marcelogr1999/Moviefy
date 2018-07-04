using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moovielist.Models.ViewModel
{
    public class LivrosModel
    {       
        public Livro _Livro { get; set; }
        public Boolean Acao { get; set; }

        public LivrosModel()
        {
            _Livro = new Livro();
        }
    }
}