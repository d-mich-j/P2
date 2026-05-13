using SistemaRecetas.Modelos;
using Xunit;

namespace SistemaRecetas.Tests;

public class RecetaTests
{
    [Fact]
    public void Constructor_AsignaPropiedadesCorrectamente()
    {
        var receta = new Receta("Paella", "Chef Ramírez", 45);

        Assert.Equal("Paella", receta.Nombre);
        Assert.Equal("Chef Ramírez", receta.Chef);
        Assert.Equal(45, receta.TiempoMinutos);
    }

    [Fact]
    public void ToString_RetornaFormatoCorrecto()
    {
        var receta = new Receta("Paella", "Chef Ramírez", 45);

        Assert.Equal("Paella - Chef Ramírez (45 min)", receta.ToString());
    }

    [Fact]
    public void Constructor_TiempoNegativo_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Receta("Test", "Chef", -1));
    }

    [Fact]
    public void Constructor_TiempoCero_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Receta("Test", "Chef", 0));
    }
}﻿
