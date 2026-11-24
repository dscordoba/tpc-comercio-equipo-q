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

        // agrega una marca nueva a la base
        public void agregar(Marca nueva)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("INSERT INTO MARCAS (Descripcion) VALUES (@descripcion)");

                datos.setearParametro("@descripcion", nueva.Descripcion);
                

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // cambia los datos de una marca que ya existe
        public void modificar(Marca marca)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Descripcion = @descripcion WHERE Id = @id");

                datos.setearParametro("@id", marca.Id);
                datos.setearParametro("@descripcion", marca.Descripcion);
                

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        // inactiva o reactiva la marca, no la borra
        public void eliminarLogico(int id, bool activo = false)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Activo = @activo WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@activo", activo);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }

}

