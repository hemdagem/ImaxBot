using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using ImaxBot.Core.FilmFinder;

namespace ImaxBot.Core.AngleSharpClient
{

    public class AngleSharpClient : IAngleSharpClient
    {
        private readonly IBrowsingContext _context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
        private const string ImaxSite = "http://www.odeon.co.uk";

        public async Task<List<FilmTimes>> GetFilmData(int filmId)
        {
            using (IDocument document = await _context.OpenAsync($"{ImaxSite}/showtimes/showtimesByFilmCinema/?siteId=211&filmMasterId={filmId}"))
            {
                return FilmTimesFactory.Create(document);
            }
        }

        public async Task<List<FilmInformation>> GetFilmIds()
        {
            using (IDocument document = await _context.OpenAsync(ImaxSite))
            {
                return FilmInformationFactory.Create(document);
            }
        }
    }
}