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
                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

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

        public List<Cliente> listarConSP()
        {
            List<Cliente> lista = new List<Cliente>();
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearProcedimiento("listarClientes");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

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

        // Verificar si existe el DNI en BD
        public bool ExisteDni(string dni, int idExcluir = 0)
        {
            ConexionDatos datos = new ConexionDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Clientes WHERE Dni = @Dni AND Id != @Id");
                datos.setearParametro("@Dni", dni);
                datos.setearParametro("@Id", idExcluir);

                datos.ejecutarLectura();
                if (datos.Lector.Read())
                    return Convert.ToInt32(datos.Lector[0]) > 0;

                return false;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Verificar si existe el Email en BD
        public bool ExisteEmail(string email, int idExcluir = 0)
        {
            ConexionDatos datos = new ConexionDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Clientes WHERE Email = @Email AND Id != @Id");
                datos.setearParametro("@Email", email);
                datos.setearParametro("@Id", idExcluir);

                datos.ejecutarLectura();
                if (datos.Lector.Read())
                    return Convert.ToInt32(datos.Lector[0]) > 0;

                return false;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        /* Agregar BD */
        public void agregar(Cliente nuevo)
        {
            if (ExisteDni(nuevo.Dni))
                throw new Exception("Ya existe un cliente registrado con ese DNI.");

            if (ExisteEmail(nuevo.Email))
                throw new Exception("Ya existe un cliente registrado con ese Email.");

            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO Clientes (Nombre, Apellido, Dni, Email) " +
                    "VALUES (@nombre, @apellido, @dni, @email)"
                );

                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@dni", nuevo.Dni);
                datos.setearParametro("@email", nuevo.Email);

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
            if (ExisteDni(cliente.Dni, cliente.Id))
                throw new Exception("Ya existe otro cliente registrado con ese DNI.");

            if (ExisteEmail(cliente.Email, cliente.Id))
                throw new Exception("Ya existe otro cliente registrado con ese Email.");

            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE Clientes " +
                    "SET Nombre = @nombre, Apellido = @apellido, Dni = @dni, Email = @email " +
                    "WHERE Id = @id"
                );

                datos.setearParametro("@id", cliente.Id);
                datos.setearParametro("@nombre", cliente.Nombre);
                datos.setearParametro("@apellido", cliente.Apellido);
                datos.setearParametro("@dni", cliente.Dni);
                datos.setearParametro("@email", cliente.Email);

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

        /* Eliminación lógica (Inactivar/Reactivar */
        public void eliminarLogico(int id, bool activo = false)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("UPDATE CLIENTES SET Activo = @activo WHERE Id = @id");
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
