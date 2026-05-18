using SistemaRecetas.Gestores;
using SistemaRecetas.Modelos;
using SistemaRecetas.Servicios;
using Xunit;

namespace SistemaRecetas.Tests;

public class ServicioRecetasTests
{
    private readonly ServicioRecetas _servicio;
    private readonly GestorRecetas _gestor;
    private readonly ExportadorTxt _exportador;

    public ServicioRecetasTests()
    {
        _gestor = new GestorRecetas();
        _exportador = new ExportadorTxt();
        _servicio = new ServicioRecetas(_gestor, _exportador);
    }

    [Fact]
    public void RegistrarUsuario_AgregaUsuarioALaLista()
    {
        _servicio.RegistrarUsuario("TestUser");

        Assert.NotNull(_servicio.BuscarUsuario("TestUser"));
        Assert.Equal(1, _servicio.ContarUsuarios());
    }

    [Fact]
    public void BuscarUsuario_RetornaUsuarioCorrecto_CaseInsensitive()
    {
        _servicio.RegistrarUsuario("Ana");

        Assert.NotNull(_servicio.BuscarUsuario("ANA"));
        Assert.NotNull(_servicio.BuscarUsuario("ana"));
        Assert.NotNull(_servicio.BuscarUsuario("Ana"));
    }

    [Fact]
    public void BuscarUsuario_RetornaNull_SiNoExiste()
    {
        Assert.Null(_servicio.BuscarUsuario("Inexistente"));
    }

    [Fact]
    public void EliminarUsuario_RetornaTrue_SiExiste()
    {
        _servicio.RegistrarUsuario("Carlos");

        bool resultado = _servicio.EliminarUsuario("Carlos");

        Assert.True(resultado);
        Assert.Equal(0, _servicio.ContarUsuarios());
    }

    [Fact]
    public void EliminarUsuario_RetornaFalse_SiNoExiste()
    {
        bool resultado = _servicio.EliminarUsuario("NoExiste");

        Assert.False(resultado);
    }

    [Fact]
    public void ExportarLibros_CreaArchivoConContenido()
    {
        var usuario = _servicio.RegistrarUsuario("PruebaExport");
        usuario.CrearLibroRecetas("Favoritas");
        usuario.AgregarRecetaALibro("Favoritas", new Receta("Paella", "Chef Ramírez", 45));

        string ruta = Path.GetTempFileName();
        try
        {
            _servicio.Exportador.ExportarATxt(usuario, ruta);

            Assert.True(File.Exists(ruta));
            string contenido = File.ReadAllText(ruta);
            Assert.Contains("Paella", contenido);
            Assert.Contains("PruebaExport", contenido);
        }
        finally
        {
            if (File.Exists(ruta))
                File.Delete(ruta);
        }
    }

    [Fact]
    public void ContarUsuarios_RetornaCantidadCorrecta()
    {
        _servicio.RegistrarUsuario("Usuario1");
        _servicio.RegistrarUsuario("Usuario2");
        _servicio.RegistrarUsuario("Usuario3");

        Assert.Equal(3, _servicio.ContarUsuarios());
    }
}
