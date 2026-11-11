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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarTabla();
            }
        }

        // Capturo el ID de la tabla para cargar datos en el formulario
        protected void dgvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSeleccionado = Convert.ToInt32(dgvClientes.SelectedDataKey.Value);

            ClienteNegocio negocio = new ClienteNegocio();
            Cliente seleccionado = negocio.listar().Find(x => x.Id == idSeleccionado);

            if (seleccionado != null)
            {
                hfClienteId.Value = seleccionado.Id.ToString();
                txtId.Text = seleccionado.Id.ToString();
                txtNombre.Text = seleccionado.Nombre;
                txtApellido.Text = seleccionado.Apellido;
                txtDni.Text = seleccionado.Dni;
                txtEmail.Text = seleccionado.Email;

                rbActivoSI.Checked = seleccionado.Activo;
                rbActivoNO.Checked = !seleccionado.Activo;
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Nombre = txtNombre.Text.Trim();
                cliente.Apellido = txtApellido.Text.Trim();
                cliente.Dni = txtDni.Text.Trim();
                cliente.Email = txtEmail.Text.Trim();
                cliente.Activo = rbActivoSI.Checked;

                ClienteNegocio negocio = new ClienteNegocio();

                // Valido si es cliente nuevo
                if (string.IsNullOrEmpty(hfClienteId.Value))
                {
                    negocio.agregar(cliente);
                }
                else
                {
                    cliente.Id = int.Parse(hfClienteId.Value);
                    negocio.modificar(cliente);
                }

                cargarTabla();
                limpiarFormulario();
            }
            catch (Exception ex)
            {
                // Manejo de validaciones (DNI o Email duplicado)
                throw ex;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente = int.Parse(hfClienteId.Value);
                ClienteNegocio negocio = new ClienteNegocio();

                // Verificar si está en la tabla Ventas
                if (negocio.tieneVentas(idCliente))
                {
                    return;
                }

                negocio.eliminar(idCliente);

                cargarTabla();
                limpiarFormulario();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarFormulario();
        }

        // Función para cargar datos en la tabla
        private void cargarTabla()
        {
            ClienteNegocio negocio = new ClienteNegocio();
            dgvClientes.DataSource = negocio.listar();
            dgvClientes.DataBind();
        }

        // Función para limpiar los inputs
        private void limpiarFormulario()
        {
            hfClienteId.Value = string.Empty;
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDni.Text = string.Empty;
            txtEmail.Text = string.Empty;

            rbActivoSI.Checked = true;
            rbActivoNO.Checked = false;

            dgvClientes.SelectedIndex = -1;
        }

    }
}