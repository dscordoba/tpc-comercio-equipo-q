using Dominio;
using Negocio;
using Presentacion.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Mod_Ventas
{
    public partial class NuevaVenta : System.Web.UI.Page
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
                cargarClientes();
                cargarProductos();

                txtFecha.Attributes["max"] = DateTime.Today.ToString("yyyy-MM-dd");
                txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");

                VentaNegocio negocio = new VentaNegocio();
                txtNumeroFactura.Text = negocio.obtenerProximoNumeroFactura();

                Session.Remove("DetalleVenta");
                cargarLineas();
            }
        }
        private void cargarClientes()
        {
            ClienteNegocio cliente = new ClienteNegocio();
            List<Cliente> clientesActivos = cliente.listarConSP().FindAll(x => x.Activo);

            ddlCliente.DataSource = clientesActivos;
            ddlCliente.DataTextField = "NombreCompleto";
            ddlCliente.DataValueField = "Id";
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0, new ListItem("Seleccione un cliente", ""));

        }

        private void cargarGrilla()//revisar
        {
            List<DetalleVenta> detalle = Session["DetalleVenta"] as List<DetalleVenta>;

            if (detalle == null)
            {
                detalle = new List<DetalleVenta>();
                Session["DetalleVenta"] = detalle;
            }

            dgvLineasVenta.DataSource = detalle;
            dgvLineasVenta.DataBind();

            decimal total = 0;
            foreach (DetalleVenta item in detalle)
            {
                total += item.Cantidad * item.PrecioUnitario;
            }
            txtTotalFinal.Text = total.ToString("0.00");
        }

        private void cargarProductos()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> productosActivos = negocio.listarConSP().FindAll(x => x.Activo);
            productosActivos.Sort((a, b) => a.Nombre.CompareTo(b.Nombre));

            ddlProducto.DataSource = productosActivos;
            ddlProducto.DataTextField = "Nombre";
            ddlProducto.DataValueField = "Id";
            ddlProducto.DataBind();

            ddlProducto.Items.Insert(0, new ListItem("Seleccione un producto", ""));
                
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ddlProducto.SelectedValue))
            {
                txtPrecioVenta.Text = string.Empty;
                return;
            }

            //Obtengo el Id seleccionado y traigo la lista desde ProductoNegocio
            int idSeleccionado = int.Parse(ddlProducto.SelectedValue);
            ProductoNegocio negocio = new ProductoNegocio();
            Producto productoSleccionado = negocio.listarConSP().Find(x => x.Id == idSeleccionado);

            Producto productoSeleccionado = null;

            if (productoSeleccionado != null)
                txtPrecioVenta.Text = calcularPrecioVenta(productoSeleccionado).ToString();
            else
                txtPrecioVenta.Text = string.Empty;
         }
        protected void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;

                if (string.IsNullOrEmpty(ddlProducto.SelectedValue))
                {
                    lblError.Text = "Debe seleccionar un producto";
                    lblError.Visible = true;
                    return;
                }

                int cantidadNueva;
                if (!int.TryParse(txtCantidad.Text.Trim(), out cantidadNueva) || cantidadNueva <= 0)
                {
                    lblError.Text = "Ingrese una cantidad mayor a cero";
                    lblError.Visible = true;
                    return;
                }

                int idProducto = int.Parse(ddlProducto.SelectedValue);
                ProductoNegocio negocio = new ProductoNegocio();
                Producto productoSeleccionado = negocio.listarConSP().Find(x => x.Id == idProducto);

                if(productoSeleccionado == null)
                {
                    lblError.Text = "No se encontró el producto seleccionado";
                    lblError.Visible = true;
                    return;
                }
                
                List<DetalleVenta> detalle = Session["DetalleVenta"] as List<DetalleVenta>;
                if (detalle == null)
                {
                    detalle = new List<DetalleVenta>();
                    Session["DetalleVenta"] = detalle;
                }
                //verifico que el stock disponible sea suficiente para la compra
                int cantidadYaCargada = 0;
                foreach(DetalleVenta item in detalle)
                {
                    if(item.ProductoAsociado != null && item.ProductoAsociado.Id == idProducto)
                    {
                        cantidadYaCargada += item.Cantidad;
                    }
                }

                if(cantidadYaCargada + cantidadNueva > productoSeleccionado.StockActual)
                {
                    lblError.Text = "No hay stock suficiente. Stock disponible: " + productoSeleccionado.StockActual;
                    lblError.Visible = true;
                    return;
                }

                DetalleVenta nuevaLinea = new DetalleVenta();
                nuevaLinea.ProductoAsociado = productoSeleccionado;
                nuevaLinea.Cantidad = cantidadNueva;
                nuevaLinea.PrecioUnitario = calcularPrecioVenta(productoSeleccionado);

                detalle.Add(nuevaLinea);
                Session["DetalleVenta"] = detalle;

                cargarLineas();
                txtCantidad.Text = string.Empty;
                txtPrecioVenta.Text = string.Empty;
                ddlProducto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;                
            }
        }

        protected void dgvLineasVenta_BorrarLinea(object sender, GridViewDeleteEventArgs e)
        {
            List<DetalleVenta> detalle = Session["DetalleVenta"] as List<DetalleVenta>;

            if(detalle != null && e.RowIndex >= 0 && e.RowIndex < detalle.Count)
            {
                detalle.RemoveAt(e.RowIndex);
                Session["DetalleVenta"] = detalle;
                cargarLineas();
            }
        }

        protected void cargarLineas()
        {
            List<DetalleVenta> lineas = (List<DetalleVenta>)Session["DetalleVenta"];
            if (lineas == null)
            {
                lineas = new List<DetalleVenta>();
                Session["DetalleVenta"] = lineas;
            }

            dgvLineasVenta.DataSource = lineas;
            dgvLineasVenta.DataBind();

            decimal total = 0;
            foreach (DetalleVenta item in lineas)
            {
               total += item.Cantidad * item.PrecioUnitario;
            }
                txtTotalFinal.Text = total.ToString("0.00");
        }

        private decimal calcularPrecioVenta(Producto prod)
        {
            decimal montoGanancia = (prod.PrecioCompraActual * prod.PorcentajeGanancia) / 100;
            return prod.PrecioCompraActual + montoGanancia;
        }
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;

                if (string.IsNullOrEmpty(ddlCliente.SelectedValue))
                {
                    lblError.Text = "Debe seleccionar un cliente.";
                    lblError.Visible = true;
                    return;
                }

                List<DetalleVenta> detalle = Session["DetalleVenta"] as List<DetalleVenta>;
                if (detalle == null || detalle.Count == 0)
                {
                    lblError.Text = "Debe agregar al menos un producto antes de confirmar la venta";
                    lblError.Visible = true;
                    return;
                }
                //calculo del total general
                decimal totalVenta = 0;
                foreach (DetalleVenta item in detalle)
                {
                   totalVenta += item.Cantidad * item.PrecioUnitario;
                }

                txtTotalFinal.Text = totalVenta.ToString("0.00");

                VentaNegocio venta = new VentaNegocio();
                string numeroGenerado = venta.obtenerProximoNumeroFactura();

                Venta nuevaVenta = new Dominio.Venta();
                nuevaVenta.NumeroFactura = numeroGenerado;
                nuevaVenta.Fecha = DateTime.Now; 

                nuevaVenta.ClienteAsociado = new Dominio.Cliente { Id = int.Parse(ddlCliente.SelectedValue)};
                nuevaVenta.UsuarioAsociado = (Usuario)Session["SessionActiva"];

                nuevaVenta.TotalFinal = totalVenta;

                int idVentaGenerada = venta.agregar(nuevaVenta);
                foreach (DetalleVenta item in detalle)
                {
                    venta.agregarLinea(
                        idVentaGenerada,
                        item.ProductoAsociado.Id,
                        item.Cantidad,
                        item.PrecioUnitario
                    );
                }
                nuevaVenta.LineasVenta = detalle;
                Session["facturaVenta"] = nuevaVenta;
                Session["facturaCliente"] = ddlCliente.SelectedItem.Text;

                limpiarFormulario();
                Response.Redirect("~/Mod_Ventas/Factura.aspx", false);

               // lblMensajeExito.Text = "¡Venta realizada con éxito! Nro. de factura: " + nuevaVenta.NumeroFactura;
               // pnlVentaExitosa.Visible = true;
               // Response.Redirect("~/Mod_Ventas/Factura.aspx", false);

            }
            catch ( Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }
        protected void btnCancelar_Click(Object sender, EventArgs e)
        {
            limpiarFormulario();
        }

        protected void btnAceptarMensaje_Click(Object sender, EventArgs e)
        {
            pnlVentaExitosa.Visible = false;
        }
        protected void limpiarFormulario()
        {
            Session.Remove("DetalleVenta");
            cargarGrilla();

            if (ddlCliente.Items.Count > 0)
            {
                ddlCliente.SelectedIndex = 0;
            }

            if (ddlProducto.Items.Count > 0)
            {
                ddlProducto.SelectedIndex = 0;
            }

            //limpia los campos de cabecera
            txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtTotalFinal.Text = "0.00";
            txtCantidad.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            lblError.Visible = false;

            VentaNegocio negocio = new VentaNegocio();
            txtNumeroFactura.Text = negocio.obtenerProximoNumeroFactura();
        }

        protected void chkIncluir_CheckedChanged(object sender, EventArgs e)//
        {
            try
            {
                lblError.Visible = false;

                CheckBox chk = (CheckBox)sender;
                if(!chk.Checked)
                {
                    GridViewRow fila = (GridViewRow)chk.NamingContainer;
                    int indice = fila.RowIndex;

                    List<DetalleVenta> detalle = Session["DetalleVenta"] as List<DetalleVenta>;
                    if(detalle != null && indice >= 0 && indice < detalle.Count)
                    {
                        detalle.RemoveAt(indice);
                        Session["DetalleVenta"] = detalle;

                        cargarGrilla();
                        actualizarTotalVenta(detalle);
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private void actualizarTotalVenta(List<DetalleVenta> detalle)
        {
            decimal totalVenta = 0;

            if(detalle != null)
            {
                foreach(DetalleVenta item in detalle)
                {
                    decimal subtotalLinea = item.Cantidad * item.PrecioUnitario;
                    totalVenta += subtotalLinea;
                }
            }

            txtTotalFinal.Text = totalVenta.ToString("0.00");
        }
    }
}