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
    public partial class Categorias : System.Web.UI.Page
    {
        private void cargarGrilla()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            dgvCategorias.DataSource = negocio.listarConSP();
            dgvCategorias.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnInactivar.Visible = false;
                btnInactivar.Enabled = false;
                divRbActivoNo.Visible = false;
                lblError.Visible = false;
                cargarGrilla();
            }
        }

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSeleccionado = Convert.ToInt32(dgvCategorias.SelectedDataKey.Value);

            CategoriaNegocio negocio = new CategoriaNegocio();
            Dominio.Categoria seleccionado = negocio.listarConSP().Find(x => x.Id == idSeleccionado);

            if (seleccionado != null)
            {
                lblError.Visible = false;
                Session.Add("categoriaSeleccionada", seleccionado);

                txtId.Text = seleccionado.Id.ToString();
                txtDescripcion.Text = seleccionado.Descripcion;

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
            lblError.Visible = false;

            try
            {
                //valido Descripcion
                if(string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    lblError.Text = "La descripción es obligatoria.";
                    lblError.Visible = true;
                    return;
                }

                Categoria nuevo = new Categoria();
                nuevo.Descripcion = txtDescripcion.Text.Trim();

                CategoriaNegocio negocio = new CategoriaNegocio();

                if (Session["categoriaSeleccionada"] == null)
                {
                    nuevo.Activo = true;
                    negocio.agregar(nuevo);
                }
                else
                {
                    Dominio.Categoria seleccionada = (Dominio.Categoria)Session["categoriaSeleccionada"];
                    nuevo.Id = seleccionada.Id;
                    nuevo.Activo = seleccionada.Activo;
                    negocio.modificar(nuevo);
                }

                Session.Remove("categoriaSeleccionada");
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
                if (Session["categoriaSeleccionada"] != null)
                {
                    Dominio.Categoria seleccionada = (Dominio.Categoria)Session["categoriaSeleccionada"];
                    CategoriaNegocio negocio = new CategoriaNegocio();

                    negocio.eliminarLogico(seleccionada.Id, !seleccionada.Activo);

                    Session.Remove("categoriaSeleccionada");
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

        protected void limpiarFormulario()
        {
            Session.Remove("categoriaSeleccionada");

            txtId.Text = string.Empty;
            txtDescripcion.Text = string.Empty;

            rbActivoSI.Checked = true;
            rbActivoNO.Checked = false;
            rbActivoSI.Enabled = false;
            rbActivoNO.Enabled = false;

            divRbActivoSi.Visible = true;
            divRbActivoNo.Visible = false;

            dgvCategorias.SelectedIndex = -1;

            btnInactivar.Visible = false;
            btnInactivar.Enabled = false;

            lblError.Text = string.Empty;
            lblError.Visible = false;
        }
    }
}