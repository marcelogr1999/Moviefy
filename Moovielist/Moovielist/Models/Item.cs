using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moovielist.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public Livro _Livro { get; set; }
        public String Estado { get; set; }

        public Item()
        {
            _Livro = new Livro();
        }
    }
}