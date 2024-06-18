using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EntidadFederativa
    {
        public static (bool Success, string Message, List<ML.EntidadFederativa> entidadFederativas, Exception Error) GetAll()
        {
            try
            {
                using (DL.HcanalesEmpleadoAjaxContext context = new DL.HcanalesEmpleadoAjaxContext())
                {
                    var getEstados = context.EntidadFederativas.ToList();

                    if (getEstados != null)
                    {
                        List<ML.EntidadFederativa> EntidadFederativas = new List<ML.EntidadFederativa>();
                        foreach (var getEstado in getEstados)
                        {
                            ML.EntidadFederativa entidadFed = new ML.EntidadFederativa
                            {
                                IdEstado = getEstado.IdEstado,
                                Estado = getEstado.Estado
                            };

                            EntidadFederativas.Add(entidadFed);
                        }

                        return (true, null, EntidadFederativas, null);
                    }
                    return (false, "No hay estados", null, null);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null, ex);
            }
        }
    }
}
