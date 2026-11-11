using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, Dni, Email, Activo, NombreUsuario, Contraseña, Perfil FROM USUARIOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    aux.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    aux.Contraseña = (string)datos.Lector["Contraseña"];
                    aux.Perfil = (string)datos.Lector["Perfil"];

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

        // agrega un usuario nuevo a la base
        public void agregar(Usuario nuevo)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO USUARIOS (Nombre, Apellido, Dni, Email, Activo, NombreUsuario, Contraseña, Perfil) " +
                    "VALUES (@nombre, @apellido, @dni, @email, @activo, @nombreUsuario, @contrasenia, @perfil)"
                );

                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@dni", nuevo.Dni);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@activo", nuevo.Activo);
                datos.setearParametro("@nombreUsuario", nuevo.NombreUsuario);
                datos.setearParametro("@contrasenia", nuevo.Contraseña);
                datos.setearParametro("@perfil", nuevo.Perfil);

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

        // cambia los datos de un usuario que ya existe
        public void modificar(Usuario usuario)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE USUARIOS " +
                    "SET Nombre = @nombre, Apellido = @apellido, Dni = @dni, Email = @email, Activo = @activo, " +
                    "NombreUsuario = @nombreUsuario, Contraseña = @contrasenia, Perfil = @perfil " +
                    "WHERE Id = @id"
                );

                datos.setearParametro("@id", usuario.Id);
                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@apellido", usuario.Apellido);
                datos.setearParametro("@dni", usuario.Dni);
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@activo", usuario.Activo);
                datos.setearParametro("@nombreUsuario", usuario.NombreUsuario);
                datos.setearParametro("@contrasenia", usuario.Contraseña);
                datos.setearParametro("@perfil", usuario.Perfil);

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

        // borra el usuario de la base
        public void eliminar(int id)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("DELETE FROM USUARIOS WHERE Id = @id");
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