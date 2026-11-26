using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentacion.Mod_Compras
{
    public partial class Informes : System.Web.UI.Page
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
                ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
                ddlProveedor.DataSource = proveedorNegocio.listar();
                ddlProveedor.DataValueField = "Id";
                ddlProveedor.DataTextField = "RazonSocial";
                ddlProveedor.DataBind();

                // no se pueden elegir fechas futuras
                txtDesde.Attributes["max"] = DateTime.Today.ToString("yyyy-MM-dd");
                txtHasta.Attributes["max"] = DateTime.Today.ToString("yyyy-MM-dd");

                cargarInforme();
            }
        }

        private void cargarInforme()
        {
            try
            {
                lblError.Visible = false;
                pnlDetalle.Visible = false;

                int idProveedor = int.Parse(ddlProveedor.SelectedValue);

                // si no cargan fechas uso un rango amplio
                DateTime desde = new DateTime(2000, 1, 1);
                DateTime hasta = DateTime.Today;

                if (txtDesde.Text != "")
                    desde = DateTime.Parse(txtDesde.Text);

                if (txtHasta.Text != "")
                    hasta = DateTime.Parse(txtHasta.Text);

                if (desde > hasta)
                {
                    lblError.Text = "La fecha desde no puede ser mayor a la fecha hasta.";
                    lblError.Visible = true;
                    return;
                }

                CompraNegocio negocio = new CompraNegocio();
                List<Compra> lista = negocio.listar(idProveedor, desde, hasta);
                Session["informeCompras"] = lista;

                dgvCompras.DataSource = lista;
                dgvCompras.DataBind();

                decimal total = 0;
                foreach (Compra compra in lista)
                {
                    total += compra.Total;
                }

                lblCantidadCompras.Text = lista.Count.ToString();
                lblTotalComprado.Text = total.ToString("C");
            }
            catch (Exception)
            {
                lblError.Text = "No se pudo cargar el informe.";
                lblError.Visible = true;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarInforme();
        }

        protected void dgvCompras_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Compra> lista = (List<Compra>)Session["informeCompras"];
            Compra seleccionada = lista[dgvCompras.SelectedIndex];

            lblTituloDetalle.Text = "Detalle de la compra " + seleccionada.NumeroFactura + " - " + seleccionada.ProveedorAsociado.RazonSocial;
            dgvDetalle.DataSource = seleccionada.LineasCompra;
            dgvDetalle.DataBind();
            pnlDetalle.Visible = true;
        }
    }
}