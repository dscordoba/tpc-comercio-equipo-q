using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Compra
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

       

         public Proveedor ProveedorAsociado { get; set; }
         public List<DetalleCompra> LineasCompra { get; set; }

         public Compra()
         {
             LineasCompra = new List<DetalleCompra>();
         }
    }
}
