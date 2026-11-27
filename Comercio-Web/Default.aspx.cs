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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva(Session["SessionActiva"]))
            {
                Response.Redirect("~/Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                // Resumen del día (completo lbl de ventas y compras del día)
                VentaNegocio ventaNegocio = new VentaNegocio();

                if (Seguridad.esAdmin(Session["SessionActiva"]))
                {
                    lblVentasHoy.Text = ventaNegocio.contarVentasHoy().ToString();

                    CompraNegocio compraNegocio = new CompraNegocio();
                    lblComprasHoy.Text = compraNegocio.contarComprasHoy().ToString();
                }
                else
                {
                    Usuario usuarioLogueado = (Usuario)Session["SessionActiva"];
                    lblVentasHoy.Text = ventaNegocio.listarVentas()
                        .Count(v => v.UsuarioAsociado.Id == usuarioLogueado.Id && v.Fecha.Date == DateTime.Today)
                        .ToString();
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