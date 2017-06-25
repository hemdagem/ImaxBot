using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImaxBot.Core
{
    public interface IFilmFinder
    {
        Task<FilmInformation> Find(string filmName);
        Task<List<FilmTimes>> GetFilmDetails(int filmId);
    }
}