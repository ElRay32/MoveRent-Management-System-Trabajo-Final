using System.Data.SqlClient;

namespace MoveRent.Services
{
    public class ReservaService
    {
        // Cadena de conexión a la base de datos MoveRentDB
        private string connectionString = "Data Source=localhost;Initial Catalog=MoveRentDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        // Método para realizar una reserva si el auto está disponible
        public void HacerReserva()
        {
            // Solicita al usuario los datos de entrada
            Console.Write("ID Cliente: ");
            int idCliente = int.Parse(Console.ReadLine());
            Console.Write("ID Auto: ");
            int idAuto = int.Parse(Console.ReadLine());

            // Genera un monto aleatorio entre 3000 y 9000
            Random rnd = new Random();
            decimal montoTotal = rnd.Next(3000, 9001);

            // Establece la conexión con la base de datos
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Verifica si el auto está disponible
                SqlCommand validacion = new SqlCommand("SELECT Disponible FROM Auto WHERE Id = @idAuto", conn);
                validacion.Parameters.AddWithValue("@idAuto", idAuto);
                var disponible = validacion.ExecuteScalar();

                // Si está disponible, registra la reserva y marca el auto como no disponible
                if (disponible != null && (bool)disponible)
                {
                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO Reserva (IdCliente, IdAuto, FechaReserva, MontoTotal, FechaModificacion)
                        VALUES (@idCliente, @idAuto, GETDATE(), @monto, GETDATE());
                        UPDATE Auto SET Disponible = 0 WHERE Id = @idAuto", conn);

                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.Parameters.AddWithValue("@idAuto", idAuto);
                    cmd.Parameters.AddWithValue("@monto", montoTotal);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine($"✅ Reserva realizada correctamente. Monto total: RD${montoTotal}");
                }
                else
                {
                    Console.WriteLine("❌ Error: Auto no disponible o no encontrado.");
                }
            } 

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        // Método que muestra todas las reservas realizadas, incluyendo datos del cliente y del auto
        public void ListarReservas()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Consulta que une las tablas Reserva, Cliente y Auto
                SqlCommand cmd = new SqlCommand(@"
                    SELECT r.Id, c.Nombre, a.Marca, a.Modelo, r.FechaReserva, r.MontoTotal
                    FROM Reserva r
                    INNER JOIN Cliente c ON r.IdCliente = c.Id
                    INNER JOIN Auto a ON r.IdAuto = a.Id", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- Lista de Reservas ---");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Cliente: {reader["Nombre"]}, Auto: {reader["Marca"]} {reader["Modelo"]}, Fecha: {((DateTime)reader["FechaReserva"]).ToShortDateString()}, Monto: RD${reader["MontoTotal"]}");
                }
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        // Método para actualizar una reserva existente
        public void ActualizarReserva()
        {
            // Entrada de datos
            Console.Write("ID de la reserva a modificar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nuevo ID Cliente: ");
            int idCliente = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nuevo ID Auto: ");
            int idAuto = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nueva fecha (YYYY-MM-DD): ");
            DateTime fecha = DateTime.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Actualiza la reserva incluyendo la fecha de modificación
                var cmd = new SqlCommand("UPDATE Reserva SET IdCliente=@idCliente, IdAuto=@idAuto, FechaReserva=@fecha, FechaModificacion=GETDATE() WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@idAuto", idAuto);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@id", id);

                int filas = cmd.ExecuteNonQuery();

                if (filas > 0)
                    Console.WriteLine("Reserva actualizada correctamente.");
                else
                    Console.WriteLine("No se encontró la reserva.");
            }
        }

        // Método para eliminar una reserva existente
        public void EliminarReserva()
        {
            Console.Write("ID de la reserva a eliminar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Elimina la reserva con el ID dado
                var cmd = new SqlCommand("DELETE FROM Reserva WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                int filas = cmd.ExecuteNonQuery();

                if (filas > 0)
                    Console.WriteLine("Reserva eliminada correctamente.");
                else
                    Console.WriteLine("No se encontró la reserva.");
            }
        }
    }
}
