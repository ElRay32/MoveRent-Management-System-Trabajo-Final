using System;
using RentalCar.Models;


namespace RentalCar.interf
{
    class program
    {
        static void Main(string[] args)
        {
            AutoService autoService = new AutoService();
            Auto auto = new Auto(0, "Toyota", "Corolla", "A123BCD", true);
            autoService.AgregarAuto(auto);
            Console.WriteLine("Auto agregado exitosamente.");
        }
    }
}
