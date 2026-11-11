using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Mod_Admin
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarTabla();
            }
        }

        // al elegir una fila, pasa los datos al formulario
        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSeleccionado = Convert.ToInt32(dgvUsuarios.SelectedDataKey.Value);

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario seleccionado = negocio.listar().Find(x => x.Id == idSeleccionado);

            if (seleccionado != null)
            {
                hfUsuarioId.Value = seleccionado.Id.ToString();
                txtId.Text = seleccionado.Id.ToString();
                txtNombre.Text = seleccionado.Nombre;
                txtApellido.Text = seleccionado.Apellido;
                txtDni.Text = seleccionado.Dni;
                txtEmail.Text = seleccionado.Email;
                txtNombreUsuario.Text = seleccionado.NombreUsuario;
                ddlPerfil.SelectedValue = seleccionado.Perfil;

                rbActivoSI.Checked = seleccionado.Activo;
                rbActivoNO.Checked = !seleccionado.Activo;
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Nombre = txtNombre.Text.Trim();
                usuario.Apellido = txtApellido.Text.Trim();
                usuario.Dni = txtDni.Text.Trim();
                usuario.Email = txtEmail.Text.Trim();
                usuario.NombreUsuario = txtNombreUsuario.Text.Trim();
                usuario.Contraseña = txtContrasenia.Text;
                usuario.Perfil = ddlPerfil.SelectedValue;
                usuario.Activo = rbActivoSI.Checked;

                UsuarioNegocio negocio = new UsuarioNegocio();

                // si no hay id es un usuario nuevo, si hay id se modifica
                if (string.IsNullOrEmpty(hfUsuarioId.Value))
                {
                    negocio.agregar(usuario);
                }
                else
                {
                    usuario.Id = int.Parse(hfUsuarioId.Value);

                    // el campo contraseña no muestra la clave guardada, si queda vacio se deja la que ya tenia
                    if (usuario.Contraseña == "")
                    {
                        usuario.Contraseña = negocio.listar().Find(x => x.Id == usuario.Id).Contraseña;
                    }

                    negocio.modificar(usuario);
                }

                cargarTabla();
                limpiarFormulario();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = int.Parse(hfUsuarioId.Value);
                UsuarioNegocio negocio = new UsuarioNegocio();

                negocio.eliminar(idUsuario);

                cargarTabla();
                limpiarFormulario();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarFormulario();
        }

        // llena la grilla con los usuarios de la base
        private void cargarTabla()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvUsuarios.DataSource = negocio.listar();
            dgvUsuarios.DataBind();
        }

        // deja el formulario vacio
        private void limpiarFormulario()
        {
            hfUsuarioId.Value = string.Empty;
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDni.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtNombreUsuario.Text = string.Empty;
            txtContrasenia.Text = string.Empty;
            ddlPerfil.SelectedIndex = 0;

            rbActivoSI.Checked = true;
            rbActivoNO.Checked = false;

            dgvUsuarios.SelectedIndex = -1;
        }

    }
}