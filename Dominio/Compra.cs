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
        public Compra()
        {
            LineasCompra = new List<DetalleCompra>();
        }
    }
}
