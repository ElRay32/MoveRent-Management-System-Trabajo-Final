using Microsoft.Data.SqlClient;
using RentalCar.DAL;
using RentalCar.Models;

namespace RentalCar.BLL
{
    // Clase encargada de la lógica de negocio relacionada a los autos
    public class AutoService
    {
        private string connectionString = "Data Source=.;Initial Catalog=RentalCarDB;Integrated Security=True;";

        // Método para agregar un auto nuevo a la base de datos
        public void AgregarAuto()
        {
            Console.Write("Marca: ");
            string marca = Console.ReadLine();
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();
            Console.Write("Placa: ");
            string placa = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Verificamos si ya existe un auto con la misma placa
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Auto WHERE Placa = @placa", conn);
                checkCmd.Parameters.AddWithValue("@placa", placa);
                int existe = (int)checkCmd.ExecuteScalar();

                if (existe > 0)
                {
                    Console.WriteLine("Error: Ya existe un auto con esa placa.");
                }
                else
                {
                    // Insertamos el auto en la base de datos
                    SqlCommand cmd = new SqlCommand("INSERT INTO Auto (Marca, Modelo, Placa, Disponible) VALUES (@marca, @modelo, @placa, 1)", conn);
                    cmd.Parameters.AddWithValue("@marca", marca);
                    cmd.Parameters.AddWithValue("@modelo", modelo);
                    cmd.Parameters.AddWithValue("@placa", placa);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Auto registrado exitosamente.");
                }
            }
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        // Método para mostrar en consola todos los autos disponibles
        public void ListarAutos()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Auto WHERE Disponible = 1", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("\n--- Autos Disponibles ---");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Marca: {reader["Marca"]}, Modelo: {reader["Modelo"]}, Placa: {reader["Placa"]}");
                }
            }
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}

