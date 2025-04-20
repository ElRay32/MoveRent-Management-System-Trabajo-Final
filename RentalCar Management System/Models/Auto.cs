namespace RentalCar.Models
{
    // Clase que representa un automóvil disponible para alquilar
    public class Auto
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public bool Disponible { get; set; }

        // Constructor de la clase Auto
        public Auto(int id, string marca, string modelo, string placa, bool disponible)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            Disponible = disponible;
        }
        // Método sobrecargado para obtener información del auto
        public string ObtenerInfo() => $"{Marca} {Modelo} ({Placa})";
        public string ObtenerInfo(bool mostrarDisponibilidad) => $"{Marca} {Modelo} ({Placa}) - Disponible: {Disponible}";
    }

    // Clase abstracta base para representar personas
    public abstract class Persona
    {
        public string Nombre { get; set; }
        public string Cedula { get; set; }

        public Persona(string nombre, string cedula)
        {
            Nombre = nombre;
            Cedula = cedula;
        }

        // Método abstracto que será implementado por las clases hijas
        public abstract void MostrarInfo();
    }

    // Clase Cliente que hereda de Persona
    public class Cliente : Persona
    {
        public string Telefono { get; set; }

        public Cliente(string nombre, string cedula, string telefono)
            : base(nombre, cedula)
        {
            Telefono = telefono;
        }

        // Implementación del método MostrarInfo
        public override void MostrarInfo()
        {
            Console.WriteLine($"Cliente: {Nombre} - {Cedula} - {Telefono}");
        }

    }

    // Interface que define métodos relacionados a reservas
    public interface IReservable
    {
        void Reservar();
        void Cancelar();
    }

    // Clase Reserva que implementa la interface IReservable
    public class Reserva : IReservable
    {
        public int IdReserva { get; set; }
        public int IdAuto { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaReserva { get; set; }

        public void Reservar() => Console.WriteLine("Reserva realizada.");
        public void Cancelar() => Console.WriteLine("Reserva cancelada.");
    }

}