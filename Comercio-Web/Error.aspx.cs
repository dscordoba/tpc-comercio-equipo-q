using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["error"] != null)
            {
                lblMensaje.Text = Session["error"].ToString();
            }

            // Si tiene sesión activa va al menú principal, si no, va al Login
            if (Seguridad.sesionActiva(Session["SessionActiva"]))
            {
                lnkVolver.NavigateUrl = "~/Default.aspx";
                lnkVolver.Text = "Volver al menú principal";
            }
            else
            {
                lnkVolver.NavigateUrl = "~/Login.aspx";
                lnkVolver.Text = "Volver al Login";
            }
        }
    }
}