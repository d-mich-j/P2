using SistemaRecetas.Interfaces;
using SistemaRecetas.Modelos;

namespace SistemaRecetas.Gestores;

public class GestorRecetas : IGestorRecetas
{
    public List<Receta> RecetasDisponibles { get; }

    public GestorRecetas()
    {
        RecetasDisponibles = new List<Receta>();
    }

    public void AgregarReceta(Receta receta)
    {
        if (!RecetasDisponibles.Contains(receta))
            RecetasDisponibles.Add(receta);
    }

    public void EliminarReceta(Receta receta)
    {
        if (RecetasDisponibles.Contains(receta))
            RecetasDisponibles.Remove(receta);
    }

    public void EliminarPorIndice(int indice)
    {
        if (indice >= 0 && indice < RecetasDisponibles.Count)
            RecetasDisponibles.RemoveAt(indice);
    }

    public List<Receta> BuscarPorNombre(string nombre)
    {
        return RecetasDisponibles
            .Where(r => r.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void LimpiarCatalogo()
    {
        RecetasDisponibles.Clear();
    }

    public void QuickSort(List<Receta> lista)
    {
        if (lista.Count <= 1)
            return;

        QuickSortInterno(lista, 0, lista.Count - 1);
    }

    private void QuickSortInterno(List<Receta> lista, int izquierda, int derecha)
    {
        if (izquierda >= derecha)
            return;

        int pivotIndex = Particionar(lista, izquierda, derecha);
        QuickSortInterno(lista, izquierda, pivotIndex - 1);
        QuickSortInterno(lista, pivotIndex + 1, derecha);
    }

    private int Particionar(List<Receta> lista, int izquierda, int derecha)
    {
        int pivot = lista[derecha].TiempoMinutos;
        int i = izquierda - 1;

        for (int j = izquierda; j < derecha; j++)
        {
            if (lista[j].TiempoMinutos <= pivot)
            {
                i++;
                (lista[i], lista[j]) = (lista[j], lista[i]);
            }
        }

        (lista[i + 1], lista[derecha]) = (lista[derecha], lista[i + 1]);
        return i + 1;
    }

    public List<Receta> MergeSort(List<Receta> lista)
    {
        if (lista.Count <= 1)
            return new List<Receta>(lista);

        int medio = lista.Count / 2;
        var izquierda = MergeSort(lista.GetRange(0, medio));
        var derecha = MergeSort(lista.GetRange(medio, lista.Count - medio));

        return Merge(izquierda, derecha);
    }

    private List<Receta> Merge(List<Receta> izquierda, List<Receta> derecha)
    {
        var resultado = new List<Receta>();
        int i = 0, j = 0;

        while (i < izquierda.Count && j < derecha.Count)
        {
            if (izquierda[i].TiempoMinutos <= derecha[j].TiempoMinutos)
                resultado.Add(izquierda[i++]);
            else
                resultado.Add(derecha[j++]);
        }

        while (i < izquierda.Count)
            resultado.Add(izquierda[i++]);

        while (j < derecha.Count)
            resultado.Add(derecha[j++]);

        return resultado;
    }

    public int BusquedaBinaria(string nombre)
    {
        var copia = RecetasDisponibles
            .OrderBy(r => r.Nombre, StringComparer.OrdinalIgnoreCase)
            .ToList();

        int izquierda = 0, derecha = copia.Count - 1;

        while (izquierda <= derecha)
        {
            int medio = (izquierda + derecha) / 2;
            int comparacion = string.Compare(copia[medio].Nombre, nombre, StringComparison.OrdinalIgnoreCase);

            if (comparacion == 0)
                return medio;
            else if (comparacion < 0)
                izquierda = medio + 1;
            else
                derecha = medio - 1;
        }

        return -1;
    }
}
