using Microsoft.Data.SqlClient;

namespace MoveRent.Services
{
    public class PagoService
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=MoveRentDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        // Método para registrar un pago de una reserva específica
        public void RegistrarPago()
        {
            // Solicita al usuario el ID de la reserva y el monto que desea pagar
            Console.Write("ID Reserva: ");
            int idReserva = int.Parse(Console.ReadLine());
            Console.Write("Monto a pagar (RD$): ");
            decimal montoPagado = decimal.Parse(Console.ReadLine());

            decimal montoTotal = 0; // Variable donde se guardará el monto total esperado de la reserva

            // Se establece la conexión con la base de datos
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Se consulta el monto total asociado a la reserva ingresada
                SqlCommand getMonto = new SqlCommand("SELECT MontoTotal FROM Reserva WHERE Id = @idReserva", conn);
                getMonto.Parameters.AddWithValue("@idReserva", idReserva);
                var result = getMonto.ExecuteScalar(); // Obtiene un solo valor

                // Verificamos que se obtuvo un monto y que puede convertirse a decimal
                if (result != null && decimal.TryParse(result.ToString(), out montoTotal))
                {
                    // Si la reserva existe, registramos el pago en la tabla Pago
                    SqlCommand cmd = new SqlCommand("INSERT INTO Pago (IdReserva, Monto, FechaPago) VALUES (@idReserva, @monto, GETDATE())", conn);
                    cmd.Parameters.AddWithValue("@idReserva", idReserva);
                    cmd.Parameters.AddWithValue("@monto", montoPagado);
                    cmd.ExecuteNonQuery();

                    // Evaluamos el resultado del pago comparando con el monto total
                    if (montoPagado == montoTotal)
                    {
                        Console.WriteLine("Pago exitoso. Gracias por su pago exacto.");
                    }
                    else if (montoPagado > montoTotal)
                    {
                        // Si pagó de más, se calcula y muestra el sobrante
                        decimal sobrante = montoPagado - montoTotal;
                        Console.WriteLine($"Pago exitoso. Su cambio es: RD${sobrante}");
                    }
                    else
                    {
                        // Si pagó de menos, se muestra cuánto falta
                        decimal falta = montoTotal - montoPagado;
                        Console.WriteLine($"Pago insuficiente. Aún debe: RD${falta}");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Reserva no encontrada.");
                }
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
