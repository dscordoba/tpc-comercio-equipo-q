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


        public List<Compra> listar(int idProveedor, DateTime desde, DateTime hasta)
        {
            List<Compra> lista = new List<Compra>();
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("SELECT C.Id, C.Fecha, C.NumeroFactura, C.IdProveedor, P.RazonSocial, D.IdProducto, PR.Nombre, D.Cantidad, D.PrecioCompra " +
                    "FROM COMPRAS C " +
                    "INNER JOIN PROVEEDORES P ON P.Id = C.IdProveedor " +
                    "INNER JOIN DETALLE_COMPRAS D ON D.IdCompra = C.Id " +
                    "INNER JOIN PRODUCTOS PR ON PR.Id = D.IdProducto " +
                    "WHERE (@idProveedor = 0 OR C.IdProveedor = @idProveedor) " +
                    "AND C.Fecha >= @desde AND C.Fecha < DATEADD(DAY, 1, @hasta) " +
                    "ORDER BY C.Fecha DESC, C.Id DESC, D.Id");
                datos.setearParametro("@idProveedor", idProveedor);
                datos.setearParametro("@desde", desde);
                datos.setearParametro("@hasta", hasta);
                datos.ejecutarLectura();

                int ultimoId = 0;
                Compra compra = null;

                while (datos.Lector.Read())
                {
                    int idCompra = (int)datos.Lector["Id"];

                    // si cambió la compra, creo una nueva
                    if (idCompra != ultimoId)
                    {
                        compra = new Compra();
                        compra.Id = idCompra;
                        compra.Fecha = (DateTime)datos.Lector["Fecha"];
                        compra.NumeroFactura = (string)datos.Lector["NumeroFactura"];
                        compra.ProveedorAsociado = new Proveedor();
                        compra.ProveedorAsociado.Id = (int)datos.Lector["IdProveedor"];
                        compra.ProveedorAsociado.RazonSocial = (string)datos.Lector["RazonSocial"];
                        lista.Add(compra);
                        ultimoId = idCompra;
                    }

                    DetalleCompra linea = new DetalleCompra();
                    linea.ProductoAsociado = new Producto();
                    linea.ProductoAsociado.Id = (int)datos.Lector["IdProducto"];
                    linea.ProductoAsociado.Nombre = (string)datos.Lector["Nombre"];
                    linea.Cantidad = (int)datos.Lector["Cantidad"];
                    linea.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    compra.LineasCompra.Add(linea);
                }

                return lista;
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
