using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Moovielist.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID{ get; set; }

        [Required(ErrorMessage = "Campo de username é necessário")]
        public String Username{ get; set; }
        [Required(ErrorMessage = "Senha é necessária")]
        public String Pass { get; set; }
        public List<Item> ListaUsuario { get; set; }
    }
}