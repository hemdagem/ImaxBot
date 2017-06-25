using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;

namespace ImaxBot.Core
{
    public class AngleSharpClient : IAngleSharpClient
    {
        private readonly IBrowsingContext _context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
        private readonly string _imaxSite = "http://www.odeon.co.uk";
        public async Task<List<FilmTimes>> GetFilmData(int filmId)
        {
            var filmData = new List<FilmTimes>();
            IDocument document = await _context.OpenAsync($"{_imaxSite}/showtimes/showtimesByFilmCinema/?siteId=211&filmMasterId={filmId}");
            foreach (IHtmlAnchorElement filmAnchorTag in document.QuerySelectorAll(".performance-detail").OfType<IHtmlAnchorElement>())
            {
                filmData.Add(new FilmTimes { AuditoriumInfo = filmAnchorTag.Attributes["data-auditorium-info"].Value, Title = filmAnchorTag.Attributes["title"].Value });
            }

            return filmData;
        }

        public async Task<List<FilmInformation>> GetFilmIds()
        {
            IDocument document = await _context.OpenAsync(_imaxSite);
            List<FilmInformation> filmIds = new List<FilmInformation>();
            foreach (IHtmlOptionElement optionElement in document.QuerySelectorAll("#your-film option").OfType<IHtmlOptionElement>())
            {
                if (optionElement.IsDisabled) continue;
                int filmId = Convert.ToInt32(optionElement.Value);
                if (filmId != 0 && !filmIds.Exists(x => x.FilmId == filmId))
                {
                    filmIds.Add(new FilmInformation { FilmId = filmId, FilmName = optionElement.Text });
                }
            }

            return filmIds;
        }
    }
}