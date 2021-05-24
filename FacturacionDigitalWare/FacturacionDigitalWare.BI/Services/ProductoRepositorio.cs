using FacturacionDigitalWare.BI.DTORequest.Producto;
using FacturacionDigitalWare.BI.DTOResponse.Producto;
using FacturacionDigitalWare.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionDigitalWare.BI.Services
{
    public class ProductoRepositorio
    {
        private readonly DBFACTURACION_DIGITAL_WAREContext _dbContext;

        /// <summary>
        /// Constructor del repositorio, inicializa las variables de la clase
        /// </summary>
        /// <param name="dbContext"></param>
        public ProductoRepositorio(DBFACTURACION_DIGITAL_WAREContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// consulta el listado de los productos que están en la base de datos
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductoDetalleResponse>> ListadoProductos()
        {
            try
            {
                var listadoDetalles = await _dbContext.Productos
                                        .Select(p => new ProductoDetalleResponse
                                        {
                                            Id = p.ProIdProducto,
                                            Nombre = p.ProNombre,
                                            Descripcion = p.ProDescripcion,
                                            Precio = p.ProPrecio,
                                            FechaCreacion = p.ProFechaCreacion.ToString("dd/MM/yyyy hh:mm tt"),
                                            Activo = p.ProActivo
                                        })
                                        .ToListAsync();
                return listadoDetalles;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }

        /// <summary>
        /// inserta un nuevo producto en la base de datos
        /// </summary>
        /// <param name="crearProducto"></param>
        public async Task CrearProducto(CrearEditarProductoRequest crearProducto)
        {
            try
            {
                var productoNuevo = new Producto
                {
                    ProIdProducto = Guid.NewGuid(),
                    ProNombre = crearProducto.Nombre,
                    ProDescripcion = crearProducto.Descripcion,
                    ProPrecio = crearProducto.Precio,
                    ProFechaCreacion = DateTime.Now,
                    ProActivo = true
                };

                await _dbContext.AddAsync(productoNuevo);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        //public bool ConsultarProducto(Guid idProducto)
        //{
        //    try
        //    {
        //        return _dbContext.Producto.Find(idProducto) != null;
        //    }
        //    catch (Exception err)
        //    {
        //        throw new Exception(err.ToString());
        //    }
        //}

        /// <summary>
        /// Consulta la información de un producto en específico para llenarla en los campos al momento de editarlo
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns></returns>
        public async Task<InformacionProductoResponse> InformacionProducto(Guid idProducto)
        {
            try
            {
                var producto = await _dbContext.Productos.FindAsync(idProducto);

                var informacionProducto = new InformacionProductoResponse
                {
                    Nombre = producto.ProNombre,
                    Descripcion = producto.ProDescripcion,
                    Precio = producto.ProPrecio,
                    Activo = producto.ProActivo
                };

                return informacionProducto;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }

        /// <summary>
        /// Actualiza la información del producto en la base de datos
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="actualizarProducto"></param>
        public async Task EditarProducto(Guid idProducto, CrearEditarProductoRequest actualizarProducto)
        {
            try
            {
                var producto = await _dbContext.Productos.FindAsync(idProducto);
                producto.ProNombre = actualizarProducto.Nombre;
                producto.ProDescripcion = actualizarProducto.Descripcion;
                producto.ProPrecio = actualizarProducto.Precio;
                producto.ProActivo = actualizarProducto.Activo;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }

        /// <summary>
        /// Elimina el producto del id informado de la base de datos
        /// </summary>
        /// <param name="idProducto"></param>
        public async Task EliminarProducto(Guid idProducto)
        {
            try
            {
                var producto = await _dbContext.Productos
                                    .Include(p => p.Inventarios)
                                    .Include(p => p.FacturaProductos)
                                    .FirstOrDefaultAsync(p => p.ProIdProducto == idProducto);

                _dbContext.Remove(producto);
                _dbContext.RemoveRange(producto.Inventarios);
                _dbContext.RemoveRange(producto.FacturaProductos);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }
    }
}
