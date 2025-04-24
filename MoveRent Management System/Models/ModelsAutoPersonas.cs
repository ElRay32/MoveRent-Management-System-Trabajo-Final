using Microsoft.Data.SqlClient;

namespace MoveRent.Models
{
    // Clase que representa un automóvil disponible para alquilar
    public class ModelsAutoPersonas
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public int Año { get; set; }
        public string Color { get; set; }
        public bool Disponible { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        // Constructor que inicializa todas las propiedades del auto
        public ModelsAutoPersonas(int id, string marca, string modelo, string placa, int año, string color, bool disponible, DateTime fechacreacion, DateTime fechamodificacion)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            Año = año;
            Color = color;
            Disponible = disponible;
            FechaCreacion = fechacreacion;
            FechaModificacion = fechamodificacion;
        }

        public string ObtenerInfo() => $"{Marca} {Modelo} ({Placa})";

        public string ObtenerInfo(bool mostrarDisponibilidad) => $"{Marca} {Modelo} ({Placa}) - Disponible: {Disponible}";
    }

    // Clase abstracta que representa una persona en el sistema y Propiedades comunes a todas las personas
    public abstract class Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public int Edad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        // Constructor base para inicializar una persona
        public Persona(string nombre, string apellido, string cedula, int edad, DateTime fechaCreacion, DateTime fechaModificacion)
        {
            Nombre = nombre;
            Apellido = apellido;
            Cedula = cedula;
            Edad = edad;
            FechaCreacion = fechaCreacion;
            FechaModificacion = fechaModificacion;
        }

        // Método abstracto que debe ser implementado por las clases hijas
        public abstract void MostrarInfo();
    }

    // Clase Cliente que hereda de Persona y agrega un campo de Teléfono. Propiedad adicional específica del cliente
    public class Cliente : Persona
    {
        public int Id { get; set; }
        public string Telefono { get; set; }

        // Constructor que inicializa los campos del cliente y llama al constructor base
        public Cliente(string nombre, string apellido, string cedula, int edad, string telefono, DateTime fechaCreacion, DateTime fechaModificacion)
            : base(nombre, apellido, cedula, edad, fechaCreacion, fechaModificacion)
        {
            Telefono = telefono;
        }

        // Implementación del método abstracto MostrarInfo
        public override void MostrarInfo()
        {
            Console.WriteLine($"Cliente: {Nombre} - {Cedula} - {Telefono}");
        }
    }

    // Interface que define métodos relacionados con reservas
    public interface IReservable
    {
        void Reservar();   // Método para crear una reserva
        void Cancelar();   // Método para cancelar una reserva
    }

    // Clase Reserva que implementa la interface IReservable
    public class Reserva : IReservable
    {
        // Propiedades que identifican la reserva
        public int IdReserva { get; set; }
        public int IdAuto { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaReserva { get; set; }

        public void Reservar() => Console.WriteLine("Reserva realizada.");
 
        public void Cancelar() => Console.WriteLine("Reserva cancelada.");
    }
}
