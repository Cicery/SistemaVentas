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


namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta objResponse = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    var lst = db.Cliente.OrderByDescending(d => d.Id).ToList();
                    objResponse.Exito = 1;
                    objResponse.Data = lst;

                }
            }
            catch (Exception e)
            {


                objResponse.Mensaje = e.Message;
            }
            return Ok(objResponse);

        }

        [HttpPost]
        public IActionResult Add(ClienteRequest requestCliente)
        {
            Respuesta objResponse = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente objCliente = new Cliente();
                    objCliente.Nombre = requestCliente.Nombre;
                    db.Cliente.Add(objCliente);
                    db.SaveChanges();
                    objResponse.Exito = 1;

                }
            }
            catch (Exception e)
            {

                objResponse.Mensaje = e.Message;
            }

            return Ok(objResponse);
        }


        [HttpPut]
        public IActionResult Edit(ClienteRequest requestCliente)
        {
            Respuesta objResponse = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente objCliente = db.Cliente.Find(requestCliente.Id);
                    objCliente.Nombre = requestCliente.Nombre;
                    db.Entry(objCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    objResponse.Exito = 1;

                }
            }
            catch (Exception e)
            {

                objResponse.Mensaje = e.Message;
            }

            return Ok(objResponse);
        }


        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            Respuesta objResponse = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente objCliente = db.Cliente.Find(Id);
                    db.Remove(objCliente);
                    db.SaveChanges();
                    objResponse.Exito = 1;

                }
            }
            catch (Exception e)
            {

                objResponse.Mensaje = e.Message;
            }

            return Ok(objResponse);
        }
    }
}
