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
            if (!Seguridad.esAdmin(Session["SessionActiva"]))
            {
                Session.Add("error", "Se requiere permiso de administrador para acceder a esta pantalla.");
                Response.Redirect("~/Error.aspx", false);
            }

            if (!IsPostBack)
            {
                btnInactivar.Visible = false;
                btnInactivar.Enabled = false;
                divRbActivoNo.Visible = false;
                lblError.Visible = false;
                cargarTabla();
                cargarPerfiles(true); 
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
                lblError.Visible = false;

                Session.Add("usuarioSeleccionado", seleccionado);

                txtId.Text = seleccionado.Id.ToString();
                txtNombre.Text = seleccionado.Nombre;
                txtApellido.Text = seleccionado.Apellido;
                txtDni.Text = seleccionado.Dni;
                txtEmail.Text = seleccionado.Email;
                txtNombreUsuario.Text = seleccionado.NombreUsuario;

                // Carga perfil "Sin asignar" solo si lo tiene 
                int perfilId = (int)seleccionado.TipoPerfil;
                cargarPerfiles(perfilId == 0);
                ddlPerfil.SelectedValue = perfilId.ToString();

                rbActivoSI.Checked = seleccionado.Activo;
                rbActivoNO.Checked = !seleccionado.Activo;

                btnInactivar.Visible = true;
                btnInactivar.Enabled = true;

                if (!seleccionado.Activo)
                {
                    btnInactivar.Text = "Reactivar";
                    btnInactivar.CssClass = "btn btn-success";
                    divRbActivoSi.Visible = false;
                    divRbActivoNo.Visible = true;
                }
                else
                {
                    btnInactivar.Text = "Inactivar";
                    btnInactivar.CssClass = "btn btn-danger";
                    divRbActivoNo.Visible = false;
                    divRbActivoSi.Visible = true;
                }
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;

                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    !Validaciones.SoloNumeros(txtDni.Text.Trim()) ||
                    txtDni.Text.Trim().Length < 7 ||
                    !Validaciones.EmailValido(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                    ddlPerfil.SelectedValue == "0" ||
                    (Session["usuarioSeleccionado"] == null && string.IsNullOrWhiteSpace(txtContrasenia.Text)))
                {
                    lblError.Text = "Verifique que los campos obligatorios sean válidos.";
                    lblError.Visible = true;
                    return;
                }

                Usuario usuario = new Usuario();
                usuario.Nombre = txtNombre.Text.Trim();
                usuario.Apellido = txtApellido.Text.Trim();
                usuario.Dni = txtDni.Text.Trim();
                usuario.Email = txtEmail.Text.Trim();
                usuario.NombreUsuario = txtNombreUsuario.Text.Trim();
                usuario.TipoPerfil = (TipoPerfil)int.Parse(ddlPerfil.SelectedValue);

                UsuarioNegocio negocio = new UsuarioNegocio();

                // Valido si hay un usuario seleccionado en Sesión
                if (Session["usuarioSeleccionado"] == null)
                {
                    usuario.Contraseña = txtContrasenia.Text.Trim();
                    negocio.agregar(usuario);
                }
                else
                {
                    Usuario seleccionado = (Usuario)Session["usuarioSeleccionado"];
                    usuario.Id = seleccionado.Id;
                    usuario.Contraseña = string.IsNullOrWhiteSpace(txtContrasenia.Text) ? seleccionado.Contraseña : txtContrasenia.Text.Trim();
                    negocio.modificar(usuario);
                }

                Session.Remove("usuarioSeleccionado");
                cargarTabla();
                limpiarFormulario();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                    lblError.Text = "Ya existe un usuario registrado con ese DNI, correo electrónico o nombre de usuario.";
                else
                    lblError.Text = "Error al guardar el usuario en la base de datos.";

                lblError.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
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
            List<Usuario> lista = negocio.listar();

            // Filtro por perfil
            int perfilSeleccionado = int.Parse(ddlFiltroPerfil.SelectedValue);
            if (perfilSeleccionado != -1)
            {
                lista = lista.Where(x => (int)x.TipoPerfil == perfilSeleccionado).ToList();
            }

            // Filtro por estado 
            int estadoSeleccionado = int.Parse(ddlFiltroEstado.SelectedValue);
            if (estadoSeleccionado != -1)
            {
                bool activo = estadoSeleccionado == 1;
                lista = lista.Where(x => x.Activo == activo).ToList();
            }

            dgvUsuarios.DataSource = lista;
            dgvUsuarios.DataBind();
        }

        protected void Filtros_Changed(object sender, EventArgs e)
        {
            cargarTabla();
        }

        protected void btnPendientes_Click(object sender, EventArgs e)
        {
            ddlFiltroPerfil.SelectedValue = "0"; // Sin asignar
            ddlFiltroEstado.SelectedValue = "1"; // Activos
            cargarTabla();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            ddlFiltroPerfil.SelectedValue = "-1";
            ddlFiltroEstado.SelectedValue = "-1";
            cargarTabla();
        }

        // deja el formulario vacio
        private void limpiarFormulario()
        {
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDni.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtNombreUsuario.Text = string.Empty;
            txtContrasenia.Text = string.Empty;

            cargarPerfiles(true);
            ddlPerfil.SelectedIndex = 0;

            rbActivoSI.Checked = true;
            rbActivoNO.Checked = false;

            dgvUsuarios.SelectedIndex = -1;

            btnInactivar.Visible = false;
            btnInactivar.Enabled = false;

            divRbActivoSi.Visible = true;
            divRbActivoNo.Visible = false;

            lblError.Visible = false;
        }

        // Método para armar el desplegable dinámicamente
        private void cargarPerfiles(bool incluirSinAsignar)
        {
            ddlPerfil.Items.Clear();

            if (incluirSinAsignar)
            {
                ddlPerfil.Items.Add(new ListItem("Sin asignar", "0"));
            }

            ddlPerfil.Items.Add(new ListItem("Administrador", "1"));
            ddlPerfil.Items.Add(new ListItem("Vendedor", "2"));
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["usuarioSeleccionado"] != null)
                {
                    Usuario seleccionado = (Usuario)Session["usuarioSeleccionado"];
                    UsuarioNegocio negocio = new UsuarioNegocio();

                    negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);

                    Session.Remove("usuarioSeleccionado");
                    cargarTabla();
                    limpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }
    }
}