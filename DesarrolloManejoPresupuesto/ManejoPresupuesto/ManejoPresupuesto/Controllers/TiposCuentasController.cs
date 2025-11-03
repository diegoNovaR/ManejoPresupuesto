using Dapper;
using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController : Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;
        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
        }


        public async Task<IActionResult> Index()
        {
            var usuarioId = 1; //temporalmente asignamos el usuario 1
            var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);
            return View(tiposCuentas);
        }


        public IActionResult Crear()
        {

                return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoCuenta tipoCuenta)
        {
            if(!ModelState.IsValid)// si el modelo no es valido
            {
                return View(tipoCuenta);// con esto estamos devolviendo el modelo con los errores de validacion
            }

            tipoCuenta.UsuarioId = 1;//temporalmente asignamos el usuario 1

            var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

            if(yaExisteTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuenta.Nombre), $"El nombre {tipoCuenta.Nombre} ya existe");
                //con nameof nos referimos a la propiedad Nombre del modelo TipoCuenta
                return View(tipoCuenta);
            }

            await repositorioTiposCuentas.Crear(tipoCuenta);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)//accion para validacion remota
        {
            var usuarioId = 1; // Reemplaza esto con la lógica para obtener el ID del usuario actual
            var existeTipoCuenta = await repositorioTiposCuentas.Existe(nombre, usuarioId);
            if(existeTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe.");
            }

            return Json(true);
        }



    }

}