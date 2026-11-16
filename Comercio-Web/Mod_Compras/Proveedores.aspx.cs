using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Compras
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }
        private void cargarGrilla()
        {
            try
            {
                ProveedorNegocio negocio = new ProveedorNegocio();
                dgvProveedores.DataSource = negocio.listar();
                dgvProveedores.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Proveedor nuevo = new Proveedor();
                ProveedorNegocio negocio = new ProveedorNegocio();

                nuevo.RazonSocial = txtRazonSocial.Text;
                nuevo.Cuit = txtCuit.Text;
                nuevo.Email = txtEmail.Text;
                nuevo.Telefono = txtTelefono.Text;
                nuevo.Activo = true;

                negocio.agregar(nuevo);
                cargarGrilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvProveedores.SelectedDataKey != null)
                {
                    int id = int.Parse(dgvProveedores.SelectedDataKey.Value.ToString());

                    ProveedorNegocio negocio = new ProveedorNegocio();
                    negocio.eliminar(id);

                    cargarGrilla();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            dgvProveedores.SelectedIndex = -1;
        }
    }
}