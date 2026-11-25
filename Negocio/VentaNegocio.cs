using Datos;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public  class VentaNegocio
    {
        public string obtenerProximoNumeroFactura()
        {
            ConexionDatos datos = new ConexionDatos();
            string proximoNumero = "00000001";

            try
            {
                datos.setearConsulta("EXEC dbo.sp_ObtenerProximoNumeroFactura");
                datos.ejecutarLectura();

                if(datos.Lector.Read())
                {
                    if (!(datos.Lector["ProximoNumero"] is DBNull))
                    {
                        proximoNumero = datos.Lector["ProximoNumero"].ToString();
                    }
                }

                return proximoNumero;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        
        }

        public int agregar(Venta nueva)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearProcedimiento("dbo.sp_InsertarVenta");
                datos.setearParametro("@NumeroFactura", nueva.NumeroFactura);
                datos.setearParametro("@Fecha", nueva.Fecha);
                datos.setearParametro("@IdCliente", nueva.ClienteAsociado.Id);
                datos.setearParametro("@TotalFinal", nueva.TotalFinal);

                int idVentaGenerada = datos.ejecutarAccionScalar();
                return idVentaGenerada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarLinea(int idVenta, int idProducto, int cantidad, decimal precioUnitario)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearProcedimiento("dbo.sp_AgregarLineaVenta");
                datos.setearParametro("@IdVenta", idVenta);
                datos.setearParametro("@IdProducto", idProducto);
                datos.setearParametro("@Cantidad", cantidad);
                datos.setearParametro("@PrecioUnitario", precioUnitario);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int contarVentasHoy()
        {
            ConexionDatos datos = new ConexionDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM VENTAS WHERE CAST(Fecha AS DATE) = CAST(GETDATE() AS DATE)");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector[0];

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
