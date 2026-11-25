using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Resumen del día (completo lbl de ventas y compras del día)
                VentaNegocio ventaNegocio = new VentaNegocio();
                lblVentasHoy.Text = ventaNegocio.contarVentasHoy().ToString();

                if (Seguridad.esAdmin(Session["SessionActiva"]))
                {
                    CompraNegocio compraNegocio = new CompraNegocio();
                    lblComprasHoy.Text = compraNegocio.contarComprasHoy().ToString();
                }

                // Alertas de stock (si el producto está igual y por debajo del stock mínimo)
                ProductoNegocio productoNegocio = new ProductoNegocio();
                List<Dominio.Producto> stockBajo = productoNegocio.listarStockBajo();

                if (stockBajo.Count > 0)
                {
                    repStockBajo.DataSource = stockBajo;
                    repStockBajo.DataBind();
                }
                else
                {
                    lblSinAlertas.Visible = true;
                }
            }
        }

    }
}