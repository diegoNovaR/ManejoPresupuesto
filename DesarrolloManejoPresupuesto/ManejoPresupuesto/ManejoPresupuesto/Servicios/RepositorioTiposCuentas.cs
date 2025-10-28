using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas//principio de inversión de dependencias
                                             //la interfaz puede ir en un archivo aparte
    {
        void Crear(TipoCuenta tipoCuenta);
    }
    public class RepositorioTiposCuentas: IRepositorioTiposCuentas
    {
        private readonly string connectionString;

        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Crear(TipoCuenta tipoCuenta)
        {
            using var connection =  new SqlConnection(connectionString);
            var id = connection.QuerySingle<int>($@"INSERT INTO TiposCuentas(Nombre, UsuarioId, Orden)
                        Values(@Nombre, @UsuarioId, 0);
                        SELECT SCOPE_IDENTITY();", tipoCuenta);//devuelve el id del registro insertado
            tipoCuenta.Id = id;//colocamos el ID del tipo cuenta creado
        }



    }
}
