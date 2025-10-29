using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas//principio de inversión de dependencias
                                             //la interfaz puede ir en un archivo aparte
    {
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
    }
    public class RepositorioTiposCuentas: IRepositorioTiposCuentas
    {
        private readonly string connectionString;

        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection =  new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO TiposCuentas(Nombre, UsuarioId, Orden)
                        Values(@Nombre, @UsuarioId, 0);
                        SELECT SCOPE_IDENTITY();", tipoCuenta);//devuelve el id del registro insertado
            tipoCuenta.Id = id;//colocamos el ID del tipo cuenta creado
        }

        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 
                            FROM TiposCuentas
                            WHERE Nombre = @Nombre AND UsuarioId = @usuarioId;", new {nombre, usuarioId});
            return existe == 1;
        }



    }
}
