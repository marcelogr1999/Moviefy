using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Moovielist.Models.DAL
{
    public class MeuContexto : DbContext
    {
        public MeuContexto() : base("Movieconn")
        {
            //DropCreateDatabaseAlways
            //DropCreateDatabaseIfModelChanges
            //Migrations (produção)

            Database.SetInitializer<MeuContexto>(new DropCreateDatabaseIfModelChanges<MeuContexto>());
        }
        public DbSet<Genero> Generos{ get; set; }
        public DbSet<Livro> Livros{ get; set; }
        public DbSet<Item> Itens{ get; set; }
        public DbSet<Usuario> Usuarios { get; set; } 
    }
}