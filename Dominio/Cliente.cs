using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente : Persona
    {
        //propiedad para mostrar ambos datos en listas o combos
        public string NombreCompleto
        {
            get { return Apellido + ", " + Nombre; }
        }
    }
}
