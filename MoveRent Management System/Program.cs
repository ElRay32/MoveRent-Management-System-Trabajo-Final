using MoveRent.Services;// Importa los espacios de nombres que contienen las clases de servicios para autos, clientes y reservas.

// Clase principal del programa
class Program
{
    // Método de entrada principal
    static void Main(string[] args)
    {
        // Instancias de los servicios de negocio
        var autoService = new AutoService();
        var clienteService = new ClienteService();
        var reservaService = new ReservaService();

        // Bucle infinito del menú principal hasta que el usuario decida salir y Switch para redirigir al módulo correspondiente con los menus de auto, clientes, reservas.
        while (true)
        {
            Console.Clear();
            Console.WriteLine("====== MoveRent Management ======");
            Console.WriteLine("1. Gestionar Autos");
            Console.WriteLine("2. Gestionar Clientes");
            Console.WriteLine("3. Gestionar Reservas");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MenuAutos(autoService);   
                    break;
                case "2":
                    MenuClientes(clienteService); 
                    break;
                case "3":
                    MenuReservas(reservaService); 
                    break;
                case "0":
                    return; 
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }

    // Submenú para gestionar Autos y switch para cada acción
    static void MenuAutos(AutoService servicio)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=======Gestión de Autos:=======");
            Console.WriteLine("1. Listar Autos");
            Console.WriteLine("2. Agregar Auto");
            Console.WriteLine("3. Actualizar Auto");
            Console.WriteLine("4. Eliminar Auto");
            Console.WriteLine("9. Volver atrás");
            Console.Write("Opción: ");
            string op = Console.ReadLine();

            switch (op)
            {
                case "1": servicio.ListarAutos(); break;
                case "2": servicio.AgregarAuto(); break;
                case "3": servicio.ActualizarAuto(); break;
                case "4": servicio.EliminarAuto(); break;
                case "9": return;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    // Submenú para gestionar Clientes
    static void MenuClientes(ClienteService servicio)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=======Gestión de Clientes:=======");
            Console.WriteLine("1. Lista Cliente");
            Console.WriteLine("2. Agregar Cliente");
            Console.WriteLine("3. Actualizar Cliente");
            Console.WriteLine("4. Eliminar Cliente");
            Console.WriteLine("9. Volver atrás");
            Console.Write("Opción: ");
            string op = Console.ReadLine();

            switch (op)
            {
                case "1": servicio.ListarClientes(); break;
                case "2": servicio.AgregarCliente(); break;
                case "3": servicio.ActualizarCliente(); break;
                case "4": servicio.EliminarCliente(); break;
                case "9": return;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    // Submenú para gestionar Reservas
    static void MenuReservas(ReservaService servicio)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=======Gestión de Reservas:=======");
            Console.WriteLine("1. Lista Reserva");
            Console.WriteLine("2. Hacer Reserva");
            Console.WriteLine("3. Actualizar Reserva");
            Console.WriteLine("4. Eliminar Reserva");
            Console.WriteLine("9. Volver atrás");
            Console.Write("Opción: ");
            string op = Console.ReadLine();

            switch (op)
            {
                case "1": servicio.ListarReservas(); break;
                case "2": servicio.HacerReserva(); break;
                case "3": servicio.ActualizarReserva(); break;
                case "4": servicio.EliminarReserva(); break;
                case "9": return;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }
}
