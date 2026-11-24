using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class Login : System.Web.UI.Page
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

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                lblError.Visible = false;
                if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    lblError.Text = "Debe ingresar usuario y contraseña.";
                    lblError.Visible = true;
                    return;
                }

                usuario.NombreUsuario = txtUsuario.Text.Trim();
                usuario.Contraseña = txtPassword.Text;

                if (negocio.login(usuario))
                {
                    if (!usuario.Activo)
                    {
                        Session.Add("error", "Tu usuario se encuentra inactivo. Contacta al administrador.");
                        Response.Redirect("~/Error.aspx", false);
                    }
                    else if (usuario.TipoPerfil == TipoPerfil.SIN_ASIGNAR)
                    {
                        Session.Add("error", "Tu cuenta está pendiente de aprobación. Un administrador debe asignarte un perfil para ingresar.");
                        Response.Redirect("~/Error.aspx", false);
                    }
                    else
                    {
                        Session.Add("SessionActiva", usuario);
                        Response.Redirect("~/Default.aspx", false);
                    }
                }
                else
                {
                    Session.Add("error", "Usuario o contraseña incorrectos.");
                    Response.Redirect("~/Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("~/Error.aspx", false);
            }
        }

    }
}