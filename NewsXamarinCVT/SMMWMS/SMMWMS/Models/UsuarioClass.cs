using System;
using System.Collections.Generic;
using System.Text;

namespace SMMWMS.Models
{
    public class UsuarioClass
    {
        public int? IdUsuario { get; set; }
        public string UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
        public int IdPerfil { get; set; }

    }
}
