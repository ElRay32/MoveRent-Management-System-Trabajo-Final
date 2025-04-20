using Microsoft.Data.SqlClient;


namespace RentalCar.Service
{
    // Clase que contiene métodos para gestionar clientes en la base de datos
    public class ClienteService
    {
        private string connectionString = "Data Source=.;Initial Catalog=RentalCarDB;Integrated Security=True;";
       
        // Método que permite agregar un nuevo cliente a la base de datos
        public void AgregarCliente()
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Cédula: ");
            string cedula = Console.ReadLine();
            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Cliente (Nombre, Cedula, Telefono) VALUES (@nombre, @cedula, @telefono)", conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@cedula", cedula);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Cliente agregado correctamente.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        // Método que muestra una lista de todos los clientes registrados
        public void ListarClientes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // Imprime en consola cada cliente encontrado
                Console.WriteLine("\n--- Lista de Clientes ---");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Nombre: {reader["Nombre"]}, Cédula: {reader["Cedula"]}, Teléfono: {reader["Telefono"]}");
                }
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
