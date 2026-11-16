using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Venta
    {
        public int Id {  get; set; }
        public string NumeroFactura {  get; set; }
        public DateTime Fecha {  get; set; }
        
        public Cliente ClienteAsociado {  get; set; }
        public List<DetalleVenta> LineasVenta { get; set; } 
        public decimal TotalFinal {  get; set; }
    }
}
