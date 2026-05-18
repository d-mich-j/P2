using SistemaRecetas.Interfaces;
using SistemaRecetas.Modelos;

namespace SistemaRecetas.Servicios;

public class ServicioRecetas
{
    public IGestorRecetas Gestor { get; }
    public IExportador Exportador { get; }
    public List<Usuario> Usuarios { get; }

    public ServicioRecetas(IGestorRecetas gestor, IExportador exportador)
    {
        Gestor = gestor;
        Exportador = exportador;
        Usuarios = new List<Usuario>();
    }

    public Usuario RegistrarUsuario(string nombre)
    {
        var usuario = new Usuario(nombre);
        Usuarios.Add(usuario);
        return usuario;
    }

    public Usuario? BuscarUsuario(string nombre)
    {
        return Usuarios.FirstOrDefault(u =>
            u.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
    }

    public bool EliminarUsuario(string nombre)
    {
        var usuario = BuscarUsuario(nombre);
        if (usuario == null)
            return false;

        Usuarios.Remove(usuario);
        return true;
    }

    public int ContarUsuarios()
    {
        return Usuarios.Count;
    }

    public void OrdenarCatalogo(string algoritmo)
    {
        switch (algoritmo.ToLower())
        {
            case "quick":
                Gestor.QuickSort(Gestor.RecetasDisponibles);
                Console.WriteLine("Catálogo ordenado con QuickSort por tiempo de preparación.");
                break;
            case "merge":
                var ordenadas = Gestor.MergeSort(Gestor.RecetasDisponibles);
                Gestor.LimpiarCatalogo();
                foreach (var r in ordenadas)
                    Gestor.AgregarReceta(r);
                Console.WriteLine("Catálogo ordenado con MergeSort por tiempo de preparación.");
                break;
            default:
                Console.WriteLine("Algoritmo no reconocido. Use 'quick' o 'merge'.");
                break;
        }
    }

    public int OrdenarLibroYCalcularTiempo(Usuario usuario, string nombreLibro)
    {
        var libro = usuario.ObtenerLibro(nombreLibro);
        var ordenado = Gestor.MergeSort(libro);

        libro.Clear();
        libro.AddRange(ordenado);

        return libro.Sum(r => r.TiempoMinutos);
    }
}
