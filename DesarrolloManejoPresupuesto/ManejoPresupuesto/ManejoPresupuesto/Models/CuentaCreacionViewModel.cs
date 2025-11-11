using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Models
{
    public class CuentaCreacionViewModel:Cuenta
    {
        //nos permite ccrear selectionables en las vistas
        public IEnumerable<SelectListItem> TiposCuentas { get; set; }//selectListItem es una clase que representa un elemento en un dropdown

    }
}
