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
        public async Task<List<FilmTimes>> GetFilmData(int filmId)
        {
            var filmData = new List<FilmTimes>();
            IDocument document = await _context.OpenAsync($"http://www.odeon.co.uk/showtimes/showtimesByFilmCinema/?date=&siteId=211&filmMasterId={filmId}");
            foreach (IHtmlAnchorElement filmAnchorTag in document.QuerySelectorAll(".content-container.times.containerFUTURE li a").OfType<IHtmlAnchorElement>())
            {
                filmData.Add(new FilmTimes { AuditoriumInfo = filmAnchorTag.Attributes["data-auditorium-info"].Value, Title = filmAnchorTag.Attributes["title"].Value });
            }

            return filmData;
        }

        public async Task<Dictionary<string,int>> GetFilmIds()
        {
            IDocument document = await _context.OpenAsync("http://www.odeon.co.uk");
            Dictionary<string,int> filmIds = new Dictionary<string, int>();
            foreach (IHtmlOptionElement optionElement in document.QuerySelectorAll("#your-film option").OfType<IHtmlOptionElement>())
            {
                if (!optionElement.IsDisabled && optionElement.Value != "0" && !filmIds.ContainsKey(optionElement.Text))
                {
                    filmIds.Add(optionElement.Text,Convert.ToInt32(optionElement.Value));
                }
            }

            return filmIds;
        }
    }
}