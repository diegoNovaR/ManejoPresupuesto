namespace ManejoPresupuesto.Servicios
{
    public interface IServicioUsuarios
    {
        int ObtenerUsuarioId();
    }
    public class ServicioUsarios: IServicioUsuarios
    {
        public int ObtenerUsuarioId()
        {
            return 1;
        }
    }
}
