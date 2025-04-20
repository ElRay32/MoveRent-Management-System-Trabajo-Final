using RentalCar.BLL;
using RentalCar.Models;


namespace RentalCar
{
    class program
    {
        static void Main(string[] args)
        {
            AutoService autoService = new AutoService();
            ModelsAutoPersonas auto = new ModelsAutoPersonas(0, "Toyota", "Corolla", "A123BCD", true);
            autoService.AgregarAuto(auto);
            Console.WriteLine("Auto agregado exitosamente.");
        }
    }
}
