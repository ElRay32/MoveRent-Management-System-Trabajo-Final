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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Validar que el cliente existe
                SqlCommand validarCliente = new SqlCommand("SELECT COUNT(*) FROM Cliente WHERE Id = @idCliente", conn);
                validarCliente.Parameters.AddWithValue("@idCliente", idCliente);
                int existeCliente = (int)validarCliente.ExecuteScalar();
                if (existeCliente == 0)
                {
                    Console.WriteLine("El cliente no existe.");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                // Validar que el auto existe y está disponible
                SqlCommand validarAuto = new SqlCommand("SELECT Disponible FROM Auto WHERE Id = @idAuto", conn);
                validarAuto.Parameters.AddWithValue("@idAuto", idAuto);
                var disponible = validarAuto.ExecuteScalar();

                if (disponible == null)
                {
                    Console.WriteLine("El auto no existe.");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                if (!(bool)disponible)
                {
                    Console.WriteLine("El auto no está disponible.");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                // Preguntar por la fecha de reserva
                Console.Write("¿Deseas ingresar una fecha personalizada para la reserva? (s/n): ");
                string respuesta = Console.ReadLine().ToLower();
                DateTime fechaReserva;

                if (respuesta == "s")
                {
                    Console.Write("Ingrese la fecha (YYYY-MM-DD): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out fechaReserva))
                    {
                        Console.WriteLine(" Fecha inválida. Se usará la fecha actual.");
                        fechaReserva = DateTime.Now;
                    }
                }
                else
                {
                    fechaReserva = DateTime.Now;
                }

                // Generar un monto aleatorio
                Random rnd = new Random();
                decimal montoTotal = rnd.Next(3000, 9001);

                // Registrar la reserva
                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Reserva (IdCliente, IdAuto, FechaReserva, MontoTotal, FechaModificacion)
            VALUES (@idCliente, @idAuto, @fechaReserva, @montoTotal, GETDATE());
            UPDATE Auto SET Disponible = 0 WHERE Id = @idAuto", conn);

                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@idAuto", idAuto);
                cmd.Parameters.AddWithValue("@fechaReserva", fechaReserva);
                cmd.Parameters.AddWithValue("@montoTotal", montoTotal);
                cmd.ExecuteNonQuery();

                Console.WriteLine($"Reserva realizada correctamente. Monto total: RD${montoTotal}");
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
            Console.Write("ID de la reserva a modificar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nuevo ID Cliente: ");
            int idCliente = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nuevo ID Auto: ");
            int idAuto = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Validar existencia del cliente
                SqlCommand validarCliente = new SqlCommand("SELECT COUNT(*) FROM Cliente WHERE Id = @idCliente", conn);
                validarCliente.Parameters.AddWithValue("@idCliente", idCliente);
                if ((int)validarCliente.ExecuteScalar() == 0)
                {
                    Console.WriteLine("Cliente no existe.");
                    Console.ReadKey(); return;
                }

                // Validar existencia del auto
                SqlCommand validarAuto = new SqlCommand("SELECT COUNT(*) FROM Auto WHERE Id = @idAuto", conn);
                validarAuto.Parameters.AddWithValue("@idAuto", idAuto);
                if ((int)validarAuto.ExecuteScalar() == 0)
                {
                    Console.WriteLine("Auto no existe.");
                    Console.ReadKey(); return;
                }

                // Obtener el auto actual asociado a la reserva
                SqlCommand getAutoViejo = new SqlCommand("SELECT IdAuto FROM Reserva WHERE Id = @id", conn);
                getAutoViejo.Parameters.AddWithValue("@id", id);
                var idAutoViejoObj = getAutoViejo.ExecuteScalar();
                if (idAutoViejoObj == null)
                {
                    Console.WriteLine("Reserva no encontrada.");
                    Console.ReadKey(); return;
                }

                int idAutoViejo = Convert.ToInt32(idAutoViejoObj);

                // Preguntar por nueva fecha
                Console.Write("¿Deseas ingresar una nueva fecha de reserva? (s/n): ");
                string respuesta = Console.ReadLine().ToLower();

                DateTime fecha = DateTime.Now;
                if (respuesta == "s")
                {
                    Console.Write("Ingrese la nueva fecha (YYYY-MM-DD): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out fecha))
                    {
                        Console.WriteLine("Fecha inválida. Usando fecha actual.");
                        fecha = DateTime.Now;
                    }
                }

                // Marcar el auto anterior como disponible
                SqlCommand liberarAutoViejo = new SqlCommand("UPDATE Auto SET Disponible = 1 WHERE Id = @idAutoViejo", conn);
                liberarAutoViejo.Parameters.AddWithValue("@idAutoViejo", idAutoViejo);
                liberarAutoViejo.ExecuteNonQuery();

                // Actualizar la reserva con nuevo auto y fecha
                SqlCommand actualizar = new SqlCommand(@"
            UPDATE Reserva 
            SET IdCliente = @idCliente, IdAuto = @idAuto, FechaReserva = @fecha, FechaModificacion = GETDATE()
            WHERE Id = @id", conn);

                actualizar.Parameters.AddWithValue("@idCliente", idCliente);
                actualizar.Parameters.AddWithValue("@idAuto", idAuto);
                actualizar.Parameters.AddWithValue("@fecha", fecha);
                actualizar.Parameters.AddWithValue("@id", id);
                int filas = actualizar.ExecuteNonQuery();

                // Marcar nuevo auto como no disponible
                SqlCommand bloquearNuevoAuto = new SqlCommand("UPDATE Auto SET Disponible = 0 WHERE Id = @idAuto", conn);
                bloquearNuevoAuto.Parameters.AddWithValue("@idAuto", idAuto);
                bloquearNuevoAuto.ExecuteNonQuery();

                if (filas > 0)
                    Console.WriteLine("Reserva actualizada correctamente.");
                else
                    Console.WriteLine("No se pudo actualizar la reserva.");
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        // Método para eliminar una reserva existente
        public void EliminarReserva()
        {
            Console.Write("ID de la reserva a eliminar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Obtener auto asociado
                SqlCommand getAuto = new SqlCommand("SELECT IdAuto FROM Reserva WHERE Id = @id", conn);
                getAuto.Parameters.AddWithValue("@id", id);
                var idAutoObj = getAuto.ExecuteScalar();
                if (idAutoObj == null)
                {
                    Console.WriteLine("Reserva no encontrada.");
                    Console.ReadKey(); return;
                }

                int idAuto = Convert.ToInt32(idAutoObj);

                // Marcar el auto como disponible
                SqlCommand liberarAuto = new SqlCommand("UPDATE Auto SET Disponible = 1 WHERE Id = @idAuto", conn);
                liberarAuto.Parameters.AddWithValue("@idAuto", idAuto);
                liberarAuto.ExecuteNonQuery();

                // Eliminar la reserva
                SqlCommand cmd = new SqlCommand("DELETE FROM Reserva WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                int filas = cmd.ExecuteNonQuery();

                if (filas > 0)
                    Console.WriteLine("Reserva eliminada correctamente y auto liberado.");
                else
                    Console.WriteLine("No se pudo eliminar la reserva.");
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

    }
}
