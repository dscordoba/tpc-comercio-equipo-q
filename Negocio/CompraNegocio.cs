using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Dominio;

namespace Negocio
{
    public class CompraNegocio
    {
        public int agregar(Compra nueva)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearProcedimiento("dbo.sp_InsertarCompra");
                datos.setearParametro("@NumeroFactura", nueva.NumeroFactura);
                datos.setearParametro("@Fecha", nueva.Fecha);
                datos.setearParametro("@IdProveedor", nueva.ProveedorAsociado.Id);

                return datos.ejecutarAccionScalar();
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

        public void agregarLinea(int idCompra, int idProveedor, int idProducto, int cantidad, decimal precioCompra)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearProcedimiento("dbo.sp_AgregarLineaCompra");
                datos.setearParametro("@IdCompra", idCompra);
                datos.setearParametro("@IdProducto", idProducto);
                datos.setearParametro("@IdProveedor", idProveedor);
                datos.setearParametro("@Cantidad", cantidad);
                datos.setearParametro("@PrecioCompra", precioCompra);

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

        public bool ExisteFactura(int idProveedor, string numeroFactura)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(1) FROM COMPRAS WHERE IdProveedor = @idProveedor AND NumeroFactura = @numeroFactura");
                datos.setearParametro("@idProveedor", idProveedor);
                datos.setearParametro("@numeroFactura", numeroFactura);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector[0]) > 0;
                }
                return false;
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

        public int contarComprasHoy()
        {
            ConexionDatos datos = new ConexionDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM COMPRAS WHERE CAST(Fecha AS DATE) = CAST(GETDATE() AS DATE)");
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
