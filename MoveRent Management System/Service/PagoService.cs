using Microsoft.Data.SqlClient;

namespace RentalCar.Service
{
    // Clase encargada de manejar los pagos asociados a las reservas
    public class PagoService
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=RentalCarDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        // Método para registrar un pago de una reserva específica
        public void RegistrarPago()
        {
            Console.Write("ID Reserva: ");
            int idReserva = int.Parse(Console.ReadLine());
            Console.Write("Monto a pagar (RD$): ");
            decimal montoPagado = decimal.Parse(Console.ReadLine());

            decimal montoTotal = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand getMonto = new SqlCommand("SELECT MontoTotal FROM Reserva WHERE Id = @idReserva", conn);
                getMonto.Parameters.AddWithValue("@idReserva", idReserva);
                var result = getMonto.ExecuteScalar();

                // Verificamos que la reserva exista y que el monto total pueda convertirse a decimal
                if (result != null && decimal.TryParse(result.ToString(), out montoTotal))
                {
                    // Insertamos el pago en la base de datos
                    SqlCommand cmd = new SqlCommand("INSERT INTO Pago (IdReserva, Monto, FechaPago) VALUES (@idReserva, @monto, GETDATE())", conn);
                    cmd.Parameters.AddWithValue("@idReserva", idReserva);
                    cmd.Parameters.AddWithValue("@monto", montoPagado);
                    cmd.ExecuteNonQuery();

                    // Comparamos el monto pagado con el monto total de la reserva
                    if (montoPagado == montoTotal)
                    {
                        Console.WriteLine("✅ Pago exitoso. Gracias por su pago exacto.");
                    }
                    else if (montoPagado > montoTotal)
                    {
                        decimal sobrante = montoPagado - montoTotal;
                        Console.WriteLine($"✅ Pago exitoso. Su cambio es: RD${sobrante}");
                    }
                    else
                    {
                        decimal falta = montoTotal - montoPagado;
                        Console.WriteLine($"⚠️ Pago insuficiente. Aún debe: RD${falta}");
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
