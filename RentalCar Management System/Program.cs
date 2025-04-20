using RentalCar.BLL;
using RentalCar.Models;
using RentalCar.Service;


namespace RentalCar
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoService autoService = new AutoService();
            ReservaService reservaService = new ReservaService();
            PagoService pagoService = new PagoService();

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ RENTALCAR ===");
                Console.WriteLine("1. Agregar Auto");
                Console.WriteLine("2. Listar Autos Disponibles");
                Console.WriteLine("3. Hacer Reserva");
                Console.WriteLine("4. Registrar Pago");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        autoService.AgregarAuto();
                        break;
                    case "2":
                        autoService.ListarAutos();
                        break;
                    case "3":
                        reservaService.HacerReserva();
                        break;
                    case "4":
                        pagoService.RegistrarPago();
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
