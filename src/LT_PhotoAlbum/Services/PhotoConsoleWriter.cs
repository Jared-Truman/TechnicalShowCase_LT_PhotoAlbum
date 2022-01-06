using AutoMapper;
using LT_PhotoAlbum.Abstractions;
using LT_PhotoAlbum.Arguments;
using LT_PhotoAlbum.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT_PhotoAlbum.Services
{
    public class PhotoConsoleWriter : IConsoleWriter<PhotoArgument>
    {
        private readonly IMapper _mapper;
        private readonly IConsoleWrapper _console;
        private readonly IAlbumPhotoGet _photoAlbumGetService;

        public PhotoConsoleWriter(IMapper mapper,
            IConsoleWrapper console,
            IAlbumPhotoGet photoAlbumGetService)
        {
            _mapper = mapper;
            _console = console;
            _photoAlbumGetService = photoAlbumGetService;
        }

        public async Task WriteLineAsync(PhotoArgument argument)
        {
            var photoAlbums = await _photoAlbumGetService.GetDataAsync(argument.AlbumId, argument.PhotoId);
            var photos = _mapper.Map<IEnumerable<Photo>>(photoAlbums);

            foreach (var photo in photos)
            {
                _console.WriteLine($"[{photo.Id}] {photo.Title}");
            }
        }
    }
}
