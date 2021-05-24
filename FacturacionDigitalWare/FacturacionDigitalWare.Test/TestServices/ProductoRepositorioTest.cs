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
    public class ProductoRepositorioTest
    {
        private readonly ProductoRepositorio _productoRepositorio;

        public ProductoRepositorioTest()
        {
            string cadenaConexion = "Server=localhost;Database=DBFACTURACION_DIGITAL_WARE;User Id=sa;Password=sa2019%;MultipleActiveResultSets=true";
            var services = new ServiceCollection();
            services.AddDbContext<DBFACTURACION_DIGITAL_WAREContext>(options => options.UseSqlServer(cadenaConexion));
            services.AddScoped<ProductoRepositorio, ProductoRepositorio>();
            var serviceProvider = services.BuildServiceProvider();
            _productoRepositorio = serviceProvider.GetService<ProductoRepositorio>();
        }


        [Fact]
        public async Task ListadoProductosTest()
        {
            var listadoProductos= await _productoRepositorio.ListadoProductos();
            listadoProductos.Count.ShouldBe(5);
            listadoProductos[0].Nombre.ShouldBe("computador");
            listadoProductos[0].Descripcion.ShouldBe("lenovo idepad 320");
            listadoProductos[0].Precio.ShouldBe(1700000);
            listadoProductos[0].FechaCreacion.ShouldBe("22/05/2021 09:35 p. m.");
            listadoProductos[0].Activo.ShouldBe(true);

            listadoProductos[1].Nombre.ShouldBe("Cámara");
            listadoProductos[1].Descripcion.ShouldBe("Camara profesional ");
            listadoProductos[1].Precio.ShouldBe(2500000);
            listadoProductos[1].FechaCreacion.ShouldBe("22/05/2021 09:41 p. m.");
            listadoProductos[1].Activo.ShouldBe(true);
        }

        [Fact]
        public async Task InformacionProductoTest()
        {
            var idProducto= Guid.Parse("14CBC960-0217-47CB-AB8E-4833BB5B75F0");
            var informacionProducto= await _productoRepositorio.InformacionProducto(idProducto);
            informacionProducto.Nombre.ShouldBe("computador");
            informacionProducto.Descripcion.ShouldBe("lenovo idepad 320");
            informacionProducto.Precio.ShouldBe(1700000);
            informacionProducto.Activo.ShouldBe(true);
        }



    }
}
