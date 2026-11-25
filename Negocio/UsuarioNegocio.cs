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
        public bool login(Usuario usuario)
        {
            ConexionDatos datos = new ConexionDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, Perfil, Activo FROM Usuarios WHERE NombreUsuario = @nombreUsuario AND Contraseña = @pass");
                datos.setearParametro("@nombreUsuario", usuario.NombreUsuario);
                datos.setearParametro("@pass", usuario.Contraseña);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        usuario.Nombre = datos.Lector["Nombre"].ToString(); 

                    if (!(datos.Lector["Apellido"] is DBNull))
                        usuario.Apellido = datos.Lector["Apellido"].ToString();

                    usuario.TipoPerfil = (TipoPerfil)(int)datos.Lector["Perfil"];
                    usuario.Activo = (bool)datos.Lector["Activo"];

                    return true;
                }
                return false;
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

        public void registrar(Usuario nuevo)
        {
            if (existeDni(nuevo.Dni))
            {
                throw new Exception("El DNI ingresado ya se encuentra registrado.");
            }

            if (existeEmail(nuevo.Email))
            {
                throw new Exception("El correo electrónico ingresado ya se encuentra registrado.");
            }

            nuevo.Activo = true;
            nuevo.TipoPerfil = TipoPerfil.SIN_ASIGNAR;
            agregar(nuevo);
        }

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
                    aux.TipoPerfil = (TipoPerfil)(int)datos.Lector["Perfil"];

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
                nuevo.Email = nuevo.Email.Trim().ToLower();
                if (string.IsNullOrEmpty(nuevo.NombreUsuario))
                {
                    nuevo.NombreUsuario = nuevo.Email;
                }

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
                datos.setearParametro("@perfil", (int)nuevo.TipoPerfil);

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
                datos.setearParametro("@perfil", (int)usuario.TipoPerfil);

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

        /* Eliminación lógica (Inactivar/Reactivar) */
        public void eliminarLogico(int id, bool activo = false)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("UPDATE USUARIOS SET Activo = @activo WHERE Id = @id");
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

        // Valida si el DNI ya está registrado en BD
        public bool existeDni(string dni)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(1) FROM Usuarios WHERE Dni = @dni");
                datos.setearParametro("@dni", dni);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector[0]) > 0;
                }
                return false;
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

        // Valida si el Email ya está registrado en BD
        public bool existeEmail(string email)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(1) FROM USUARIOS WHERE Email = @email");
                datos.setearParametro("@email", email.Trim().ToLower());
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector[0]) > 0;
                }
                return false;
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