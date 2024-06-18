using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
	public class Empleado
	{
		public static (bool, string, List<ML.Empleado> Empleados, Exception) GetAll()
		{
			try
			{
				using (DL.HcanalesEmpleadoAjaxContext context = new DL.HcanalesEmpleadoAjaxContext())
				{
					var getEmpleados = (from emp in context.Empleados
										join fed in context.EntidadFederativas on emp.IdEstado equals fed.IdEstado
										select new
										{
											IdEmpleado = emp.IdEmpleado,
											NumeroNomina = emp.NumeroNomina,
											Nombre = emp.Nombre,
											ApellidoPaterno = emp.ApellidoPaterno,
											ApellidoMaterno = emp.ApellidoMaterno,
											IdEstado = emp.IdEstado,
											Estado = fed.Estado
										}
										);
					if (getEmpleados != null)
					{
						List<ML.Empleado> empleados = new List<ML.Empleado>();
						foreach (var Empleado in getEmpleados)
						{
							ML.Empleado empleado = new ML.Empleado
							{
								IdEmpleado = Empleado.IdEmpleado,
								NumeroNomina = Empleado.NumeroNomina,
								Nombre = Empleado.Nombre,
								ApellidoPaterno = Empleado.ApellidoPaterno,
								ApellidoMaterno = Empleado.ApellidoMaterno,

								EntidadFederativa = new ML.EntidadFederativa
								{
									IdEstado = Empleado.IdEstado.Value,
									Estado = Empleado.Estado

								}
							};
							empleados.Add(empleado);
						}
						return (true, null, empleados, null);
					}
					return (true, "No tiene registros", null, null);
				}
			}
			catch (Exception ex)
			{

				return (false, "no tiene regitros", null, null);
			}
		}



        public static (bool Success, string Message, ML.Empleado Empleado, Exception Error) GetById(int idEmpleado)
        {
            try
            {
                using (DL.HcanalesEmpleadoAjaxContext context = new DL.HcanalesEmpleadoAjaxContext())
                {
                    var getEmpleado = (from emp in context.Empleados
                                       join fed in context.EntidadFederativas on emp.IdEstado equals fed.IdEstado
                                       where emp.IdEmpleado == idEmpleado
                                       select new
                                       {
                                           IdEmpleado = emp.IdEmpleado,
                                           NumeroNomina = emp.NumeroNomina,
                                           Nombre = emp.Nombre,
                                           ApellidoPaterno = emp.ApellidoPaterno,
                                           ApellidoMaterno = emp.ApellidoMaterno,
                                           IdEstado = emp.IdEstado,
                                           Estado = fed.Estado,
                                       }
                                        ).SingleOrDefault();

                    if (getEmpleado != null)
                    {
                        ML.Empleado empleado = new ML.Empleado
                        {
                            IdEmpleado = getEmpleado.IdEmpleado,
                            NumeroNomina = getEmpleado.NumeroNomina,
                            Nombre = getEmpleado.Nombre,
                            ApellidoPaterno = getEmpleado.ApellidoPaterno,
                            ApellidoMaterno = getEmpleado.ApellidoMaterno,
                            EntidadFederativa = new ML.EntidadFederativa
                            {
                                IdEstado = getEmpleado.IdEstado.Value,
                                Estado = getEmpleado.Estado,
                            }
                        };
                        return (true, null, empleado, null);
                    }
                    return (false, "No se encontro el empleado", null, null);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null, ex);
            }
        }


		public static (bool Success, string Message, Exception Error) Add(ML.Empleado Empleado)
		{
			try
			{
				using(DL.HcanalesEmpleadoAjaxContext context = new DL.HcanalesEmpleadoAjaxContext())
				{
					DL.Empleado empleadoObj = new DL.Empleado
					{
						IdEmpleado = Empleado.IdEmpleado,
						NumeroNomina = Empleado.NumeroNomina,
						Nombre = Empleado.Nombre,
						ApellidoPaterno = Empleado.ApellidoPaterno,
						ApellidoMaterno = Empleado.ApellidoMaterno,
						IdEstado = Empleado.EntidadFederativa.IdEstado

					};
					
					context.Empleados.Add(empleadoObj);

					int rowAffected = context.SaveChanges();

					if(rowAffected > 0)
					{
						return(true,null,null);
					}
					else
					{
						return(false,"No se guardo el registro",null);
					}
				}
			}
			catch (Exception ex)
			{

				return (false, ex.Message, ex);
			}
		}

		public static (bool Success, string Message, Exception Error) Update(ML.Empleado Empleado)
		{
			try
			{
				using (DL.HcanalesEmpleadoAjaxContext context = new DL.HcanalesEmpleadoAjaxContext())
				{
					DL.Empleado empleadoObj = new DL.Empleado
					{
						IdEmpleado = Empleado.IdEmpleado,
						NumeroNomina = Empleado.NumeroNomina,
						Nombre = Empleado.Nombre,
						ApellidoPaterno = Empleado.ApellidoPaterno,
						ApellidoMaterno = Empleado.ApellidoMaterno,
						IdEstado = Empleado.EntidadFederativa.IdEstado
					};

					context.Empleados.Update(empleadoObj);

					int rowAffected = context.SaveChanges(); 

					if (rowAffected > 0)
					{
						return (true, null, null);
					}
					else
					{
						return(false, "No actualizo el registro", null);
					}
						
				}
			}
			catch (Exception ex )
			{

				return (false, ex.Message, ex);
			}
		}

		public static(bool Success, string Message, Exception Error ) Delete(int IdEmpleado)
		{
			try
			{	
				using(DL.HcanalesEmpleadoAjaxContext context = new DL.HcanalesEmpleadoAjaxContext())
				{
					context.Empleados.Remove(new DL.Empleado { IdEmpleado = IdEmpleado});
					int rowAffected = context.SaveChanges();

					if (rowAffected > 0)
					{
						return (true, null, null);
					}
					else
					{
						return (false, "A ocurrido un error al eliminar el registro", null);
					}
				}

			}
			catch (Exception ex)
			{

				return (false, ex.Message, ex);
			}
		}
    }
}
