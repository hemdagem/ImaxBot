using System.Collections.Generic;

namespace ImaxBot.Core
{
    public interface IFilmService
    {
        List<FilmTimes> GetFilmTimes(string filmName);
    }
}