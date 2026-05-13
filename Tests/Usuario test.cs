using SistemaRecetas.Modelos;
using Xunit;

namespace SistemaRecetas.Tests;

public class UsuarioTests
{
    private readonly Usuario _usuario;

    public UsuarioTests()
    {
        _usuario = new Usuario("Ana");
    }

    [Fact]
    public void CrearLibroRecetas_CreaLibroConListaVacia()
    {
        _usuario.CrearLibroRecetas("Favoritas");

        Assert.True(_usuario.LibrosRecetas.ContainsKey("Favoritas"));
        Assert.Empty(_usuario.LibrosRecetas["Favoritas"]);
    }

    [Fact]
    public void CrearLibroRecetas_Duplicado_LanzaInvalidOperationException()
    {
        _usuario.CrearLibroRecetas("Favoritas");

        Assert.Throws<InvalidOperationException>(() => _usuario.CrearLibroRecetas("Favoritas"));
    }

    [Fact]
    public void AgregarRecetaALibro_AgregaCorrectamente()
    {
        _usuario.CrearLibroRecetas("Favoritas");
        var receta = new Receta("Paella", "Chef Ramírez", 45);

        _usuario.AgregarRecetaALibro("Favoritas", receta);

        Assert.Single(_usuario.LibrosRecetas["Favoritas"]);
    }

    [Fact]
    public void AgregarRecetaALibro_LibroInexistente_LanzaKeyNotFoundException()
    {
        var receta = new Receta("Paella", "Chef Ramírez", 45);

        Assert.Throws<KeyNotFoundException>(() => _usuario.AgregarRecetaALibro("NoExiste", receta));
    }

    [Fact]
    public void ContarRecetas_RetornaTotalCorrecto()
    {
        _usuario.CrearLibroRecetas("Libro1");
        _usuario.CrearLibroRecetas("Libro2");
        _usuario.AgregarRecetaALibro("Libro1", new Receta("Paella", "Chef A", 45));
        _usuario.AgregarRecetaALibro("Libro2", new Receta("Tacos", "Chef B", 30));

        Assert.Equal(2, _usuario.ContarRecetas());
    }

    [Fact]
    public void EliminarLibro_EliminaCorrectamente()
    {
        _usuario.CrearLibroRecetas("Temporal");
        _usuario.EliminarLibro("Temporal");

        Assert.False(_usuario.LibrosRecetas.ContainsKey("Temporal"));
    }

    [Fact]
    public void ObtenerLibro_LibroInexistente_LanzaKeyNotFoundException()
    {
        Assert.Throws<KeyNotFoundException>(() => _usuario.ObtenerLibro("NoExiste"));
    }
}
