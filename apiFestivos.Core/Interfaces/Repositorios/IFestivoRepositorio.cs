using apiFestivos.Dominio.DTOs;
using apiFestivos.Dominio.Entidades;

namespace apiFestivos.Core.Interfaces.Repositorios
{
    public interface IFestivoRepositorio
    {
        Task<IEnumerable<Festivo>> ObtenerTodos();

        Task<Festivo> Obtener(int Id);

        Task<IEnumerable<Festivo>> Buscar(string Dato);

        Task<Festivo> Agregar(Festivo Festivo);

        Task<Festivo> Modificar(Festivo Festivo);

        Task<bool> Eliminar(int Id);

        //********** Consultas //**********

        Task<IEnumerable<FechaFestivo>> ObtenerFecha(String Nombre);

        Task<bool> EsFestivo(DateTime Fecha);

        Task<IEnumerable<FechaFestivo>> ObtenerAño(int Año);


    }
}
