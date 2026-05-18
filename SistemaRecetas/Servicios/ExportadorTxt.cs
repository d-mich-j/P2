using SistemaRecetas.Interfaces;
using SistemaRecetas.Modelos;

namespace SistemaRecetas.Servicios;

public class ExportadorTxt : IExportador
{
    public void ExportarATxt(Usuario usuario, string rutaArchivo)
    {
        using var escritor = new StreamWriter(rutaArchivo, append: false);

        escritor.WriteLine($"=== Libros de Recetas de {usuario.Nombre} ===");
        escritor.WriteLine($"Fecha de exportación: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
        escritor.WriteLine();

        foreach (var kvp in usuario.LibrosRecetas)
        {
            escritor.WriteLine($"--- Libro: {kvp.Key} ---");

            if (kvp.Value.Count == 0)
            {
                escritor.WriteLine("  (vacío)");
            }
            else
            {
                foreach (var receta in kvp.Value)
                    escritor.WriteLine($"  * {receta}");
            }

            escritor.WriteLine();
        }
    }
}
