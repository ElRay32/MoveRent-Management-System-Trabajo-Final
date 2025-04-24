using Microsoft.Data.SqlClient;

namespace MoveRent.BD
{
    public class ConexionDB
    {
        string cadenaConexion = "Data Source=localhost;Initial Catalog=MoveRentDB;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        public static SqlConnection GetConnection()
        {
            string connectionString = "tu_cadena_de_conexion";
            return new SqlConnection(connectionString);
        }
    }
}
