using SistemaRecetas.Interfaces;

namespace SistemaRecetas.Modelos;////ooooo

public class Receta : IReceta
{
    public string Nombre { get; }
    public string Chef { get; }
    public int TiempoMinutos { get; }

    public Receta(string nombre, string chef, int tiempoMinutos)
    {
        if (tiempoMinutos <= 0)
            throw new ArgumentException("El tiempo de preparación debe ser mayor que 0.", nameof(tiempoMinutos));

        Nombre = nombre;
        Chef = chef;
        TiempoMinutos = tiempoMinutos;
    }

    public override string ToString()
    {
        return $"{Nombre} - {Chef} ({TiempoMinutos} min)";
    }
}
