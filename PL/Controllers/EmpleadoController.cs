using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
    }
}
