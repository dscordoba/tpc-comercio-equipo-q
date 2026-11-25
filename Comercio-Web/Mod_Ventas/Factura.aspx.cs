using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentacion.Mod_Ventas
{
    public partial class Factura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Dominio.Venta venta = Session["facturaVenta"] as Dominio.Venta;

            if (venta == null)
            {
                Response.Redirect("~/Mod_Ventas/NuevaVenta.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                lblNumero.Text = venta.NumeroFactura;
                lblFecha.Text = venta.Fecha.ToString("dd/MM/yyyy");
                lblCliente.Text = (string)Session["facturaCliente"];
                lblTotal.Text = venta.TotalFinal.ToString("C");

                dgvLineas.DataSource = venta.LineasVenta;
                dgvLineas.DataBind();
            }
        }
    }
}