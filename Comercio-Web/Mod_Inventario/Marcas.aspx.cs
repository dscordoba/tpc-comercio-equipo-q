using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Mod_Inventario
{
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarTabla();
            }
        }

        // al elegir una fila, pasa los datos al formulario
        protected void dgvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSeleccionado = Convert.ToInt32(dgvMarcas.SelectedDataKey.Value);

            MarcaNegocio negocio = new MarcaNegocio();
            Marca seleccionada = negocio.listar().Find(x => x.Id == idSeleccionado);

            if (seleccionada != null)
            {
                hfMarcaId.Value = seleccionada.Id.ToString();
                txtId.Text = seleccionada.Id.ToString();
                txtDescripcion.Text = seleccionada.Descripcion;

                rbActivoSI.Checked = seleccionada.Activo;
                rbActivoNO.Checked = !seleccionada.Activo;
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Marca marca = new Marca();
                marca.Descripcion = txtDescripcion.Text.Trim();
                marca.Activo = rbActivoSI.Checked;

                MarcaNegocio negocio = new MarcaNegocio();

                // si no hay id es una marca nueva, si hay id se modifica
                if (string.IsNullOrEmpty(hfMarcaId.Value))
                {
                    negocio.agregar(marca);
                }
                else
                {
                    marca.Id = int.Parse(hfMarcaId.Value);
                    negocio.modificar(marca);
                }

                cargarTabla();
                limpiarFormulario();
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
                int idMarca = int.Parse(hfMarcaId.Value);
                MarcaNegocio negocio = new MarcaNegocio();

                negocio.eliminar(idMarca);

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

        // llena la grilla con las marcas de la base
        private void cargarTabla()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            dgvMarcas.DataSource = negocio.listar();
            dgvMarcas.DataBind();
        }

        // deja el formulario vacio
        private void limpiarFormulario()
        {
            hfMarcaId.Value = string.Empty;
            txtId.Text = string.Empty;
            txtDescripcion.Text = string.Empty;

            rbActivoSI.Checked = true;
            rbActivoNO.Checked = false;

            dgvMarcas.SelectedIndex = -1;
        }

    }
}