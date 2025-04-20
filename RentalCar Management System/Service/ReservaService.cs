using Microsoft.Data.SqlClient;


namespace RentalCar.Service
{
    // Clase encargada de manejar las reservas de autos
    public class ReservaService
    {
        private string connectionString = "Data Source=.;Initial Catalog=RentalCarDB;Integrated Security=True;";

        // Método para realizar una reserva si el auto está disponible
        public void HacerReserva()
        {
            Console.Write("ID Cliente: ");
            int idCliente = int.Parse(Console.ReadLine());
            Console.Write("ID Auto: ");
            int idAuto = int.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Validamos si el auto está disponible
                SqlCommand validacion = new SqlCommand("SELECT Disponible FROM Auto WHERE Id = @idAuto", conn);
                validacion.Parameters.AddWithValue("@idAuto", idAuto);
                var disponible = validacion.ExecuteScalar();

                if (disponible != null && (bool)disponible)
                {
                    // Registramos la reserva y marcamos el auto como no disponible
                    SqlCommand cmd = new SqlCommand("INSERT INTO Reserva (IdCliente, IdAuto, FechaReserva) VALUES (@idCliente, @idAuto, GETDATE()); UPDATE Auto SET Disponible = 0 WHERE Id = @idAuto", conn);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.Parameters.AddWithValue("@idAuto", idAuto);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Reserva realizada correctamente.");
                }
                else
                {
                    Console.WriteLine("Error: Auto no disponible o no encontrado.");
                }
            }
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
