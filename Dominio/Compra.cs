using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Compra
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string NumeroFactura { get; set; }
        public Proveedor ProveedorAsociado { get; set; }
        public List<DetalleCompra> LineasCompra { get; set; }

        public int CantidadProductos
        {
            get { return LineasCompra.Count; }
        }

        public decimal Total
        {
            get
            {
                decimal total = 0;
                foreach (DetalleCompra linea in LineasCompra)
                {
                    total += linea.Subtotal;
                }
                return total;
            }
        }
        public Compra()
        {
            LineasCompra = new List<DetalleCompra>();
        }
    }
}
