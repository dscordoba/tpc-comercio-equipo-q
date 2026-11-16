using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio
{
    public partial class Validaciones
    {
        // Valida que el campo solo tenga números
        public static bool SoloNumeros(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                return false;

            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }

        // Valida el formato de mail
        public static bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string patron = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            return Regex.IsMatch(email.Trim(), patron);
        }
    }
}
