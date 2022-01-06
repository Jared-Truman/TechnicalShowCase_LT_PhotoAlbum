using AutoFixture;
using AutoMapper;
using Bogus;
using FluentAssertions;
using LT_PhotoAlbum.Abstractions;
using LT_PhotoAlbum.Arguments;
using LT_PhotoAlbum.Data.Models.Dtos;
using LT_PhotoAlbum.Profiles;
using LT_PhotoAlbum.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LT_PhotoAlbum.Tests
{
    public class PhotoConsoleWriterTests
    {
        private readonly Randomizer _randomizer;
        private readonly IMapper _mapper;
        private readonly Mock<IConsoleWrapper> _consoleMock;
        private readonly Mock<IAlbumPhotoGet> _albumPhotoGetServiceMock;

        public PhotoConsoleWriterTests()
        {
            _randomizer = new Randomizer(DateTime.Now.Millisecond);
            _consoleMock = new Mock<IConsoleWrapper>();
            _albumPhotoGetServiceMock = new Mock<IAlbumPhotoGet>();

            var profile = new AutoMapperProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(mapperConfig);
        }

        [Fact(DisplayName = "When WriteLine() is called where an AlbumId  and/or a PhotoId is not provided and nothing is returned from the data source. Write nothing out.")]
        public async Task Writeline_WhenNoAlbumIdIsPassedAndNothingIsGrabbedFromDataSource_WriteNothingAsync()
        {
            var expectedCount = 0;
            var actual = new List<string>();

            _consoleMock.Setup(x => x.WriteLine(It.IsAny<string>())).Callback((string x) => actual.Add(x));
            _albumPhotoGetServiceMock.Setup(x => x.GetDataAsync(It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult((IEnumerable<AlbumPhotoDto>)new List<AlbumPhotoDto>()));

            var sut = new PhotoConsoleWriter(_mapper, _consoleMock.Object, _albumPhotoGetServiceMock.Object);
            await sut.WriteLineAsync(new PhotoArgument());

            actual.Count.Should().Be(expectedCount);
        }

        [Fact(DisplayName = "When WriteLine() is called where an AlbumId and/or a PhotoId is provided and nothing is returned from the data source. Write nothing out.")]
        public async Task Writeline_WhenAlbumIdIsPassedAndNothingIsGrabbedFromDataSource_WriteNothingAsync()
        {
            var expectedCount = 0;
            var actual = new List<string>();
            var argument = new PhotoArgument() { AlbumId = _randomizer.Int(), PhotoId = _randomizer.Int() };

            _consoleMock.Setup(x => x.WriteLine(It.IsAny<string>())).Callback((string x) => actual.Add(x));
            _albumPhotoGetServiceMock.Setup(x => x.GetDataAsync(It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult((IEnumerable<AlbumPhotoDto>)new List<AlbumPhotoDto>()));

            var sut = new PhotoConsoleWriter(_mapper, _consoleMock.Object, _albumPhotoGetServiceMock.Object);
            await sut.WriteLineAsync(argument);

            actual.Count.Should().Be(expectedCount);
        }

        [Fact(DisplayName = "When WriteLine() is called and data is returned from the data source. Write the photo id and title out.")]
        public async Task Writeline_WhenDataIsGrabbedFromDataSource_WriteIdsAsync()
        {
            var photoAlbumDtos = new Fixture().CreateMany<AlbumPhotoDto>();
            var expected = photoAlbumDtos.Select(x => string.Concat("[", x.Id, "]", " ", x.Title)).ToList();
            var expectedCount = photoAlbumDtos.Count();
            var actual = new List<string>();
            var argument = new PhotoArgument() { AlbumId = _randomizer.Int(), PhotoId = _randomizer.Int() };

            _consoleMock.Setup(x => x.WriteLine(It.IsAny<string>())).Callback((string x) => actual.Add(x));
            _albumPhotoGetServiceMock.Setup(x => x.GetDataAsync(It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(photoAlbumDtos));

            var sut = new PhotoConsoleWriter(_mapper, _consoleMock.Object, _albumPhotoGetServiceMock.Object);
            await sut.WriteLineAsync(argument);

            actual.Count.Should().Be(expectedCount);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
