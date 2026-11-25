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

        // Valida que el campo solo tenga letras y números
        public static bool SoloAlfanumerico(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                return false;

            return Regex.IsMatch(cadena, "^[a-zA-Z0-9]+$");
        }



        // Valida el formato de mail
        public static bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string patron = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            return Regex.IsMatch(email.Trim(), patron);
        }

        // Valida que la cadena sea un número decimal positivo (admite punto o coma) y que no supere el límite máximo
        public static bool NumeroDecimalPositivo(string cadena, decimal maximo = decimal.MaxValue)
        {
            if (string.IsNullOrWhiteSpace(cadena))
                return false;

            string valorNormalizado = cadena.Trim().Replace(',', '.');

            if (decimal.TryParse(valorNormalizado, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal resultado))
            {
                return resultado > 0 && resultado <= maximo;
            }

            return false;
        }

    }
}
