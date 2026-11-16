using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("SELECT Id, RazonSocial, Cuit, Email, Telefono, Activo FROM PROVEEDORES");
                datos.ejecutarLectura();

                while(datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.RazonSocial = (string)datos.Lector["RazonSocial"];
                    aux.Cuit = (string)datos.Lector["Cuit"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
                }
                return lista;
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

        public void agregar(Proveedor nuevo)
        {
            ConexionDatos datos = new ConexionDatos();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO Proveedores (RazonSocial, Cuit, Email, Telefono, Activo)" +
                    "VALUES (@razonSocial, @cuit, @email, @telefono, @activo)"
                    );

                datos.setearParametro("@razonSocial", nuevo.RazonSocial);
                datos.setearParametro("@cuit", nuevo.Cuit);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@telefono", nuevo.Telefono);
                datos.setearParametro("@activo", nuevo.Activo);

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

        public void modificar( Proveedor proveedor)
        {
            ConexionDatos datos = new ConexionDatos();
            try
            {
                datos.setearConsulta(
                    "UPDATE PROVEEDORES" + "SET RazonSocial = @razonSocial, Cuit = @cuit, Email = @email, Telefono = @telefono, Activo = @activo" +
                    "WHERE Id = @id"
                    );

                datos.setearParametro("@id", proveedor.Id);
                datos.setearParametro("@razonSocial", proveedor.RazonSocial);
                datos.setearParametro("@cuit", proveedor.Cuit);
                datos.setearParametro("@email", proveedor.Email);
                datos.setearParametro("@telefono", proveedor.Telefono);
                datos.setearParametro("@activo", proveedor.Activo);

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

        public void eliminar(int id)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("DELETE FROM PROVEEDORES WHERE Id = @id");
                datos.setearParametro("@id", id);

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
