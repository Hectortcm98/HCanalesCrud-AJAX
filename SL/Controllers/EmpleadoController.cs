using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        [HttpGet]
        [Route("Empleado/GetAll")]
        public IActionResult GetAll()
        {
            DTO.Result result = new DTO.Result();
            var task = BL.Empleado.GetAll();
            result.Success = task.Item1;
            result.Message = task.Item2;

            if (task.Item1)
            {
                result.Data = task.Empleados;
                return Ok(result);
            }
            else
            {
                result.Error = task.Item4;
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Route("Empleado/GetByid/{idEmpleado}")]
        public IActionResult GetById(int idEmpleado)
        {
            DTO.Result result = new DTO.Result();
            var task = BL.Empleado.GetById(idEmpleado);
            result.Success = task.Success;
            result.Message = task.Message;
            if (task.Success)
            {
                result.Data = task.Empleado;
                return Ok(result);
            }
            else
            {
                result.Error = task.Error;
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("Empleado/Add")]
        public IActionResult Add([FromBody] ML.Empleado empleado)
        {
            DTO.Result result = new DTO.Result();
            var task = BL.Empleado.Add(empleado);

            result.Success = task.Success;
            result.Message = task.Message;
            if (task.Success)
            {
                return Ok(result);
            }
            else
            {
                result.Error = task.Error;
                return BadRequest(result);
            }
        }

        [HttpPut]
        [Route("Empleado/Update")]
        public IActionResult Update([FromBody] ML.Empleado empleado)
        {
            DTO.Result result = new DTO.Result();
            var task = BL.Empleado.Update(empleado);

            result.Success = task.Success;
            result.Message = task.Message;
            if (task.Success)
            {
                return Ok(result);
            }
            else
            {
                result.Error = task.Error;
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("Empleado/Delete/{idEmpleado}")]
        public IActionResult Delete(int idEmpleado)
        {
            DTO.Result result = new DTO.Result();
            var task = BL.Empleado.Delete(idEmpleado);

            result.Success = task.Success;
            result.Message = task.Message;
            if (task.Success)
            {
                return Ok(result);
            }
            else
            {
                result.Error = task.Error;
                return BadRequest(result);
            }
        }

    }
}
