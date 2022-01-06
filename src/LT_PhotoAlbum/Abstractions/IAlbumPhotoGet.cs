using LT_PhotoAlbum.Data.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT_PhotoAlbum.Abstractions
{
    public interface IAlbumPhotoGet
    {
        public Task<IEnumerable<AlbumPhotoDto>> GetDataAsync(int? albumId = null, int? photoId = null);
    }
}
