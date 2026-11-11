using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> listar()
        {
            List<Cliente> lista = new List<Cliente>();
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, Dni, Email, Activo FROM CLIENTES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Email = (string)datos.Lector["Email"];
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

        /* Agregar BD */
        public void agregar(Cliente nuevo)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO Clientes (Nombre, Apellido, Dni, Email, Activo) " +
                    "VALUES (@nombre, @apellido, @dni, @email, @activo)"
                );

                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@dni", nuevo.Dni);
                datos.setearParametro("@email", nuevo.Email);
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

        /* Modificar BD */
        public void modificar(Cliente cliente)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE Clientes " +
                    "SET Nombre = @nombre, Apellido = @apellido, Dni = @dni, Email = @email, Activo = @activo " +
                    "WHERE Id = @id"
                );

                datos.setearParametro("@id", cliente.Id);
                datos.setearParametro("@nombre", cliente.Nombre);
                datos.setearParametro("@apellido", cliente.Apellido);
                datos.setearParametro("@dni", cliente.Dni);
                datos.setearParametro("@email", cliente.Email);
                datos.setearParametro("@activo", cliente.Activo);

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

        /* Verificar si el cliente tiene ventas registradas */
        public bool tieneVentas(int idCliente)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM VENTAS WHERE IdCliente = @idCliente");
                datos.setearParametro("@idCliente", idCliente);

                int cantidad = datos.ejecutarAccionScalar();
                return cantidad > 0;
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

        /* Eliminar (solo si no tiene ventas) */
        public void eliminar(int id)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("DELETE FROM CLIENTES WHERE Id = @id");
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
