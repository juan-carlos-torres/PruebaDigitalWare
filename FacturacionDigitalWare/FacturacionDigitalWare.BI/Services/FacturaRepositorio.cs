using FacturacionDigitalWare.BI.DTORequest.Factura;
using FacturacionDigitalWare.BI.DTOResponse.Factura;
using FacturacionDigitalWare.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionDigitalWare.BI.Services
{
    public class FacturaRepositorio
    {
        private readonly DBFACTURACION_DIGITAL_WAREContext _dbContext;

        /// <summary>
        /// Constructor del repositorio, inicializa las variables de la clase
        /// </summary>
        /// <param name="dbContext"></param>
        public FacturaRepositorio(DBFACTURACION_DIGITAL_WAREContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Consulta el listado de las facturas que hay en la base de datos
        /// </summary>
        /// <returns></returns>
        public async Task<List<FacturaDetalleResponse>> ListadoFacturas()
        {
            try
            {
                var listadoFacturas = await _dbContext.Facturas
                                            .Select(f => new FacturaDetalleResponse
                                            {
                                                NombreCliente = $"{f.FacIdClienteNavigation.CliNombres} {f.FacIdClienteNavigation.CliApellidos}",
                                                Fecha = f.FacFechaRegistro.ToString("dd/MM/yyyy hh:mm tt"),
                                                ValorTotal = f.FacValorTotal,
                                                ListadoProductos = f.FacturaProductos
                                                                    .Select(p => new FacturaDetalleResponse.Producto
                                                                    {
                                                                        Nombre = p.FprIdProductoNavigation.ProNombre,
                                                                        Cantidad = p.FprCantidad,
                                                                        Valor = p.FprValor
                                                                    })
                                                                    .ToList()
                                            })
                                            .ToListAsync();

                return listadoFacturas;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Crear una nueva factura con la información que envía el usuario
        /// </summary>
        /// <param name="crearFactura"></param>
        /// <returns></returns>
        public async Task CrearFactura(CrearFacturaRequest crearFactura)
        {
            try
            {
                List<FacturaProducto> listadoProductosFactura = ProductosFactura(crearFactura);
                decimal valorTotal = CalcularValorTotalFactura(listadoProductosFactura);

                var factura = new Factura
                {
                    FacIdFactura = Guid.NewGuid(),
                    FacIdCliente = crearFactura.IdCliente,
                    FacFechaRegistro = DateTime.Now,
                    FacValorTotal = valorTotal,
                    FacturaProductos = listadoProductosFactura
                };

                await _dbContext.AddAsync(factura);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }

        /// <summary>
        /// Genera  el listado de los productos para guardarlos en la base de datos
        /// </summary>
        /// <param name="crearFactura"></param>
        /// <returns></returns>
        public List<FacturaProducto> ProductosFactura(CrearFacturaRequest crearFactura)
        {
            try
            {
                var listadoProductosFactura = crearFactura.ListadoProductos
                                .Select(p => new FacturaProducto
                                {
                                    FprIdProducto = p.Id,
                                    FprCantidad = p.Cantidad,
                                    FprValor = p.Valor
                                })
                                .ToList();
                return listadoProductosFactura;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Calcula el valor total que va a tener la factura con respecto a los productos
        /// </summary>
        /// <param name="listadoProductosFactura"></param>
        /// <returns></returns>
        public decimal CalcularValorTotalFactura(List<FacturaProducto> listadoProductosFactura)
        {
            try
            {
                return listadoProductosFactura.Sum(p => p.FprValor * p.FprCantidad);
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Obtiene la información de la factura con sus productos
        /// </summary>
        /// <param name="idFactura"></param>
        /// <returns></returns>
        public async Task<FacturaDetalleResponse> InformacionFactura(Guid idFactura)
        {
            try
            {
                var factura = await _dbContext.Facturas
                                    .Include(f => f.FacIdClienteNavigation)
                                    .Include(f => f.FacturaProductos)
                                        .ThenInclude(f => f.FprIdProductoNavigation)
                                    .FirstOrDefaultAsync(f => f.FacIdFactura == idFactura);

                var informacionFactura = new FacturaDetalleResponse
                {
                    NombreCliente = $"{factura.FacIdClienteNavigation.CliNombres} {factura.FacIdClienteNavigation.CliApellidos}",
                    Fecha = factura.FacFechaRegistro.ToString("dd/MM/yyyy hh:mm tt"),
                    ValorTotal = factura.FacValorTotal,
                    ListadoProductos = factura.FacturaProductos
                                        .Select(p => new FacturaDetalleResponse.Producto
                                        {
                                            Nombre = p.FprIdProductoNavigation.ProNombre,
                                            Cantidad = p.FprCantidad,
                                            Valor = p.FprValor
                                        })
                                        .ToList()
                };

                return informacionFactura;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Elimina la factura junto con sus productos
        /// </summary>
        /// <param name="idFactura"></param>
        /// <returns></returns>
        public async Task EliminarFactura(Guid idFactura)
        {
            try
            {
                var factura = await _dbContext.Facturas
                                .Include(f => f.FacturaProductos)
                                .FirstOrDefaultAsync(f => f.FacIdFactura == idFactura);

                _dbContext.RemoveRange(factura.FacturaProductos);
                _dbContext.Remove(factura);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }

    }
}
