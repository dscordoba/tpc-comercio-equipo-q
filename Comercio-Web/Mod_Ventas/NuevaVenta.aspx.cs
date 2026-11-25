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
        private void cargarGrilla()
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                cargarClientes();
                cargarProductos();
                cargarGrilla();
                txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }
        private void cargarProductos()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> todosLosProductos = negocio.listarConSP();
            List<Producto> productosActivos = new List<Producto>();

            foreach(Producto prod in todosLosProductos)
            {
                if(prod.Activo)
                {
                    productosActivos.Add(prod);
                }
            }

            ddlProducto.DataSource = productosActivos;
            ddlProducto.DataTextField = "Nombre";
            ddlProducto.DataValueField = "Id";
            ddlProducto.DataBind();

            ddlProducto.Items.Insert(0, new ListItem("Seleccione un producto", ""));
                
        }

        private void cargarClientes()
        {
            ClienteNegocio cliente = new ClienteNegocio();
            List<Cliente> todosLosClientes = cliente.listarConSP();
            List<Cliente> clientesActivos = new List<Cliente>();

            foreach (Cliente cli in todosLosClientes)
            {
                if (cli.Activo)
                {
                    clientesActivos.Add(cli);
                }
            }

            ddlCliente.DataSource = clientesActivos;
            ddlCliente.DataTextField = "NombreCompleto";
            ddlCliente.DataValueField = "Id";
            ddlCliente.DataBind();

            ddlCliente.Items.Insert(0, new ListItem("Seleccione un cliente", ""));
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
            List<Producto> lista = negocio.listarConSP();

            Producto productoSeleccionado = null;

            //busco el producto y cuando es encontrado salgo de la lista
            foreach(Producto prod in lista)
            {
                if(prod.Id == idSeleccionado)
                {
                    productoSeleccionado = prod;
                    break;
                }
            }
            //calculo de el precio de venta sumando el % de ganancia 
            if(productoSeleccionado != null)
            {
                decimal montoGanancia = (productoSeleccionado.PrecioCompraActual * productoSeleccionado.PorcentajeGanancia) / 100;
                decimal precioVenta = productoSeleccionado.PrecioCompraActual + montoGanancia;

                txtPrecioVenta.Text = precioVenta.ToString("0.00");
            }
            else
            {
                txtPrecioVenta.Text = string.Empty;
            }
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

                int cantidadNueva = int.Parse(txtCantidad.Text);
                if (cantidadNueva <= 0)
                {
                    lblError.Text = "Ingrese una cantidad mayor a cero";
                    lblError.Visible = true;
                    return;
                }

                //busco el producto
                int idProducto = int.Parse(ddlProducto.SelectedValue);
                ProductoNegocio negocio = new ProductoNegocio();
                List<Producto> listaProductos = negocio.listarConSP();

                Producto productoSeleccionado = null;
                foreach (Producto prod in listaProductos)
                {
                    if (prod.Id == idProducto)
                    {
                        productoSeleccionado = prod;
                        break;
                    }
                }

                if (productoSeleccionado == null)
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

                //calculo del precio unitario
                decimal montoGanancia = (productoSeleccionado.PrecioCompraActual * productoSeleccionado.PorcentajeGanancia) / 100;
                decimal precioUnitario = productoSeleccionado.PrecioCompraActual + montoGanancia;

                DetalleVenta nuevaLinea = new DetalleVenta();
                nuevaLinea.ProductoAsociado = productoSeleccionado;
                nuevaLinea.Cantidad = cantidadNueva;
                nuevaLinea.PrecioUnitario = precioUnitario;

                detalle.Add(nuevaLinea);

                cargarGrilla();
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

                DateTime fechaVenta = DateTime.Today;

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
                    decimal subtotalLinea = item.Cantidad * item.PrecioUnitario;
                    totalVenta += subtotalLinea;
                }

                txtTotalFinal.Text = totalVenta.ToString("0.00");

                VentaNegocio venta = new VentaNegocio();
                string numeroGenerado = venta.obtenerProximoNumeroFactura();

                Venta nuevaVenta = new Dominio.Venta();
                nuevaVenta.NumeroFactura = numeroGenerado;
                nuevaVenta.Fecha = fechaVenta;

                nuevaVenta.ClienteAsociado = new Dominio.Cliente();
                nuevaVenta.ClienteAsociado.Id = int.Parse(ddlCliente.SelectedValue);
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
                limpiarFormulario();

                lblMensajeExito.Text = "¡Venta realizada con éxito! Nro. de factura: " + nuevaVenta.NumeroFactura;
                pnlVentaExitosa.Visible = true;

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
            txtTotalFinal.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            lblError.Visible = false;

            VentaNegocio negocio = new VentaNegocio();
            txtNumeroFactura.Text = negocio.obtenerProximoNumeroFactura();
        }

        protected void chkIncluir_CheckedChanged(object sender, EventArgs e)
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