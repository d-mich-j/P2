Using System;
namespace SistemaRecetas.Modelos;

public class Usuario
{
    public string Nombre { get; }
    public Dictionary<string, List<Receta>> LibrosRecetas { get; }

    public Usuario(string nombre)
    {
        Nombre = nombre;
        LibrosRecetas = new Dictionary<string, List<Receta>>();
    }

    public void CrearLibroRecetas(string nombreLibro)
    {
        if (LibrosRecetas.ContainsKey(nombreLibro))
            throw new InvalidOperationException($"El libro '{nombreLibro}' ya existe.");

        LibrosRecetas[nombreLibro] = new List<Receta>();
    }

    public void AgregarRecetaALibro(string nombreLibro, Receta receta)
    {
        if (!LibrosRecetas.TryGetValue(nombreLibro, out var lista))
            throw new KeyNotFoundException($"El libro '{nombreLibro}' no existe.");

        lista.Add(receta);
    }

    public void EliminarLibro(string nombreLibro)
    {
        LibrosRecetas.Remove(nombreLibro);
    }

    public List<Receta> ObtenerLibro(string nombreLibro)
    {
        if (!LibrosRecetas.TryGetValue(nombreLibro, out var lista))
            throw new KeyNotFoundException($"El libro '{nombreLibro}' no existe.");

        return lista;
    }

    public int ContarRecetas()
    {
        int total = 0;
        foreach (var lista in LibrosRecetas.Values)
            total += lista.Count;
        return total;
    }

    public void MostrarLibros()
    {
        if (LibrosRecetas.Count == 0)
        {
            Console.WriteLine("No tienes libros de recetas.");
            return;
        }

        foreach (var kvp in LibrosRecetas)
        {
            Console.WriteLine($"\n📖 Libro: {kvp.Key}");
            if (kvp.Value.Count == 0)
            {
                Console.WriteLine("  (vacío)");
            }
            else
            {
                foreach (var receta in kvp.Value)
                    Console.WriteLine($"  - {receta}");
            }
        }
    }
}
