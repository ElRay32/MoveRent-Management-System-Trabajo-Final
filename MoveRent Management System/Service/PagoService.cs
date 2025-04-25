using Microsoft.Data.SqlClient;

namespace MoveRent.Services
{
    public class PagoService
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=MoveRentDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        // Método para registrar un pago de una reserva específica
        public void PagarReserva()
        {
            Console.Write("ID de la reserva a pagar: ");
            int idReserva = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Verificar si existe y si ya está pagada
                SqlCommand verificar = new SqlCommand("SELECT MontoTotal, Pagado FROM Reserva WHERE Id = @idReserva", conn);
                verificar.Parameters.AddWithValue("@idReserva", idReserva);
                SqlDataReader reader = verificar.ExecuteReader();

                if (!reader.Read())
                {
                    Console.WriteLine("La reserva no existe.");
                    Console.ReadKey(); return;
                }

                decimal montoTotal = Convert.ToDecimal(reader["MontoTotal"]);
                bool yaPagado = Convert.ToBoolean(reader["Pagado"]);
                reader.Close();

                if (yaPagado)
                {
                    Console.WriteLine("Esta reserva ya fue pagada.");
                    Console.ReadKey(); return;
                }

                // Ingresar monto a pagar
                Console.Write("Ingrese el monto a pagar (RD$): ");
                decimal montoPagado = Convert.ToDecimal(Console.ReadLine());

                // Registrar el pago
                SqlCommand insertarPago = new SqlCommand("INSERT INTO Pago (IdReserva, Monto, FechaPago) VALUES (@idReserva, @monto, GETDATE())", conn);
                insertarPago.Parameters.AddWithValue("@idReserva", idReserva);
                insertarPago.Parameters.AddWithValue("@monto", montoPagado);
                insertarPago.ExecuteNonQuery();

                // Si el monto cubre el total, marcar como pagado
                if (montoPagado >= montoTotal)
                {
                    SqlCommand marcarPagado = new SqlCommand("UPDATE Reserva SET Pagado = 1 WHERE Id = @idReserva", conn);
                    marcarPagado.Parameters.AddWithValue("@idReserva", idReserva);
                    marcarPagado.ExecuteNonQuery();
                }

                // Mensaje al usuario
                if (montoPagado == montoTotal)
                {
                    Console.WriteLine("Pago exacto realizado exitosamente.");
                }
                else if (montoPagado > montoTotal)
                {
                    decimal cambio = montoPagado - montoTotal;
                    Console.WriteLine($"Pago exitoso. Su cambio es: RD${cambio}");
                }
                else
                {
                    decimal restante = montoTotal - montoPagado;
                    Console.WriteLine($"Pago incompleto. Aún debe: RD${restante}");
                }

                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}