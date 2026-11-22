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
                btnInactivar.Visible = false;
                btnInactivar.Enabled = false;
                divRbActivoNo.Visible = false;
                cargarGrilla();
            }
        }
        private void cargarGrilla()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            dgvProveedores.DataSource = negocio.listarConSP();
            dgvProveedores.DataBind();
        }

        private void limpiarFormulario()
        {
            Session.Remove("proveedorSeleccionado");
            txtId.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtCuit.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;

            rbActivoSI.Checked = true;
            rbActivoNO.Checked = false;

            lblError.Visible = false;
        }
        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;

                if (string.IsNullOrWhiteSpace(txtRazonSocial.Text) ||
                    !Validaciones.SoloNumeros(txtCuit.Text.Trim()) ||
                    txtCuit.Text.Trim().Length < 11 ||
                    !Validaciones.EmailValido(txtEmail.Text) ||
                    !Validaciones.SoloNumeros(txtTelefono.Text.Trim()) ||
                    txtTelefono.Text.Trim().Length < 10)
                {
                    lblError.Text = "Verifique que los campos obligatorios sean válidos.";
                    lblError.Visible = true;
                    return;
                }

                Proveedor proveedor = new Proveedor();
                proveedor.RazonSocial = txtRazonSocial.Text.Trim();
                proveedor.Cuit = txtCuit.Text.Trim();
                proveedor.Email = txtEmail.Text.Trim();
                proveedor.Telefono = txtTelefono.Text.Trim();

                ProveedorNegocio negocio = new ProveedorNegocio();

                if (Session["proveedorSeleccionado"] == null)
                {
                    negocio.agregar(proveedor);
                }
                else
                {
                    Proveedor seleccionado = (Proveedor)Session["proveedorSeleccionado"];
                    proveedor.Id = seleccionado.Id;
                    negocio.modificar(proveedor);
                }

                Session.Remove("proveedorSeleccionado");
                cargarGrilla();
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
                if(Session["proveedorSeleccionado"] != null)
                {
                    Proveedor seleccionado = (Proveedor)Session["proveedorSeleccionado"];
                    ProveedorNegocio negocio = new ProveedorNegocio();

                    negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);

                    Session.Remove("proveedorSeleccionado");
                    cargarGrilla();
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
    }
}