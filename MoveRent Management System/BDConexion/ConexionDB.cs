using Microsoft.Data.SqlClient;

namespace RentalCar.DAL
{
    public class ConexionDB
    {
        string cadenaConexion = "Data Source=localhost;Initial Catalog=RentalCarDB;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
