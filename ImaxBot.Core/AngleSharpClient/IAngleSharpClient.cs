using System.Collections.Generic;
using System.Threading.Tasks;
using ImaxBot.Core.FilmFinder;

namespace ImaxBot.Core.AngleSharpClient
{
    public interface IAngleSharpClient
    {
        Task<List<FilmTimes>> GetFilmData(int filmId);
        Task<List<FilmInformation>> GetFilmIds();
    }
}