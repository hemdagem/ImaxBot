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
                filmData.Add(new FilmTimes
                {
                    AuditoriumInfo = GetAttributeValue(filmAnchorTag.Attributes,"data-auditorium-info"),
                    Title = GetAttributeValue(filmAnchorTag.Attributes, "title")
                });
            }

            return filmData;
        }


        private static string GetAttributeValue(INamedNodeMap nodeMap, string attribute)
        {
            var attributeValue = nodeMap[attribute];

            return attributeValue == null ? "N/A" : attributeValue.Value;
        }
    }
}