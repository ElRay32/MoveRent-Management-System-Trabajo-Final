using Microsoft.Data.SqlClient;

namespace MoveRent.Services
{
    public class ClienteService
    {
        private string connectionString = "Data Source=.;Initial Catalog=MoveRentDB;Integrated Security=True;TrustServerCertificate=True;";

        // Método que permite agregar un nuevo cliente a la base de datos
        public void AgregarCliente()
        {
            // Solicita al usuario los datos del cliente a agregar
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            Console.Write("Cédula: ");
            string cedula = Console.ReadLine();
            Console.Write("Edad: ");
            string edad = Console.ReadLine();
            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Inserta el cliente y registra la fecha actual como creación y modificación
                SqlCommand cmd = new SqlCommand("INSERT INTO Cliente (Nombre, Apellido, Cedula, Edad, Telefono, FechaCreacion, FechaModificacion) VALUES (@nombre, @apellido, @cedula, @edad, @telefono, GETDATE(), GETDATE())", conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@cedula", cedula);
                cmd.Parameters.AddWithValue("@edad", edad);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.ExecuteNonQuery(); // Ejecuta la inserción
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

                // Consulta que selecciona todos los registros de la tabla Cliente Y Recorre los resultados y los imprime en consola
                SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- Lista de Clientes ---");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Nombre: {reader["Nombre"]}, Cédula: {reader["Cedula"]},  Edad: {reader["Edad"]},  Teléfono: {reader["Telefono"]},  Fecha Creación: {reader["FechaCreacion"]},  Fecha Modificación: {reader["FechaModificacion"]}");
                }
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        // Método para actualizar los datos de un cliente existente
        public void ActualizarCliente()
        {
            // Solicita el ID del cliente a modificar y los nuevos datos
            Console.Write("ID del cliente a modificar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nuevo nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Nuevo apellido: ");
            string apellido = Console.ReadLine();

            Console.Write("Nueva cédula: ");
            string cedula = Console.ReadLine();

            Console.Write("Nueva edad: ");
            string edad = Console.ReadLine();

            Console.Write("Nuevo teléfono: ");
            string telefono = Console.ReadLine();

            // Establece conexión y ejecuta un UPDATE con los nuevos valores
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Actualiza la fila correspondiente con nuevos valores y fecha de modificación
                var cmd = new SqlCommand("UPDATE Cliente SET Nombre=@nombre, Apellido=@apellido, Cedula=@cedula, Edad=@edad, Telefono=@telefono, FechaModificacion=GETDATE() WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@cedula", cedula);
                cmd.Parameters.AddWithValue("@edad", edad);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.Parameters.AddWithValue("@id", id);
                int filas = cmd.ExecuteNonQuery(); // Ejecuta el update

                // Verifica si se modificó alguna fila y muestra el resultado
                if (filas > 0)
                    Console.WriteLine("Cliente actualizado exitosamente.");
                else
                    Console.WriteLine("Cliente no encontrado.");
            }
        }

        // Método para eliminar un cliente por su ID
        public void EliminarCliente()
        {
            Console.Write("ID del cliente a eliminar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Ejecuta un DELETE para eliminar el cliente correspondiente
                var cmd = new SqlCommand("DELETE FROM Cliente WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                int filas = cmd.ExecuteNonQuery();

                // Informa si la eliminación fue exitosa
                if (filas > 0)
                    Console.WriteLine("Cliente eliminado exitosamente.");
                else
                    Console.WriteLine("Cliente no encontrado.");
            }
        }
    }
}
