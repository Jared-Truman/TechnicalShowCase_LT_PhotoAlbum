using AutoMapper;
using LT_PhotoAlbum.Abstractions;
using LT_PhotoAlbum.Arguments;
using LT_PhotoAlbum.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT_PhotoAlbum.Services
{
    public class AlbumConsoleWriter : IConsoleWriter<AlbumArgument>
    {
        private readonly IMapper _mapper;
        private readonly IConsoleWrapper _console;
        private readonly IAlbumPhotoGet _photoAlbumGetService;

        public AlbumConsoleWriter(
            IMapper mapper,
            IConsoleWrapper console,
            IAlbumPhotoGet photoAlbumGetService)
        {
            _mapper = mapper;
            _console = console;
            _photoAlbumGetService = photoAlbumGetService;
        }

        public async Task WriteLineAsync(AlbumArgument argument)
        {
            var photoAlbums = await _photoAlbumGetService.GetDataAsync(argument.AlbumId);
            var albums = _mapper.Map<IEnumerable<Album>>(photoAlbums);

            foreach (var album in albums.GroupBy(x => x.Id).Select(x => x.First()))
            {
                _console.WriteLine($"Album Id: {album.Id}");
            }
        }
    }
}
