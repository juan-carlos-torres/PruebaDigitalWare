using FacturacionDigitalWare.BI.DTORequest.Factura;
using FacturacionDigitalWare.BI.Services;
using FacturacionDigitalWare.DAL.Models;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FacturacionDigitalWare.Test.TestServices
{
    public class FacturaRepositorioTest
    {
        private readonly FacturaRepositorio _facturaRepositorio;

        public FacturaRepositorioTest()
        {
            string cadenaConexion = "Server=localhost;Database=DBFACTURACION_DIGITAL_WARE;User Id=sa;Password=sa2019%;MultipleActiveResultSets=true";
            var services = new ServiceCollection();
            services.AddDbContext<DBFACTURACION_DIGITAL_WAREContext>(options => options.UseSqlServer(cadenaConexion));
            services.AddScoped<FacturaRepositorio, FacturaRepositorio>();
            var serviceProvider = services.BuildServiceProvider();
            _facturaRepositorio = serviceProvider.GetService<FacturaRepositorio>();
        }


        [Fact]
        public async Task ListadoFacturasTest()
        {
            var listadoFacturas = await _facturaRepositorio.ListadoFacturas();
            listadoFacturas.Count.ShouldBe(5);
            listadoFacturas[0].NombreCliente.ShouldBe("Juan Carlos Torres Torres");
            listadoFacturas[0].Fecha.ShouldBe("12/03/2001 12:00 a. m.");
            listadoFacturas[0].ValorTotal.ShouldBe(10000000);

            listadoFacturas[1].NombreCliente.ShouldBe("Juan Carlos Torres Torres");
            listadoFacturas[1].Fecha.ShouldBe("20/04/2000 12:00 a. m.");
            listadoFacturas[1].ValorTotal.ShouldBe(2000000);
        }

        [Fact]
        public async Task InformacionFacturaTest()
        {
            var idFactura = Guid.Parse("DBB67712-8F29-4DCA-9E7E-22F0CB2C5EA8");
            var informacionFactura = await _facturaRepositorio.InformacionFactura(idFactura);
            informacionFactura.ValorTotal.ShouldBe(10000000);
            informacionFactura.NombreCliente.ShouldBe("Juan Carlos Torres Torres");
            informacionFactura.Fecha.ShouldBe("12/03/2001 12:00 a. m.");
        }


        [Fact]
        public void ProductosFacturaTest()
        {
            CrearFacturaRequest crearFactura = new CrearFacturaRequest
            {
                ListadoProductos =  new List<CrearFacturaRequest.Producto>
                {
                    new CrearFacturaRequest.Producto
                    {
                        Cantidad = 5,
                        Valor = 1000
                    },
                    new CrearFacturaRequest.Producto
                    {
                        Cantidad = 2,
                        Valor = 2000
                    }
                }
            };

            var listadoProductos = _facturaRepositorio.ProductosFactura(crearFactura);
            listadoProductos.Count.ShouldBe(2);
            listadoProductos[0].FprValor.ShouldBe(1000);
            listadoProductos[0].FprCantidad.ShouldBe(5);

            listadoProductos[1].FprValor.ShouldBe(2000);
            listadoProductos[1].FprCantidad.ShouldBe(2);
        }



        [Fact]
        public void CalcularValorTotalFacturaTest()
        {
            List<FacturaProducto> listadoProductosFactura = new List<FacturaProducto>
            {
                new FacturaProducto
                {
                    FprValor = 1000,
                    FprCantidad = 5
                },
                new FacturaProducto
                {
                    FprValor = 2000,
                    FprCantidad = 2
                }
            };

            var valorTotal = _facturaRepositorio.CalcularValorTotalFactura(listadoProductosFactura);

            valorTotal.ShouldBe(9000);
        }

    }
}
