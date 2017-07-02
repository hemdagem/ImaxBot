using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;

namespace ImaxBot.Core
{
    public class FilmTimesFactory
    {
        public static List<FilmTimes> Create(IDocument document)
        {
            var filmData = new List<FilmTimes>();
            foreach (IHtmlAnchorElement filmAnchorTag in document.QuerySelectorAll(".performance-detail").OfType<IHtmlAnchorElement>())
            {
                filmData.Add(new FilmTimes { AuditoriumInfo = filmAnchorTag.Attributes["data-auditorium-info"].Value, Title = filmAnchorTag.Attributes["title"].Value });
            }

            return filmData;
        }
    }
}