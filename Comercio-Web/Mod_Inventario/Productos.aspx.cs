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
            if (!Seguridad.esAdmin(Session["SessionActiva"]))
            {
                Session.Add("error", "Se requiere permiso de administrador para acceder a esta pantalla.");
                Response.Redirect("~/Error.aspx", false);
            }

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
            try
            {
                lblError.Visible = false;

                // Validaciones 
                bool tieneProveedores = lbProveedores.Items.Cast<ListItem>().Any(item => item.Selected);

                if (string.IsNullOrWhiteSpace(txtCodigoArt.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrEmpty(ddlCategoria.SelectedValue) ||
                    string.IsNullOrEmpty(ddlMarca.SelectedValue) ||
                    !tieneProveedores ||
                    !Validaciones.NumeroDecimalPositivo(txtPorcentajeGanancia.Text, 999.99m) ||
                    !Validaciones.SoloNumeros(txtStockMinimo.Text.Trim()))
                {
                    lblError.Text = "Verifique que todos los campos obligatorios sean válidos.";
                    lblError.Visible = true;
                    return;
                }

                Dominio.Producto producto = new Dominio.Producto();
                producto.CodigoProducto = txtCodigoArt.Text.Trim();
                producto.Nombre = txtNombre.Text.Trim();
                producto.Descripcion = txtDescripcion.Text.Trim();

                // Normaliza el decimal por si ingresan coma
                string gananciaNormalizada = txtPorcentajeGanancia.Text.Trim().Replace(',', '.');
                producto.PorcentajeGanancia = decimal.Parse(gananciaNormalizada, System.Globalization.CultureInfo.InvariantCulture);

                producto.StockMinimo = int.Parse(txtStockMinimo.Text.Trim());

                // Captura de marca y categoria
                producto.MarcaAsociada = new Marca();
                producto.MarcaAsociada.Id = int.Parse(ddlMarca.SelectedValue);

                producto.CategoriaAsociada = new Categoria();
                producto.CategoriaAsociada.Id = int.Parse(ddlCategoria.SelectedValue);

                // Captura de lista de proveedores seleccionados
                producto.ProveedoresAsociados = new List<Proveedor>();
                foreach (ListItem item in lbProveedores.Items)
                {
                    if (item.Selected)
                    {
                        Proveedor prov = new Proveedor();
                        prov.Id = int.Parse(item.Value);
                        producto.ProveedoresAsociados.Add(prov);
                    }
                }

                ProductoNegocio negocio = new ProductoNegocio();

                // Valida Session para saber si agrega o modifica
                if (Session["productoSeleccionado"] == null)
                {
                    negocio.agregar(producto);
                }
                else
                {
                    Dominio.Producto seleccionado = (Dominio.Producto)Session["productoSeleccionado"];
                    producto.Id = seleccionado.Id;
                    negocio.modificar(producto);
                }
                Session.Remove("productoSeleccionado");
                cargarTabla();
                limpiarFormulario();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["productoSeleccionado"] != null)
                {
                    Dominio.Producto seleccionado = (Dominio.Producto)Session["productoSeleccionado"];
                    ProductoNegocio negocio = new ProductoNegocio();

                    negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);

                    Session.Remove("productoSeleccionado");
                    cargarTabla();
                    limpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarFormulario();
        }

        // Función para limpiar los inputs
        private void limpiarFormulario()
        {
            Session.Remove("productoSeleccionado");

            txtId.Text = string.Empty;
            txtCodigoArt.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecioActual.Text = string.Empty;
            txtPorcentajeGanancia.Text = string.Empty;
            txtStockActual.Text = string.Empty;
            txtStockMinimo.Text = string.Empty;

            ddlCategoria.SelectedIndex = 0;
            ddlMarca.SelectedIndex = 0;

            lbProveedores.ClearSelection(); // Limpia seleccionados

            rbActivoSI.Checked = true;
            rbActivoNO.Checked = false;
            rbActivoSI.Enabled = false;
            rbActivoNO.Enabled = false;

            divRbActivoSi.Visible = true;
            divRbActivoNo.Visible = false;

            dgvProductos.SelectedIndex = -1;

            btnInactivar.Visible = false;
            btnInactivar.Enabled = false;

            lblError.Text = string.Empty;
            lblError.Visible = false;
        }

    }
}