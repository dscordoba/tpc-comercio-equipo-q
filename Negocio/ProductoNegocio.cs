using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {
        /* Listar Productos con SP */
        public List<Producto> listarConSP()
        {
            List<Producto> lista = new List<Producto>();
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearProcedimiento("listarProductos");
                datos.ejecutarLectura();

                // Recorro y leo 'datos' 
                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoProducto = (string)datos.Lector["CodigoProducto"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    // Contempla si el dato viene NULL
                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.StockActual = (int)datos.Lector["StockActual"];
                    aux.StockMinimo = (int)datos.Lector["StockMinimo"];
                    aux.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    aux.PrecioCompraActual = (decimal)datos.Lector["PrecioCompraActual"];
                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    aux.MarcaAsociada = new Marca
                    {
                        Id = (int)datos.Lector["IdMarca"],
                        Descripcion = (string)datos.Lector["MarcaDescripcion"]
                    };

                    aux.CategoriaAsociada = new Categoria
                    {
                        Id = (int)datos.Lector["IdCategoria"],
                        Descripcion = (string)datos.Lector["CategoriaDescripcion"]
                    };

                    lista.Add(aux);
                }

                datos.cerrarConexion();

                // Recorro la 'lista' para buscar los proveedores de cada producto
                foreach (Producto item in lista)
                {
                    ConexionDatos datosProv = new ConexionDatos();
                    try
                    {
                        datosProv.setearProcedimiento("listarProveedoresPorProducto");
                        datosProv.setearParametro("@IdProducto", item.Id);
                        datosProv.ejecutarLectura();

                        // Recorro y leo 'datos' 
                        while (datosProv.Lector.Read())
                        {
                            Proveedor prov = new Proveedor();
                            prov.Id = (int)datosProv.Lector["Id"];
                            prov.RazonSocial = (string)datosProv.Lector["RazonSocial"];
                            prov.Cuit = (string)datosProv.Lector["Cuit"];

                            // Contempla si el dato viene NULL
                            if (!(datosProv.Lector["Email"] is DBNull))
                                prov.Email = (string)datosProv.Lector["Email"];

                            if (!(datosProv.Lector["Telefono"] is DBNull))
                                prov.Telefono = (string)datosProv.Lector["Telefono"];

                            prov.Activo = bool.Parse(datosProv.Lector["Activo"].ToString());

                            item.ProveedoresAsociados.Add(prov);
                        }
                    }
                    finally
                    {
                        datosProv.cerrarConexion();
                    }
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

        /* Eliminación lógica (Inactivar/Reactivar) */
        public void eliminarLogico(int id, bool activo = false)
        {
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("UPDATE PRODUCTOS SET Activo = @activo WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@activo", activo);

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

        // Verifica si existe el Código de Producto en BD 
        public bool ExisteCodigoProducto(string codigo, int idExcluir = 0)
        {
            ConexionDatos datos = new ConexionDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM PRODUCTOS WHERE CodigoProducto = @Codigo AND Id != @Id");
                datos.setearParametro("@Codigo", codigo);
                datos.setearParametro("@Id", idExcluir);

                datos.ejecutarLectura();
                if (datos.Lector.Read())
                    return Convert.ToInt32(datos.Lector[0]) > 0;

                return false;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        /* Agregar BD */
        public void agregar(Producto nuevo)
        {
            if (ExisteCodigoProducto(nuevo.CodigoProducto))
                throw new Exception("Ya existe un producto registrado con ese código de artículo.");

            ConexionDatos datos = new ConexionDatos();

            try
            {
                // Inserta el producto y recupera el Id generado con SCOPE_IDENTITY()
                datos.setearConsulta(
                    "INSERT INTO PRODUCTOS (CodigoProducto, Nombre, Descripcion, PorcentajeGanancia, StockMinimo, IdMarca, IdCategoria) " +
                    "VALUES (@codigo, @nombre, @descripcion, @porcentajeGanancia, @stockMinimo, @idMarca, @idCategoria); " +
                    "SELECT SCOPE_IDENTITY();"
                );

                datos.setearParametro("@codigo", nuevo.CodigoProducto);
                datos.setearParametro("@nombre", nuevo.Nombre);

                // Manejo de NULL si la descripción viene vacía
                if (string.IsNullOrWhiteSpace(nuevo.Descripcion))
                    datos.setearParametro("@descripcion", DBNull.Value);
                else
                    datos.setearParametro("@descripcion", nuevo.Descripcion);

                datos.setearParametro("@porcentajeGanancia", nuevo.PorcentajeGanancia);
                datos.setearParametro("@stockMinimo", nuevo.StockMinimo);
                datos.setearParametro("@idMarca", nuevo.MarcaAsociada.Id);
                datos.setearParametro("@idCategoria", nuevo.CategoriaAsociada.Id);

                // Ejecuta y obtiene el ID recién creado
                int idProductoGenerado = datos.ejecutarAccionScalar();
                datos.cerrarConexion();

                // Guarda las relaciones en la tabla PRODUCTOS_PROVEEDORES
                if (nuevo.ProveedoresAsociados != null && nuevo.ProveedoresAsociados.Count > 0)
                {
                    foreach (Proveedor prov in nuevo.ProveedoresAsociados)
                    {
                        ConexionDatos datosProv = new ConexionDatos();
                        try
                        {
                            datosProv.setearConsulta("INSERT INTO PRODUCTOS_PROVEEDORES (IdProducto, IdProveedor) VALUES (@idProducto, @idProveedor)");
                            datosProv.setearParametro("@idProducto", idProductoGenerado);
                            datosProv.setearParametro("@idProveedor", prov.Id);
                            datosProv.ejecutarAccion();
                        }
                        finally
                        {
                            datosProv.cerrarConexion();
                        }
                    }
                }
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

        /* Modificar BD */
        public void modificar(Producto producto)
        {
            if (ExisteCodigoProducto(producto.CodigoProducto, producto.Id))
                throw new Exception("Ya existe otro producto registrado con ese código de artículo.");

            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE PRODUCTOS " +
                    "SET CodigoProducto = @codigo, Nombre = @nombre, Descripcion = @descripcion, " +
                    "PorcentajeGanancia = @porcentajeGanancia, StockMinimo = @stockMinimo, " +
                    "IdMarca = @idMarca, IdCategoria = @idCategoria " +
                    "WHERE Id = @id"
                );

                datos.setearParametro("@id", producto.Id);
                datos.setearParametro("@codigo", producto.CodigoProducto);
                datos.setearParametro("@nombre", producto.Nombre);

                if (string.IsNullOrWhiteSpace(producto.Descripcion))
                    datos.setearParametro("@descripcion", DBNull.Value);
                else
                    datos.setearParametro("@descripcion", producto.Descripcion);

                datos.setearParametro("@porcentajeGanancia", producto.PorcentajeGanancia);
                datos.setearParametro("@stockMinimo", producto.StockMinimo);
                datos.setearParametro("@idMarca", producto.MarcaAsociada.Id);
                datos.setearParametro("@idCategoria", producto.CategoriaAsociada.Id);

                datos.ejecutarAccion();
                datos.cerrarConexion();

                // Elimina las asociaciones anteriores de proveedores
                ConexionDatos datosDelete = new ConexionDatos();
                try
                {
                    datosDelete.setearConsulta("DELETE FROM PRODUCTOS_PROVEEDORES WHERE IdProducto = @idProducto");
                    datosDelete.setearParametro("@idProducto", producto.Id);
                    datosDelete.ejecutarAccion();
                }
                finally
                {
                    datosDelete.cerrarConexion();
                }

                // Inserta los nuevos proveedores seleccionados
                if (producto.ProveedoresAsociados != null && producto.ProveedoresAsociados.Count > 0)
                {
                    foreach (Proveedor prov in producto.ProveedoresAsociados)
                    {
                        ConexionDatos datosProv = new ConexionDatos();
                        try
                        {
                            datosProv.setearConsulta("INSERT INTO PRODUCTOS_PROVEEDORES (IdProducto, IdProveedor) VALUES (@idProducto, @idProveedor)");
                            datosProv.setearParametro("@idProducto", producto.Id);
                            datosProv.setearParametro("@idProveedor", prov.Id);
                            datosProv.ejecutarAccion();
                        }
                        finally
                        {
                            datosProv.cerrarConexion();
                        }
                    }
                }
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

        public List<Producto> listarStockBajo()
        {
            List<Producto> lista = new List<Producto>();
            ConexionDatos datos = new ConexionDatos();

            try
            {
                datos.setearConsulta("SELECT Id, CodigoProducto, Nombre, StockActual, StockMinimo FROM PRODUCTOS WHERE Activo = 1 AND StockActual <= StockMinimo ORDER BY StockActual ASC");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoProducto = datos.Lector["CodigoProducto"].ToString();
                    aux.Nombre = datos.Lector["Nombre"].ToString();
                    aux.StockActual = (int)datos.Lector["StockActual"];
                    aux.StockMinimo = (int)datos.Lector["StockMinimo"];

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
    }
}
