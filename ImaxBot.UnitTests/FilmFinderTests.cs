using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using ImaxBot.Core;
using Moq;
using Xunit;

namespace ImaxBot.UnitTests
{
    public class FilmFinderTests
    {
        private readonly FilmFinder _filmFinder;
        private readonly Mock<IAngleSharpClient> _angleSharpClientMock;
        public FilmFinderTests()
        {
            _angleSharpClientMock = new Mock<IAngleSharpClient>();
            _filmFinder = new FilmFinder(_angleSharpClientMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void Find_should_return_null_when_filmNameIsEmpty(string filmName)
        {
            Assert.Null(await _filmFinder.Find(filmName));
        }

        [Fact]
        public async void Find_should_return_null_when_film_is_not_available_in_list()
        {
            _angleSharpClientMock.Setup(x => x.GetFilmIds())
                .ReturnsAsync(new List<FilmInformation> { new FilmInformation { FilmId = 1, FilmName = "test" } });
            Assert.Null(await _filmFinder.Find("unknownfilm"));
        }

        [Fact]
        public async void Find_should_return_film_it_is_an_exact_match()
        {
            _angleSharpClientMock.Setup(x => x.GetFilmIds())
                .ReturnsAsync(new List<FilmInformation> { new FilmInformation { FilmId = 1, FilmName = "John Wick" } });
            var filmInformation = await _filmFinder.Find("John Wick");
            Assert.Equal(1, filmInformation.FilmId);
            Assert.Equal("John Wick", filmInformation.FilmName);
        }

        [Theory]
        [InlineData("John WicK", "John Wick")]
        [InlineData("GUArdIans", "Guardians")]
        public async void Find_should_return_film_it_is_an_exact_match_with_different_casing(string filmName, string expected)
        {
            _angleSharpClientMock.Setup(x => x.GetFilmIds())
                .ReturnsAsync(new List<FilmInformation> { new FilmInformation { FilmId = 1, FilmName = expected } });
            var filmInformation = await _filmFinder.Find(filmName);
            Assert.Equal(1, filmInformation.FilmId);
            Assert.Equal(expected, filmInformation.FilmName);
        }

        [Theory]
        [InlineData("John Wi", "John Wick")]
        [InlineData("Guardi", "Guardians")]
        public async void Find_should_return_First_film_it_is_a_partial_match(string filmName, string expected)
        {
            _angleSharpClientMock.Setup(x => x.GetFilmIds())
                .ReturnsAsync(new List<FilmInformation> { new FilmInformation { FilmId = 1, FilmName = expected } });
            var filmInformation = await _filmFinder.Find(filmName);
            Assert.Equal(1, filmInformation.FilmId);
            Assert.Equal(expected, filmInformation.FilmName);
        }

        [Theory]
        [InlineData("JoHn Wi", "John Wick")]
        [InlineData("GuArDi", "Guardians")]
        public async void Find_should_return_First_film_it_is_a_partial_match_with_different_casing(string filmName, string expected)
        {
            _angleSharpClientMock.Setup(x => x.GetFilmIds())
                .ReturnsAsync(new List<FilmInformation> { new FilmInformation { FilmId = 1, FilmName = expected } });
            var filmInformation = await _filmFinder.Find(filmName);
            Assert.Equal(1, filmInformation.FilmId);
            Assert.Equal(expected, filmInformation.FilmName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async void GetFilmDetails_should_return_null_when_filmId_is_less_than_1(int filmId)
        {
            Assert.Null(await _filmFinder.GetFilmDetails(filmId));
        }

        [Fact]
        public async void GetFilmDetails_should_return_film_information_if_found()
        {
            _angleSharpClientMock.Setup(x => x.GetFilmData(10))
                .ReturnsAsync(new List<FilmTimes>() { new FilmTimes { AuditoriumInfo = "Screen 1", Title = "John Wick" } });
            var filmTimes = await _filmFinder.GetFilmDetails(10);
            Assert.NotNull(filmTimes);
            _angleSharpClientMock.Verify(x => x.GetFilmData(10), Times.Once);
            Assert.Equal("Screen 1", filmTimes.First().AuditoriumInfo);
            Assert.Equal("John Wick", filmTimes.First().Title);
        }
    }
}
