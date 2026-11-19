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
    }
}
