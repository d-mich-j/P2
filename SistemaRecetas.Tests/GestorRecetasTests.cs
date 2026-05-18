using SistemaRecetas.Gestores;
using SistemaRecetas.Modelos;
using Xunit;

namespace SistemaRecetas.Tests;

public class GestorRecetasTests
{
    private readonly GestorRecetas _gestor;

    public GestorRecetasTests()
    {
        _gestor = new GestorRecetas();
    }

    [Fact]
    public void AgregarReceta_AumentaConteo()
    {
        var receta = new Receta("Paella Valenciana", "Chef Ramírez", 45);

        _gestor.AgregarReceta(receta);

        Assert.Single(_gestor.RecetasDisponibles);
    }

    [Fact]
    public void AgregarReceta_Duplicado_NoSeAgrega()
    {
        var receta = new Receta("Paella Valenciana", "Chef Ramírez", 45);
        _gestor.AgregarReceta(receta);
        _gestor.AgregarReceta(receta);

        Assert.Single(_gestor.RecetasDisponibles);
    }

    [Fact]
    public void EliminarReceta_DisminuyeConteo()
    {
        var receta = new Receta("Paella", "Chef", 45);
        _gestor.AgregarReceta(receta);

        _gestor.EliminarReceta(receta);

        Assert.Empty(_gestor.RecetasDisponibles);
    }

    [Fact]
    public void BuscarPorNombre_RetornaCoincidenciasParciales()
    {
        _gestor.AgregarReceta(new Receta("Paella Valenciana", "Chef Ramírez", 45));
        _gestor.AgregarReceta(new Receta("Tacos", "Chef B", 30));

        var resultados = _gestor.BuscarPorNombre("paella");

        Assert.Contains(resultados, r => r.Nombre == "Paella Valenciana");
    }

    [Fact]
    public void BuscarPorNombre_SinCoincidencias_RetornaListaVacia()
    {
        _gestor.AgregarReceta(new Receta("Paella", "Chef", 45));

        var resultados = _gestor.BuscarPorNombre("sushi");

        Assert.Empty(resultados);
    }

    [Fact]
    public void QuickSort_OrdenaPorTiempoAscendente()
    {
        _gestor.AgregarReceta(new Receta("Receta C", "Chef", 90));
        _gestor.AgregarReceta(new Receta("Receta A", "Chef", 10));
        _gestor.AgregarReceta(new Receta("Receta B", "Chef", 45));

        _gestor.QuickSort(_gestor.RecetasDisponibles);
        var lista = _gestor.RecetasDisponibles;

        for (int i = 0; i < lista.Count - 1; i++)
            Assert.True(lista[i].TiempoMinutos <= lista[i + 1].TiempoMinutos);
    }

    [Fact]
    public void MergeSort_RetornaListaOrdenada_SinModificarOriginal()
    {
        var recetaA = new Receta("Receta A", "Chef", 90);
        var recetaB = new Receta("Receta B", "Chef", 10);
        _gestor.AgregarReceta(recetaA);
        _gestor.AgregarReceta(recetaB);

        var original = new List<Receta>(_gestor.RecetasDisponibles);
        var ordenada = _gestor.MergeSort(_gestor.RecetasDisponibles);

        for (int i = 0; i < ordenada.Count - 1; i++)
            Assert.True(ordenada[i].TiempoMinutos <= ordenada[i + 1].TiempoMinutos);

        Assert.Equal(original[0], _gestor.RecetasDisponibles[0]);
        Assert.Equal(original[1], _gestor.RecetasDisponibles[1]);
    }

    [Fact]
    public void BusquedaBinaria_RetornaIndiceCorrectoSiExiste()
    {
        _gestor.AgregarReceta(new Receta("Paella Valenciana", "Chef Ramírez", 45));
        _gestor.AgregarReceta(new Receta("Tacos", "Chef B", 30));

        int indice = _gestor.BusquedaBinaria("Paella Valenciana");

        Assert.True(indice >= 0);
    }

    [Fact]
    public void BusquedaBinaria_RetornaMenos1SiNoExiste()
    {
        _gestor.AgregarReceta(new Receta("Paella", "Chef", 45));

        int indice = _gestor.BusquedaBinaria("RecetaXYZInexistente");

        Assert.Equal(-1, indice);
    }

    [Fact]
    public void BusquedaBinaria_EsCaseInsensitive()
    {
        _gestor.AgregarReceta(new Receta("Paella Valenciana", "Chef Ramírez", 45));

        int indice = _gestor.BusquedaBinaria("paella valenciana");

        Assert.True(indice >= 0);
    }

    [Fact]
    public void LimpiarCatalogo_VaciaLaLista()
    {
        _gestor.AgregarReceta(new Receta("Paella", "Chef", 45));
        _gestor.LimpiarCatalogo();

        Assert.Empty(_gestor.RecetasDisponibles);
    }
}
