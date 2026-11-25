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
            if (!IsPostBack)
            {
                ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
                ddlProveedor.DataSource = proveedorNegocio.listar();
                ddlProveedor.DataValueField = "Id";
                ddlProveedor.DataTextField = "RazonSocial";
                ddlProveedor.DataBind();

                ProductoNegocio productoNegocio = new ProductoNegocio();
                ddlProducto.DataSource = productoNegocio.listarConSP();
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
    }
}
