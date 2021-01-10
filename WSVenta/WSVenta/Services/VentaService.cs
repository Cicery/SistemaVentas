using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Request;

namespace WSVenta.Services
{
    public class VentaService : IVentaService
    {
        public void Add(VentaRequest requestModel)
        {         
           
                using (VentaRealContext db = new VentaRealContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var venta = new Venta();
                            venta.Total = requestModel.Conceptos.Sum(x => x.Cantidad * x.PrecioUnitario);
                            venta.Fecha = DateTime.Now;
                            venta.IdCliente = requestModel.IdCliente;
                            db.Venta.Add(venta);
                            db.SaveChanges();

                            foreach (var modelConcepto in requestModel.Conceptos)
                            {
                                var concepto = new Concepto();
                                concepto.Cantidad = modelConcepto.Cantidad;
                                concepto.IdProducto = modelConcepto.IdProducto;
                                concepto.PrecioUnitario = modelConcepto.PrecioUnitario;
                                concepto.IdVenta = venta.Id;
                                concepto.Importe = modelConcepto.Importe;
                                db.Concepto.Add(concepto);
                                db.SaveChanges();
                            }
                            transaction.Commit();

                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            throw new Exception("Error en el proceso de insercion de la venta", e);
                        }

                    }
                }      
            
 
        }
    }
}
