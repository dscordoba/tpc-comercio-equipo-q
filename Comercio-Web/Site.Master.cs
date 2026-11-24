using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si no hay una sesión activa me redirige a Login
            if (!Seguridad.sesionActiva(Session["SessionActiva"]))
            {
                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                // Recuperamos el objeto Usuario guardado en el Login
                Usuario user = (Usuario)Session["SessionActiva"];

                lblUsuarioLogueado.Text = user.Nombre + " " + user.Apellido;
                btnCerrarSesion.Visible = true;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx", false);
        }
    }
}