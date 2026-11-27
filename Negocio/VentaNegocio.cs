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
                datos.setearParametro("@IdUsuario", nueva.UsuarioAsociado.Id); 
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

        public List<Venta> listarVentas()
        {
            List<Venta> lista = new List<Venta>();
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta(@"SELECT V.Id, V.NumeroFactura, V.Fecha, V.TotalFinal,
                                              C.Id AS IdCliente, C.Nombre AS ClienteNombre, C.Apellido AS ClienteApellido, C.Dni AS ClienteDni,
                                              U.Id AS IdUsuario, U.Nombre AS VendedorNombre, U.Apellido AS VendedorApellido
                                       FROM VENTAS V
                                       INNER JOIN CLIENTES C ON V.IdCliente = C.Id
                                       INNER JOIN USUARIOS U ON V.IdUsuario = U.Id
                                       ORDER BY V.Fecha DESC, V.Id DESC");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta aux = new Venta();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.NumeroFactura = datos.Lector["NumeroFactura"].ToString();
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.TotalFinal = (decimal)datos.Lector["TotalFinal"];

                    aux.ClienteAsociado = new Cliente();
                    aux.ClienteAsociado.Id = (int)datos.Lector["IdCliente"];
                    aux.ClienteAsociado.Nombre = datos.Lector["ClienteNombre"].ToString();
                    aux.ClienteAsociado.Apellido = datos.Lector["ClienteApellido"].ToString();
                    aux.ClienteAsociado.Dni = datos.Lector["ClienteDni"].ToString();

                    aux.UsuarioAsociado = new Usuario();
                    aux.UsuarioAsociado.Id = (int)datos.Lector["IdUsuario"];
                    aux.UsuarioAsociado.Nombre = datos.Lector["VendedorNombre"].ToString();
                    aux.UsuarioAsociado.Apellido = datos.Lector["VendedorApellido"].ToString();

                    lista.Add(aux);
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

        public List<DetalleVenta> listarDetalleVenta(int idVenta)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta(@"SELECT DV.Id, DV.Cantidad, DV.PrecioUnitario,
                                              P.Id AS IdProducto, P.CodigoProducto, P.Nombre AS NombreProducto
                                       FROM DETALLE_VENTAS DV
                                       INNER JOIN PRODUCTOS P ON DV.IdProducto = P.Id
                                       WHERE DV.IdVenta = @IdVenta");
                datos.setearParametro("@IdVenta", idVenta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta det = new DetalleVenta();
                    det.Id = (int)datos.Lector["Id"];
                    det.Cantidad = (int)datos.Lector["Cantidad"];
                    det.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];

                    det.ProductoAsociado = new Producto();
                    det.ProductoAsociado.Id = (int)datos.Lector["IdProducto"];
                    det.ProductoAsociado.CodigoProducto = datos.Lector["CodigoProducto"].ToString();
                    det.ProductoAsociado.Nombre = datos.Lector["NombreProducto"].ToString();

                    lista.Add(det);
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

        public List<Usuario> listarVendedores()
        {
            List<Usuario> lista = new List<Usuario>();
            ConexionDatos datos = new ConexionDatos();

            try
            {
                // Trae Admins y Vendedores activos o que tengan ventas
                datos.setearConsulta("SELECT Id, Nombre, Apellido FROM USUARIOS WHERE Perfil IN (1, 2) ORDER BY Apellido, Nombre");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario u = new Usuario();
                    u.Id = (int)datos.Lector["Id"];
                    u.Nombre = datos.Lector["Nombre"].ToString();
                    u.Apellido = datos.Lector["Apellido"].ToString();
                    lista.Add(u);
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

    }
}
