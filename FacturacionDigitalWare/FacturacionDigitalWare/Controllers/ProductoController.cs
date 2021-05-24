using FacturacionDigitalWare.BI.DTORequest.Producto;
using FacturacionDigitalWare.BI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionDigitalWare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoRepositorio _productoRepositorio;
        private readonly ILogger<ProductoController> _logger;

        /// <summary>
        /// Constructor del controlador, inicializa las variables de la clase
        /// </summary>
        /// <param name="productoRepositorio"></param>
        /// <param name="logger"></param>
        public ProductoController(ProductoRepositorio productoRepositorio, ILogger<ProductoController> logger)
        {
            _productoRepositorio = productoRepositorio;
            _logger = logger;
        }

        /// <summary>
        /// Consulta el listado de los productos para mostrarlos en la tabla, carga la información
        /// </summary>
        /// <returns></returns>
        [HttpGet("[Action]")]
        public async Task<IActionResult> ListadoProductos()
        {
            try
            {
                var listadoProductos = await _productoRepositorio.ListadoProductos();
                return Ok(listadoProductos);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar los productos""");
            }
        }

        /// <summary>
        /// Crea el producto en la DB con la información enviada por el cliente
        /// </summary>
        /// <param name="crearProducto"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CrearProducto([FromBody] CrearEditarProductoRequest crearProducto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productoRepositorio.CrearProducto(crearProducto);
                    return StatusCode(StatusCodes.Status201Created, @"""Se ha creado el producto correctamente""");
                }
                else
                {
                    var listadoErrores = ModelState
                                        .Where(y => y.Value.ValidationState == ModelValidationState.Invalid)
                                        .Select(y => new
                                        {
                                            Campo = new string(y.Key?.ToArray()),
                                            Error = y.Value?.Errors?.FirstOrDefault()?.ErrorMessage
                                        })
                                        .ToList();

                    return StatusCode(StatusCodes.Status400BadRequest, $@"""Por favor valide la información enviada: {JsonConvert.SerializeObject(listadoErrores)}""");
                }
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al crear el producto""");
            }
        }

        /// <summary>
        /// Consulta la información del producto para cuando se va a editar en la pantalla
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns></returns>
        [HttpGet("[Action]/{idProducto}")]
        public async Task<IActionResult> InformacionProducto(Guid idProducto)
        {
            try
            {
                var informacionProducto = await _productoRepositorio.InformacionProducto(idProducto);
                return Ok(informacionProducto);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar la información del producto""");
            }
        }

        /// <summary>
        /// Edita la información del producto que envíe el cliente
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="actualizarProducto"></param>
        /// <returns></returns>
        [HttpPut("[Action]/{idProducto}")]
        public async Task<IActionResult> EditarProducto(Guid idProducto, [FromBody] CrearEditarProductoRequest actualizarProducto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productoRepositorio.EditarProducto(idProducto, actualizarProducto);
                    return StatusCode(StatusCodes.Status200OK, @"""Se ha actualizado correctamente la información del producto""");
                }
                else
                {
                    var listadoErrores = ModelState
                                        .Where(y => y.Value.ValidationState == ModelValidationState.Invalid)
                                        .Select(y => new
                                        {
                                            Campo = new string(y.Key?.ToArray()),
                                            Error = y.Value?.Errors?.FirstOrDefault()?.ErrorMessage
                                        })
                                        .ToList();

                    return StatusCode(StatusCodes.Status400BadRequest, $@"""Por favor valide la información enviada: {JsonConvert.SerializeObject(listadoErrores)}""");
                }
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al actualizar el producto""");
            }
        }

        /// <summary>
        /// Elimina el producto de la base de datos con el id informado
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns></returns>
        [HttpDelete("[Action]/{idProducto}")]
        public async Task<IActionResult> EliminarProducto(Guid idProducto)
        {
            try
            {
                await _productoRepositorio.EliminarProducto(idProducto);
                return Ok(@"""Se ha eliminado el producto correctamente""");
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al eliminar el producto""");
            }
        }
    }
}
