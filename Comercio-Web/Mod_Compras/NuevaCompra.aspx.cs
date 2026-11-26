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
    public partial class NuevaCompra : System.Web.UI.Page
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
                ddlProveedor.DataSource = proveedorNegocio.listar().FindAll(x => x.Activo);
                ddlProveedor.DataValueField = "Id";
                ddlProveedor.DataTextField = "RazonSocial";
                ddlProveedor.DataBind();

                ProductoNegocio productoNegocio = new ProductoNegocio();
                List<Producto> productosActivos = productoNegocio.listarConSP().FindAll(x => x.Activo);
                productosActivos.Sort((a, b) => a.Nombre.CompareTo(b.Nombre));
                ddlProducto.DataSource = productosActivos;
                ddlProducto.DataValueField = "Id";
                ddlProducto.DataTextField = "Nombre";
                ddlProducto.DataBind();


                txtFecha.Attributes["max"] = DateTime.Today.ToString("yyyy-MM-dd");

                Session.Remove("lineasCompra");
                cargarLineas();
            }
        }
        private void cargarLineas()
        {
            List<DetalleCompra> lineas = (List<DetalleCompra>)Session["lineasCompra"];
            if (lineas == null)
                lineas = new List<DetalleCompra>();

            dgvLineasCompra.DataSource = lineas;
            dgvLineasCompra.DataBind();

            decimal total = 0;
            foreach (DetalleCompra linea in lineas)
            {
                total += linea.Subtotal;
            }
            txtTotal.Text = total.ToString();
        }

        protected void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;

                if (ddlProducto.SelectedValue == "")
                {
                    lblError.Text = "Seleccione un producto";
                    lblError.Visible = true;
                    return;
                }

                if (!Validaciones.SoloNumeros(txtCantidad.Text) || int.Parse(txtCantidad.Text) <= 0)
                {
                    lblError.Text = "La cantidad debe ser un número entero mayor a 0";
                    lblError.Visible = true;
                    return;
                }

                if (!Validaciones.NumeroDecimalPositivo(txtPrecioCompra.Text))
                {
                    lblError.Text = "El precio debe ser un número mayor a 0";
                    lblError.Visible = true;
                    return;
                }

                DetalleCompra linea = new DetalleCompra();
                linea.ProductoAsociado = new Producto();
                linea.ProductoAsociado.Id = int.Parse(ddlProducto.SelectedValue);
                linea.ProductoAsociado.Nombre = ddlProducto.SelectedItem.Text;
                linea.Cantidad = int.Parse(txtCantidad.Text);

                string precioNormalizado = txtPrecioCompra.Text.Trim().Replace(',', '.');
                linea.PrecioCompra = decimal.Parse(precioNormalizado, System.Globalization.CultureInfo.InvariantCulture);

                List<DetalleCompra> lineas = (List<DetalleCompra>)Session["lineasCompra"];
                if (lineas == null)
                    lineas = new List<DetalleCompra>();

                lineas.Add(linea);
                Session["lineasCompra"] = lineas;

                cargarLineas();

                ddlProducto.SelectedIndex = 0;
                txtCantidad.Text = "";
                txtPrecioCompra.Text = "";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }
        protected void dgvLineasCompra_BorrarLinea(object sender, GridViewDeleteEventArgs e)
        {
            List<DetalleCompra> lineas = (List<DetalleCompra>)Session["lineasCompra"];

            if (lineas != null)
            {
                lineas.RemoveAt(e.RowIndex);
                cargarLineas();
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;
                lblExito.Visible = false;

                if (ddlProveedor.SelectedValue == "")
                {
                    lblError.Text = "Seleccione un proveedor";
                    lblError.Visible = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFecha.Text))
                {
                    lblError.Text = "Ingrese la fecha de la compra";
                    lblError.Visible = true;
                    return;
                }

                DateTime fecha = DateTime.Parse(txtFecha.Text);
                if (fecha > DateTime.Today)
                {
                    lblError.Text = "La fecha no puede ser futura";
                    lblError.Visible = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNumeroFactura.Text))
                {
                    lblError.Text = "Ingrese el número de factura";
                    lblError.Visible = true;
                    return;
                }

                if (!Validaciones.SoloAlfanumerico(txtNumeroFactura.Text.Trim()) || txtNumeroFactura.Text.Trim().Length > 8)
                {
                    lblError.Text = "El número de factura debe ser alfanumérico, de hasta 8 caracteres";
                    lblError.Visible = true;
                    return;
                }

                List<DetalleCompra> lineas = (List<DetalleCompra>)Session["lineasCompra"];
                if (lineas == null || lineas.Count == 0)
                {
                    lblError.Text = "Agregue al menos un producto";
                    lblError.Visible = true;
                    return;
                }

                int idProveedor = int.Parse(ddlProveedor.SelectedValue);
                string factura = txtNumeroFactura.Text.Trim();

                CompraNegocio negocio = new CompraNegocio();

                if (negocio.ExisteFactura(idProveedor, factura))
                {
                    lblError.Text = "Ya existe una compra con ese número de factura para este proveedor";
                    lblError.Visible = true;
                    return;
                }

                Compra nueva = new Compra();
                nueva.NumeroFactura = factura;
                nueva.Fecha = fecha;
                nueva.ProveedorAsociado = new Proveedor();
                nueva.ProveedorAsociado.Id = idProveedor;

                int idCompra = negocio.agregar(nueva);

                foreach (DetalleCompra linea in lineas)
                {
                    negocio.agregarLinea(idCompra, idProveedor, linea.ProductoAsociado.Id, linea.Cantidad, linea.PrecioCompra);
                }

                limpiarFormulario();
                lblExito.Text = "Compra registrada con éxito";
                lblExito.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarFormulario();
        }

        private void limpiarFormulario()
        {
            Session.Remove("lineasCompra");
            cargarLineas();

            ddlProveedor.SelectedIndex = 0;
            ddlProducto.SelectedIndex = 0;
            txtFecha.Text = "";
            txtNumeroFactura.Text = "";
            txtCantidad.Text = "";
            txtPrecioCompra.Text = "";
            lblError.Visible = false;
            lblExito.Visible = false;
        }
    }
}
