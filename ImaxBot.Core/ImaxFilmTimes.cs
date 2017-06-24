using System;
using System.Collections.Generic;

namespace ImaxBot.Core
{
    public class ImaxFilmTimes
    {
        readonly IFilmService filmService;
        public ImaxFilmTimes(IFilmService iFilmService)
        {
            filmService = iFilmService;
        }

        public List<FilmTimes> GetFilmTimes(string filmName)
        {
            if (string.IsNullOrEmpty(filmName))
            {
                throw new NullReferenceException();
            }
            return filmService.GetFilmTimes(filmName);
        }
    }
}
