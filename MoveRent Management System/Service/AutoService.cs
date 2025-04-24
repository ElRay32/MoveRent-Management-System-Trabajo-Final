using System.Data.SqlClient;

namespace MoveRent.Services
{
    public class AutoService
    {
        private string connectionString = "Data Source=.;Initial Catalog=MoveRentDB;Integrated Security=True;";

        // Método que permite agregar un nuevo auto
        public void AgregarAuto()
        {
            // Se solicitan los datos del nuevo auto al usuario
            Console.Write("Marca: ");
            string marca = Console.ReadLine();
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();
            Console.Write("Placa: ");
            string placa = Console.ReadLine();
            Console.Write("Año: ");
            string año = Console.ReadLine();
            Console.Write("Color: ");
            string color = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Se verifica si ya existe un auto con esa placa
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Auto WHERE Placa = @placa", conn);
                checkCmd.Parameters.AddWithValue("@placa", placa);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    Console.WriteLine("Ya existe un auto con esta placa.");
                    return;
                }

                // Se inserta el nuevo auto en la base de datos con su fecha de creación/modificación
                SqlCommand cmd = new SqlCommand("INSERT INTO Auto (Marca, Modelo, Placa, Año, Color, Disponible, FechaCreacion, FechaModificacion) VALUES (@marca, @modelo, @placa, @año, @color, 1, GETDATE(), GETDATE())", conn);
                cmd.Parameters.AddWithValue("@marca", marca);
                cmd.Parameters.AddWithValue("@modelo", modelo);
                cmd.Parameters.AddWithValue("@placa", placa);
                cmd.Parameters.AddWithValue("@año", año);
                cmd.Parameters.AddWithValue("@color", color);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Auto agregado exitosamente.");
            }
        }

        // Método que lista todos los autos disponibles
        public void ListarAutos()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Se consulta la tabla Auto filtrando solo los autos disponibles
                SqlCommand cmd = new SqlCommand("SELECT * FROM Auto WHERE Disponible = 1", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- Autos Disponibles ---");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Marca: {reader["Marca"]}, Modelo: {reader["Modelo"]}, Placa: {reader["Placa"]}, Año: {reader["Año"]}, Color: {reader["Color"]}");
                }
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        // Método para actualizar los datos de un auto
        public void ActualizarAuto()
        {
            // Se solicitan nuevos datos para el auto a modificar
            Console.Write("ID del auto a modificar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nueva Marca: ");
            string marca = Console.ReadLine();
            Console.Write("Nuevo Modelo: ");
            string modelo = Console.ReadLine();
            Console.Write("Nueva Placa: ");
            string placa = Console.ReadLine();
            Console.Write("Nuevo Año: ");
            string año = Console.ReadLine();
            Console.Write("Nuevo Color: ");
            string color = Console.ReadLine();
            Console.Write("¿Está disponible? (true/false): ");
            bool disponible = Convert.ToBoolean(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Se actualizan los datos del auto y se registra la fecha de modificación
                SqlCommand cmd = new SqlCommand("UPDATE Auto SET Marca = @marca, Modelo = @modelo, Placa = @placa, Año = @año, Color = @color, Disponible = @disponible, FechaModificacion = GETDATE() WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@marca", marca);
                cmd.Parameters.AddWithValue("@modelo", modelo);
                cmd.Parameters.AddWithValue("@placa", placa);
                cmd.Parameters.AddWithValue("@año", año);
                cmd.Parameters.AddWithValue("@color", color);
                cmd.Parameters.AddWithValue("@disponible", disponible);
                cmd.Parameters.AddWithValue("@id", id);

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas > 0)
                    Console.WriteLine("Auto actualizado correctamente.");
                else
                    Console.WriteLine("No se encontró ningún auto con ese ID.");
            }
        }

        // Método para eliminar un auto por su ID
        public void EliminarAuto()
        {
            Console.Write("ID del auto a eliminar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Se elimina el auto con el ID indicado
                SqlCommand cmd = new SqlCommand("DELETE FROM Auto WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas > 0)
                    Console.WriteLine("Auto eliminado correctamente.");
                else
                    Console.WriteLine("No se encontró ningún auto con ese ID.");
            }
        }
    }
}
