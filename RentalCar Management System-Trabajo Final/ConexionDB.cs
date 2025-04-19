using System.Data.SqlClient;

namespace RentalCar.DAL
{
    // Clase que maneja la conexión con la base de datos SQL Server
    public class ConexionDB
    {
        private readonly string _cadenaConexion = "Data Source=.;Initial Catalog=RentalCarDB;Integrated Security=True;";

        // Método que devuelve una nueva conexión SQL
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}