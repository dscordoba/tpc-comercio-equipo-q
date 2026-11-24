using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Registro : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    Response.Redirect("~/Default.aspx", false);
                    return;
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            string dni = txtDni.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                !Validaciones.SoloNumeros(dni) ||
                dni.Length < 7 ||
                !Validaciones.EmailValido(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmarPassword.Text))
            {
                lblError.Text = "Verifique que los campos obligatorios sean válidos.";
                lblError.Visible = true;
                return;
            }

            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                lblError.Text = "Las contraseñas no coinciden.";
                lblError.Visible = true;
                return;
            }

            try
            {
                Usuario nuevo = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Dni = dni,
                    Email = txtEmail.Text.Trim(),
                    Contraseña = txtPassword.Text
                };

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.registrar(nuevo);

                // Si se registró en la BD muestro panel de confirmación
                formRegistro.Visible = false;
                pnlExito.Visible = true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                    lblError.Text = "Ya existe un usuario registrado con ese DNI o correo electrónico.";
                else
                    lblError.Text = "Error al registrar el usuario en la base de datos.";

                lblError.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }
    }

}