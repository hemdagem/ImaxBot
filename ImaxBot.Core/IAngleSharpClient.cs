using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImaxBot.Core
{
    public interface IAngleSharpClient
    {
        Task<List<FilmTimes>> GetFilmData(int filmId);
        Task<List<FilmInformation>> GetFilmIds();
    }
}