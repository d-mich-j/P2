namespace SistemaRecetas.Interfaces;

public interface IReceta
{
    string Nombre { get; }
    string Chef { get; }
    int TiempoMinutos { get; }
    string ToString();
}
