using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Ventas
{
    public partial class Clientes : System.Web.UI.Page
    {
        // Función para cargar datos en la tabla
        private void cargarTabla()
        {
            ClienteNegocio negocio = new ClienteNegocio();
            dgvClientes.DataSource = negocio.listarConSP();
            dgvClientes.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnInactivar.Visible = false;
                btnInactivar.Enabled = false;
                divRbActivoNo.Visible = false;
                lblError.Visible = false; 
                cargarTabla();
            }
        }

        // Capturo el ID de la tabla para cargar datos en el formulario
        protected void dgvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSeleccionado = Convert.ToInt32(dgvClientes.SelectedDataKey.Value);

            ClienteNegocio negocio = new ClienteNegocio();
            Cliente seleccionado = negocio.listarConSP().Find(x => x.Id == idSeleccionado);

            if (seleccionado != null)
            {
                lblError.Visible = false;

                Session.Add("clienteSeleccionado", seleccionado);

                txtId.Text = seleccionado.Id.ToString();
                txtNombre.Text = seleccionado.Nombre;
                txtApellido.Text = seleccionado.Apellido;
                txtDni.Text = seleccionado.Dni;
                txtEmail.Text = seleccionado.Email;

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

                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    !Validaciones.SoloNumeros(txtDni.Text.Trim()) ||
                    txtDni.Text.Trim().Length < 7 ||
                    !Validaciones.EmailValido(txtEmail.Text))
                {
                    lblError.Text = "Verifique que los campos obligatorios sean válidos.";
                    lblError.Visible = true;
                    return;
                }

                Cliente cliente = new Cliente();
                cliente.Nombre = txtNombre.Text.Trim();
                cliente.Apellido = txtApellido.Text.Trim();
                cliente.Dni = txtDni.Text.Trim();
                cliente.Email = txtEmail.Text.Trim();

                ClienteNegocio negocio = new ClienteNegocio();

                // Valido si hay un cliente seleccionado en Sesión
                if (Session["clienteSeleccionado"] == null)
                {
                    negocio.agregar(cliente);
                }
                else
                {
                    Cliente seleccionado = (Cliente)Session["clienteSeleccionado"];
                    cliente.Id = seleccionado.Id;
                    negocio.modificar(cliente);
                }

                Session.Remove("clienteSeleccionado"); 
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
                if (Session["clienteSeleccionado"] != null)
                {
                    Cliente seleccionado = (Cliente)Session["clienteSeleccionado"];
                    ClienteNegocio negocio = new ClienteNegocio();

                    // Verificar si está en la tabla Ventas (solo si lo vas a inactivar)
                    /*if (seleccionado.Activo && negocio.tieneVentas(seleccionado.Id))
                    {
                        return;
                    }*/

                    negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);

                    Session.Remove("clienteSeleccionado");
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
            Session.Remove("clienteSeleccionado");
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDni.Text = string.Empty;
            txtEmail.Text = string.Empty;

            rbActivoSI.Checked = true;
            rbActivoNO.Checked = false;

            dgvClientes.SelectedIndex = -1;

            btnInactivar.Visible = false;
            btnInactivar.Enabled = false;

            divRbActivoSi.Visible = true;
            divRbActivoNo.Visible = false;

            lblError.Visible = false;
        }

    }
}