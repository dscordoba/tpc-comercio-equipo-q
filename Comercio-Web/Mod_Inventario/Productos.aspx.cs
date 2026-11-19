using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Inventario
{
    public partial class Producto : System.Web.UI.Page
    {
        // Función para cargar datos en la tabla
        private void cargarTabla()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            dgvProductos.DataSource = negocio.listarConSP();
            dgvProductos.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnInactivar.Visible = false;
                btnInactivar.Enabled = false;
                divRbActivoNo.Visible = false;
                lblError.Visible = false;
                cargarDesplegables();
                cargarTabla();
            }
        }

        // Carga los desplegables de MARCA, CATEGORÍA Y PROVEEDORES (Solo activos)
        private void cargarDesplegables()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();

            try
            {
                // Categorías
                ddlCategoria.DataSource = categoriaNegocio.listar().FindAll(x => x.Activo);
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataTextField = "Descripcion";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", ""));

                // Marcas 
                ddlMarca.DataSource = marcaNegocio.listar().FindAll(x => x.Activo);
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataTextField = "Descripcion";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Seleccione una marca", ""));

                // Proveedores 
                lbProveedores.DataSource = proveedorNegocio.listar().FindAll(x => x.Activo);
                lbProveedores.DataValueField = "Id";
                lbProveedores.DataTextField = "RazonSocial";
                lbProveedores.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }

        // Capturo el ID de la tabla para cargar datos en el formulario
        protected void dgvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSeleccionado = Convert.ToInt32(dgvProductos.SelectedDataKey.Value);

            ProductoNegocio negocio = new ProductoNegocio();
            Dominio.Producto seleccionado = negocio.listarConSP().Find(x => x.Id == idSeleccionado);

            if (seleccionado != null)
            {
                lblError.Visible = false;
                Session.Add("productoSeleccionado", seleccionado);

                txtId.Text = seleccionado.Id.ToString();
                txtCodigoArt.Text = seleccionado.CodigoProducto;
                txtNombre.Text = seleccionado.Nombre;
                txtDescripcion.Text = seleccionado.Descripcion;
                txtStockActual.Text = seleccionado.StockActual.ToString();
                txtStockMinimo.Text = seleccionado.StockMinimo.ToString();
                txtPorcentajeGanancia.Text = seleccionado.PorcentajeGanancia.ToString();
                txtPrecioActual.Text = seleccionado.PrecioCompraActual.ToString();

                // Desplegables (Marca y Categoría)
                ddlMarca.SelectedValue = seleccionado.MarcaAsociada.Id.ToString();
                ddlCategoria.SelectedValue = seleccionado.CategoriaAsociada.Id.ToString();

                // ListBox múltiple (Proveedores)
                lbProveedores.ClearSelection(); // Limpia selecciones anteriores
                if (seleccionado.ProveedoresAsociados != null)
                {
                    foreach (Proveedor prov in seleccionado.ProveedoresAsociados)
                    {
                        ListItem item = lbProveedores.Items.FindByValue(prov.Id.ToString());
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                    }
                }

                rbActivoSI.Checked = seleccionado.Activo;
                rbActivoNO.Checked = !seleccionado.Activo;

                btnInactivar.Visible = true;
                btnInactivar.Enabled = true;

                if (!seleccionado.Activo)
                {
                    btnInactivar.Text = "Reactivar";
                    btnInactivar.CssClass = "btn btn-success";
                    divRbActivoSi.Visible = false;
                    divRbActivoNo.Visible = true;
                }
                else
                {
                    btnInactivar.Text = "Inactivar";
                    btnInactivar.CssClass = "btn btn-danger";
                    divRbActivoNo.Visible = false;
                    divRbActivoSi.Visible = true;
                }
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {

        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}