using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;
using WSVenta.Services;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {

        private IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            this._ventaService = ventaService;
        }

        [HttpPost]
        public IActionResult Add(VentaRequest requestModel)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                //var venta = new VentaService();
                _ventaService.Add(requestModel);
                respuesta.Exito = 1;

            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
                return BadRequest(respuesta);

            }

            return Ok(respuesta);
        }
    }
}
