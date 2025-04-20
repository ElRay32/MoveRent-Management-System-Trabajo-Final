using Microsoft.Data.SqlClient;
using RentalCar.Models;
using RentalCar.DAL;

namespace RentalCar.BLL
{
    // Clase de servicio que maneja la lógica de negocio para los autos
    public class AutoService
    {
        private ConexionDB conexion = new ConexionDB();

        // Método para agregar un nuevo auto a la base de datos
        public void AgregarAuto(ModelsAutoPersonas auto)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Auto (Marca, Modelo, Placa, Disponible) VALUES (@marca, @modelo, @placa, @disponible)", conn);
                cmd.Parameters.AddWithValue("@marca", auto.Marca);
                cmd.Parameters.AddWithValue("@modelo", auto.Modelo);
                cmd.Parameters.AddWithValue("@placa", auto.Placa);
                cmd.Parameters.AddWithValue("@disponible", auto.Disponible);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

