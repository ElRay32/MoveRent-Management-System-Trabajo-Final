using Microsoft.Data.SqlClient;

namespace RentalCar.Service
{
    // Clase encargada de manejar los pagos asociados a las reservas
    public class PagoService
    {
        private string connectionString = "Data Source=.;InitialCatalog=RentalCarDB;Integrated Security=True;";

        // Método para registrar un pago de una reserva específica
        public void RegistrarPago()
        {
            Console.Write("ID Reserva: ");
            int idReserva = int.Parse(Console.ReadLine());
            Console.Write("Monto (RD$): ");
            decimal monto = decimal.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Insertamos el pago en la base de datos
                SqlCommand cmd = new SqlCommand("INSERT INTO Pago (IdReserva, Monto, FechaPago) VALUES (@idReserva, @monto, GETDATE())", conn);
                cmd.Parameters.AddWithValue("@idReserva", idReserva);
                cmd.Parameters.AddWithValue("@monto", monto);
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Pago registrado correctamente.\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
