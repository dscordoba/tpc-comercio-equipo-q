using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Comercio_Web.Mod_Ventas
{
    public partial class InformeVentas : System.Web.UI.Page
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
                configurarPermisos();
                cargarInforme();
            }
        }

        private void configurarPermisos()
        {
            bool esAdmin = Seguridad.esAdmin(Session["SessionActiva"]);

            // Si es admin muestra el filtro de vendedor; si es vendedor lo oculta
            divFiltroVendedor.Visible = esAdmin;

            if (esAdmin)
            {
                VentaNegocio negocio = new VentaNegocio();
                List<Usuario> vendedores = negocio.listarVendedores();

                ddlVendedor.DataSource = vendedores.Select(u => new
                {
                    Id = u.Id,
                    NombreCompleto = u.Apellido + ", " + u.Nombre
                }).ToList();
                ddlVendedor.DataValueField = "Id";
                ddlVendedor.DataTextField = "NombreCompleto";
                ddlVendedor.DataBind();
                ddlVendedor.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }

        private void cargarInforme()
        {
            try
            {
                lblError.Visible = false;

                Usuario usuarioLogueado = (Usuario)Session["SessionActiva"];
                bool esAdmin = Seguridad.esAdmin(usuarioLogueado);

                VentaNegocio negocio = new VentaNegocio();
                List<Venta> lista = negocio.listarVentas();

                if (!esAdmin)
                {
                    lista = lista.Where(x => x.UsuarioAsociado.Id == usuarioLogueado.Id).ToList();
                }
                else if (ddlVendedor.SelectedValue != "0" && !string.IsNullOrEmpty(ddlVendedor.SelectedValue))
                {
                    int idVendedor = int.Parse(ddlVendedor.SelectedValue);
                    lista = lista.Where(x => x.UsuarioAsociado.Id == idVendedor).ToList();
                }

                // Filtro fechas 
                if (!string.IsNullOrEmpty(txtFechaDesde.Text))
                {
                    DateTime desde = DateTime.Parse(txtFechaDesde.Text).Date;
                    lista = lista.Where(x => x.Fecha.Date >= desde).ToList();
                }

                if (!string.IsNullOrEmpty(txtFechaHasta.Text))
                {
                    DateTime hasta = DateTime.Parse(txtFechaHasta.Text).Date;
                    lista = lista.Where(x => x.Fecha.Date <= hasta).ToList();
                }

                // Filtro cliente o factura
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    string filtro = txtBuscar.Text.Trim().ToLower();
                    lista = lista.Where(x =>
                        x.NumeroFactura.ToLower().Contains(filtro) ||
                        x.ClienteAsociado.Nombre.ToLower().Contains(filtro) ||
                        x.ClienteAsociado.Apellido.ToLower().Contains(filtro) ||
                        x.ClienteAsociado.Dni.Contains(filtro)
                    ).ToList();
                }

                // Actualiza cantidad de ventas y total facturado
                lblCantidadVentas.Text = lista.Count.ToString();
                lblTotalFacturado.Text = lista.Sum(x => x.TotalFinal).ToString("C2");

                dgvVentas.DataSource = lista;
                dgvVentas.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarInforme();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFechaDesde.Text = string.Empty;
            txtFechaHasta.Text = string.Empty;
            txtBuscar.Text = string.Empty;
            if (ddlVendedor.Items.Count > 0)
                ddlVendedor.SelectedIndex = 0;

            cargarInforme();
        }

        protected void dgvVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClientScript.RegisterStartupScript(this.GetType(), "AbrirModalFactura", "abrirModalGlobal('modalDetalleFactura');", true);

                int idVenta = Convert.ToInt32(dgvVentas.SelectedDataKey.Value);
                VentaNegocio negocio = new VentaNegocio();

                // Busca la cabecera de la venta seleccionada
                Venta venta = negocio.listarVentas().FirstOrDefault(x => x.Id == idVenta);

                if (venta != null)
                {
                    lblModalNroFactura.Text = venta.NumeroFactura;
                    lblModalFecha.Text = venta.Fecha.ToString("dd/MM/yyyy HH:mm");
                    lblModalVendedor.Text = venta.UsuarioAsociado.Apellido + ", " + venta.UsuarioAsociado.Nombre;
                    lblModalCliente.Text = venta.ClienteAsociado.Apellido + ", " + venta.ClienteAsociado.Nombre;
                    lblModalDni.Text = venta.ClienteAsociado.Dni;
                    lblModalTotal.Text = venta.TotalFinal.ToString("C2");

                    dgvDetalleFactura.DataSource = negocio.listarDetalleVenta(idVenta);
                    dgvDetalleFactura.DataBind();

                    // Abre el modal
                    string script = "var myModal = new bootstrap.Modal(document.getElementById('modalDetalleFactura')); myModal.show();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalFactura", script, true);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

    }
}