using ImaxBot.Core;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ImaxBot.IntegrationTests
{
    public class AngleSharpClientTests
    {
        [Fact]
        public async Task GetFilmIds_should_return_more_then_zero_films()
        {
            var filmIds = await new AngleSharpClient().GetFilmIds();

            Assert.NotEmpty(filmIds);
        }
    }
}
