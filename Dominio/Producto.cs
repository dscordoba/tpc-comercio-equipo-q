using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Producto
    {

        public int Id { get; set; }
        public string CodigoArticulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public decimal PorcentajeGanancia { get; set; }
        public decimal PrecioCompraActual { get; set; }
        public Marca MarcaAsociada { get; set; }
        public bool Activo { get; set; }

        // Descomentar cuando estén Categoria y Proveedor (Marce)

        // public Categoria CategoriaAsociada { get; set; }
        // public List<Proveedor> ProveedoresAsociados { get; set; }

        // public Producto()
        // {
        //     ProveedoresAsociados = new List<Proveedor>();
        // }
    }
}
