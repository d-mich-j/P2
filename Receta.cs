using System;

public class Receta 
{
    public string Nombre { get; }
    public string Chef { get; }
    public int TiempoMinutos { get; }
    

    public Receta(string nombre, string chef, int tiempoMinuto) {
        Nombre = nombre;
        Chef = chef;
        
    }
    
}
