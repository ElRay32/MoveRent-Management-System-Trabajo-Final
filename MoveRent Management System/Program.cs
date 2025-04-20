using RentalCar.BLL;
using Microsoft.Data.SqlClient;
using RentalCar.Models;
using RentalCar.Service;


namespace RentalCar
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoService autoService = new AutoService();
            ClienteService clienteService = new ClienteService();
            ReservaService reservaService = new ReservaService();
            PagoService pagoService = new PagoService();

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ RENTALCAR DE MOVERENT ===");
                Console.WriteLine("1. Agregar Auto");
                Console.WriteLine("2. Listar Autos Disponibles");
                Console.WriteLine("3. Agregar Cliente");
                Console.WriteLine("4. Listar Clientes");
                Console.WriteLine("5. Hacer Reserva");
                Console.WriteLine("6. Listar Reservas");
                Console.WriteLine("7. Registrar Pago");
                Console.WriteLine("8. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": autoService.AgregarAuto(); break;
                    case "2": autoService.ListarAutos(); break;
                    case "3": clienteService.AgregarCliente(); break;
                    case "4": clienteService.ListarClientes(); break;
                    case "5": reservaService.HacerReserva(); break;
                    case "6": reservaService.ListarReservas(); break;
                    case "7": pagoService.RegistrarPago(); break;
                    case "8": salir = true; break;
                    default:
                        Console.WriteLine("Opción inválida. Presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
