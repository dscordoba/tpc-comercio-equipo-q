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
    public partial class Categorias : System.Web.UI.Page
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
                CategoriaNegocio negocio = new CategoriaNegocio();
                dgvCategorias.DataSource = negocio.listar();
                dgvCategorias.DataBind();
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
                Categoria nuevo = new Categoria();
                CategoriaNegocio negocio = new CategoriaNegocio();

                nuevo.Descripcion = txtDescripcion.Text;
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
                if (dgvCategorias.SelectedDataKey != null)
                {
                    int id = int.Parse(dgvCategorias.SelectedDataKey.Value.ToString());

                    CategoriaNegocio negocio = new CategoriaNegocio();
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
            dgvCategorias.SelectedIndex = -1;
        }
    }
}