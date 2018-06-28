using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moovielist.Models
{
    public class Livro
    {
        public int LivroID { get; set; }
        public String Livro_nome { get; set; }
        public Genero _Genero { get; set; }

        public Livro()
        {
            _Genero = new Genero();
        }
    }

}