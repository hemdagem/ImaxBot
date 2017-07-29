using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;

namespace ImaxBot.Core.FilmFinder
{
    public class FilmInformationFactory
    {
        public static List<FilmInformation> Create(IDocument document)
        {
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