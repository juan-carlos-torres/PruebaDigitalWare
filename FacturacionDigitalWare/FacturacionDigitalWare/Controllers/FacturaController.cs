using FacturacionDigitalWare.BI.DTORequest.Factura;
using FacturacionDigitalWare.BI.Services;
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
    public class FacturaController : ControllerBase
    {
        private readonly FacturaRepositorio _facturaRepositorio;
        private readonly ILogger<FacturaController> _logger;

        /// <summary>
        /// Constructor del controlador, inicializa las variables de la clase
        /// </summary>
        /// <param name="facturaRepositorio"></param>
        /// <param name="logger"></param>
        public FacturaController(FacturaRepositorio facturaRepositorio, ILogger<FacturaController> logger)
        {
            _facturaRepositorio = facturaRepositorio;
            _logger = logger;
        }


        /// <summary>
        /// Consulta el listado de las facturas
        /// </summary>
        /// <returns></returns>
        [HttpGet("[Action]")]
        public async Task<IActionResult> ListadoFacturas()
        {
            try
            {
                var listadoFacturas = await _facturaRepositorio.ListadoFacturas();
                return Ok(listadoFacturas);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar las facturas""");
            }
        }


        /// <summary>
        /// Crea la factura en la DB con la información enviada por el cliente
        /// </summary>
        /// <param name="crearFacturaRequest"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CrearFactura([FromBody] CrearFacturaRequest crearFacturaRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _facturaRepositorio.CrearFactura(crearFacturaRequest);
                    return StatusCode(StatusCodes.Status201Created, @"""Se ha creado la factura correctamente""");
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
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al crear la factura """);
            }
        }


        /// <summary>
        /// Consulta la información de la factura para cuando se va a editar en la pantalla
        /// </summary>
        /// <param name="idFactura"></param>
        /// <returns></returns>
        [HttpGet("[Action]/{idFactura}")]
        public async Task<IActionResult> InformacionFactura(Guid idFactura)
        {
            try
            {
                var informacionFactura= await _facturaRepositorio.InformacionFactura(idFactura);
                return Ok(informacionFactura);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar la información de la factura""");
            }
        }



        /// <summary>
        /// Elimina la factura de la base de datos con el id informado
        /// </summary>
        /// <param name="idFactura"></param>
        /// <returns></returns>
        [HttpDelete("[Action]/{idFactura}")]
        public async Task<IActionResult> EliminarFactura(Guid idFactura)
        {
            try
            {
                await _facturaRepositorio.EliminarFactura(idFactura);
                return Ok(@"""Se ha eliminado la factura correctamente""");
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al eliminar la factura""");
            }
        }

    }
}
