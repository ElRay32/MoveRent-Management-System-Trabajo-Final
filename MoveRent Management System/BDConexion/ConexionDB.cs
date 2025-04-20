using Microsoft.Data.SqlClient;

namespace RentalCar.DAL
{
    public class ConexionDB
    {
        private readonly string cadenaConexion = "Data Source=.;Initial Catalog=RentalCarDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
