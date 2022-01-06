using AutoFixture;
using Bogus;
using FluentAssertions;
using LT_PhotoAlbum.Data;
using LT_PhotoAlbum.Data.Models.Dtos;
using LT_PhotoAlbum.Tests.Mocks;
using Moq;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LT_PhotoAlbum.Tests
{
    public class AlbumPhotoGetServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Randomizer _randomizer;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;

        public AlbumPhotoGetServiceTests()
        {
            _fixture = new Fixture();
            _randomizer = new Randomizer(DateTime.Now.Millisecond);
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        }

        [Fact(DisplayName = "When GetData() is called without arguments and the API return is nothing. Return an empty IEnumerable.")]
        public async Task GetData_WithoutArgumentsWhereApiReturnsNoData_Should_ReturnEmptyIEnumerableAsync()
        {
            var expectedCount = 0;
            var jsonForContent = "[]";
            var httpResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(jsonForContent) };
            var clientHandler = new DelegatingHandlerMock((request, cancellationToken) => Task.FromResult(httpResponseMessage));
            var client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri($"http://{_randomizer.String2(10)}.com/")
            };

            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            var sut = new AlbumPhotoGet(_httpClientFactoryMock.Object);
            var actual = await sut.GetDataAsync(null, null);

            actual.Count().Should().Be(expectedCount);
        }

        [Fact(DisplayName = "When GetData() is called without arguments and the API returns json data. Return an empty IEnumerable.")]
        public async Task GetData_WithoutArgumentsWhereApiReturnsData_Should_ReturnEmptyIEnumerableAsync()
        {
            var expected = _fixture.CreateMany<AlbumPhotoDto>();
            var expectedCount = expected.Count();
            var jsonForContent = JsonConvert.SerializeObject(expected);
            var httpResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent(jsonForContent) };
            var clientHandler = new DelegatingHandlerMock((request, cancellationToken) => Task.FromResult(httpResponseMessage));
            var client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri($"http://{_randomizer.String2(10)}.com/")
            };

            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            var sut = new AlbumPhotoGet(_httpClientFactoryMock.Object);
            var actual = await sut.GetDataAsync(null, null);

            actual.Count().Should().Be(expectedCount);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
