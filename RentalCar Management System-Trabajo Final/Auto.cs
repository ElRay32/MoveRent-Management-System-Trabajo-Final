namespace RentalCar.Models
{  
    // Clase que representa un automóvil disponible para alquilar
    internal class Auto
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

        public abstract void MostrarInfo();
    }

}
