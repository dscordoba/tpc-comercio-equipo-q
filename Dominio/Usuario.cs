using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public enum TipoPerfil
    {
        SIN_ASIGNAR = 0,
        ADMIN = 1,
        VENDEDOR = 2
    }

    public class Usuario : Persona
    {
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public TipoPerfil TipoPerfil { get; set; }

        public Usuario()
        {
            TipoPerfil = TipoPerfil.SIN_ASIGNAR; 
            Activo = true;
        }
    }
}
