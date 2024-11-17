
using Xunit;
using Moq;
using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Core.Interfaces.Servicios;
using apiFestivos.Dominio.Entidades;
using System.Threading.Tasks;
using System;

//Jhon Fernando Sanchez Alvarez

public class FestivoServicioTest
{
    private readonly Mock<IFestivoRepositorio> _repositorioMock;
    private readonly IFestivoServicio _servicio;

    public FestivoServicioTest()
    {
        _repositorioMock = new Mock<IFestivoRepositorio>();
        _servicio = new FestivoServicio(_repositorioMock.Object);
    }
    //Pruebas Unitarias para el método EsFestivo()
    //• Verificar que, si la fecha coincide con una festiva, EsFestivo devuelve true (Caso Positivo).
    //• Probar una fecha que no esté en la lista de festivos y verificar que el resultado sea false.
    [Fact]
    public async Task EsFestivo_FechaFestiva_RetornaTrue()
    {
        
        var idFestivo = 1;
        var fechaFestiva = new DateTime(2024, 12, 25);

       
        var festivosDelAño = new List<Festivo>
        {
        new Festivo
            {
            Id = idFestivo, 
            Nombre = "Navidad", 
            Dia = 25, 
            Mes = 12, 
            IdTipo = 1,
            DiasPascua = 0
            }
        };
        _repositorioMock.Setup(repositorio => repositorio.ObtenerTodos()).ReturnsAsync(festivosDelAño);
        
        var resultado2 = await _servicio.EsFestivo(fechaFestiva);


       
        Assert.True(resultado2);

        
    }


    [Fact]
    public async Task EsFestivo_FechaNoFestiva_RetornaFalse()
    {
        
        var idFestivo2 = 5;
        var fechaFestiva2 = new DateTime(2024, 12, 26);

        
        var festivosDelAño2 = new List<Festivo>
        {
        new Festivo
            {
            Id = idFestivo2,
            Nombre = "Navidad",
            Dia = 25,
            Mes = 12,
            IdTipo = 1,
            DiasPascua = 0
            }
        };
        _repositorioMock.Setup(repositorio => repositorio.ObtenerTodos()).ReturnsAsync(festivosDelAño2);
       
        var resultado = await _servicio.EsFestivo(fechaFestiva2);

        Assert.False(resultado);

        
    }
    //• Verificar que, al dar un festivo con tipo 1, se retorne la fecha esperada.
    [Fact]
    public async Task ObtenerFecha_Tipo1()
    {
        
        var idFestivo2 = 5;
        
        var fechaFestiva2 = new DateTime(2024, 1, 1);

        var festivosDelAño2 = new List<Festivo>
        {
        new Festivo
            {
            Id = idFestivo2,
            Nombre = "Año Nuevo",
            Dia = 1,
            Mes = 1,
            IdTipo = 1,
            DiasPascua = 0
            }
        }.AsEnumerable();

        _repositorioMock.Setup(repositorio => repositorio.ObtenerTodos()).ReturnsAsync(festivosDelAño2);
        
        var resultado = await _servicio.ObtenerAño(fechaFestiva2.Year);

        Assert.Equal(resultado.ToList()[0].Nombre, festivosDelAño2.ToList()[0].Nombre);
        Assert.Equal(resultado.ToList()[0].Fecha, fechaFestiva2);
    }
    //Probar que un festivo movible (tipo 2) caiga en el lunes siguiente a la fecha inicial.
    [Fact]
    public async Task ObtenerFecha_Tipo2()
    {

        var idFestivo2 = 5;

        var fechaFestiva2 = new DateTime(2024, 1, 8);

        var festivosDelAño2 = new List<Festivo>
        {
        new Festivo
            {
            Id = idFestivo2,
            Nombre = "Año Nuevo",
            Dia = 8,
            Mes = 1,
            IdTipo = 2,
            DiasPascua = 0
            }
        }.AsEnumerable();

        _repositorioMock.Setup(repositorio => repositorio.ObtenerTodos()).ReturnsAsync(festivosDelAño2);

        var resultado = await _servicio.ObtenerAño(fechaFestiva2.Year);

        Assert.Equal(resultado.ToList()[0].Nombre, festivosDelAño2.ToList()[0].Nombre);
        Assert.Equal(resultado.ToList()[0].Fecha, fechaFestiva2);
    }
    //Verificar un festivo que se desplaza a lunes basado en una fecha relativa a Semana Santa(tipo 4).
    [Fact]
    public async Task ObtenerFecha_Tipo4()
    {

        var idFestivo2 = 5;

        var fechaFestiva2 = new DateTime(2024, 5, 27);

        var festivosDelAño2 = new List<Festivo>
        {
        new Festivo
            {
            Id = idFestivo2,
            Nombre = "Corpus Christi",
            Dia = 27,
            Mes = 5,
            IdTipo = 4,
            DiasPascua = 61
            }
        }.AsEnumerable();

        _repositorioMock.Setup(repositorio => repositorio.ObtenerTodos()).ReturnsAsync(festivosDelAño2);

        var resultado = await _servicio.ObtenerAño(fechaFestiva2.Year);

        Assert.Equal(resultado.ToList()[0].Nombre, festivosDelAño2.ToList()[0].Nombre);
        Assert.Equal(resultado.ToList()[0].Fecha, fechaFestiva2);
    }


}
