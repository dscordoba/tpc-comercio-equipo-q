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
                btnInactivar.Visible = false;
                divRbActivoNo.Visible = false;
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
               
                Session.Add("marcaSeleccionada", seleccionada);
                txtId.Text = seleccionada.Id.ToString();
                txtDescripcion.Text = seleccionada.Descripcion;

                rbActivoSI.Checked = seleccionada.Activo;
                rbActivoNO.Checked = !seleccionada.Activo;
                btnInactivar.Visible = true;

                if (!seleccionada.Activo)
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
                Marca marca = new Marca();
                marca.Descripcion = txtDescripcion.Text.Trim();
                

                MarcaNegocio negocio = new MarcaNegocio();

                // si no hay id es una marca nueva, si hay id se modifica
                if (Session["marcaSeleccionada"] == null)
                {
                    negocio.agregar(marca);
                }
                else
                {
                    Marca seleccionada = (Marca)Session["marcaSeleccionada"];
                    marca.Id = seleccionada.Id;
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

       

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["marcaSeleccionada"] != null)
                {
                    Marca seleccionada = (Marca)Session["marcaSeleccionada"];
                    MarcaNegocio negocio = new MarcaNegocio();

                    // si estaba activa la inactiva, si no la reactiva
                    negocio.eliminarLogico(seleccionada.Id, !seleccionada.Activo);

                    cargarTabla();
                    limpiarFormulario();
                }
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
            Session.Remove("marcaSeleccionada");
            
            txtId.Text = string.Empty;
            txtDescripcion.Text = string.Empty;

            rbActivoSI.Checked = true;
            rbActivoNO.Checked = false;

            dgvMarcas.SelectedIndex = -1;
            btnInactivar.Visible = false;
            divRbActivoSi.Visible = true;
            divRbActivoNo.Visible = false;
        }

    }
}