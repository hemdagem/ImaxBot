using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace ImaxBot.Core
{
    public class ImaxFilmTimesTests
    {
        private ImaxFilmTimes imaxFilmTimes;
        private Mock<IFilmService> iFilmService;
        public ImaxFilmTimesTests()
        {
            iFilmService = new Mock<IFilmService>();
            imaxFilmTimes = new ImaxFilmTimes(iFilmService.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetFilmTimes_should_throw_null_reference_exception_when_filmName_is_null_or_empty(string filmName)
        {
            Assert.Throws<NullReferenceException>(() => imaxFilmTimes.GetFilmTimes(filmName));
        }

        [Fact]
        public void GetFilmTimes_should_return_empty_list_when_film_service_doesnt_find_film()
        {
            var filmName = "hello";
            iFilmService.Setup(x => x.GetFilmTimes(filmName)).Returns(new List<FilmTimes>());

            Assert.Empty(imaxFilmTimes.GetFilmTimes(filmName));
            iFilmService.Verify(x => x.GetFilmTimes(filmName), Times.Once);
        }

        [Fact]
        public void GetFilmTimes_should_return_list_when_film_service_finds_film()
        {
            var filmName = "hello";
            iFilmService.Setup(x => x.GetFilmTimes(filmName)).Returns(new List<FilmTimes>() { new FilmTimes() });
            var filmTimes = imaxFilmTimes.GetFilmTimes(filmName);
            Assert.Equal(filmTimes.Count, 1);
            iFilmService.Verify(x => x.GetFilmTimes(filmName), Times.Once);
        }
    }
}