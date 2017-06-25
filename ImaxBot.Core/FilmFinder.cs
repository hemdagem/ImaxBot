using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImaxBot.Core
{
    public class FilmFinder : IFilmFinder
    {
        private readonly IAngleSharpClient _client;
        public FilmFinder(IAngleSharpClient client)
        {
            _client = client;
        }
        public async Task<FilmInformation> Find(string filmName)
        {
            if (string.IsNullOrEmpty(filmName)) return null;

            List<FilmInformation> films = await _client.GetFilmIds();

            FilmInformation film = films.FirstOrDefault(x =>
            {
                string filmNameLowerCase = filmName.ToLower();
                return x.FilmName.ToLower() == filmNameLowerCase ||
                       x.FilmName.ToLower().StartsWith(filmNameLowerCase);
            });

            return film;
        }

        public async Task<List<FilmTimes>> GetFilmDetails(int filmId)
        {
            if (filmId < 1) return null;
            return await _client.GetFilmData(filmId);
        }
    }
}