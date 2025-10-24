using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(TipoCuenta tipoCuenta)
        {
            if(!ModelState.IsValid)// si el modelo no es valido
            {
                return View(tipoCuenta);// con esto estamos devolviendo el modelo con los errores de validacion
            }
            return View();
        }
    }

}