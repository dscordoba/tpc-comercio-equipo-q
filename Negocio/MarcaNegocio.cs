using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {

        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            ConexionDatos datos = new ConexionDatos();

            datos.setearConsulta("select Id, Descripcion, Activo from MARCAS");
            datos.ejecutarLectura();

            while (datos.Lector.Read())
            {
                Marca aux = new Marca();
                aux.Id = (int)datos.Lector["Id"];
                aux.Descripcion = (string)datos.Lector["Descripcion"];
                aux.Activo = (bool)datos.Lector["Activo"];

                lista.Add(aux);
            }

            datos.cerrarConexion();
            return lista;
        }

    }

}

