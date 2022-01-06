using LT_PhotoAlbum.Abstractions;
using LT_PhotoAlbum.Data.Models.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LT_PhotoAlbum.Data
{
    public class AlbumPhotoGet : IAlbumPhotoGet
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AlbumPhotoGet(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<AlbumPhotoDto>> GetDataAsync(int? albumId = null, int? photoId = null)
        {
            var relativePath = GetRelativePath(albumId, photoId);
            var response = _httpClientFactory
                .CreateClient("LT")
                .GetAsync($"Photos{relativePath}")
                .Result;
            var json = await response.Content.ReadAsStringAsync();

            return json != "{}" ?
                JsonConvert.DeserializeObject<IEnumerable<AlbumPhotoDto>>(json) :
                new List<AlbumPhotoDto>();
        }

        private static string GetRelativePath(int? albumId, int? photoId)
        {
            var queryParams = new List<KeyValuePair<string, string>>();

            if (albumId.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("albumId", albumId.ToString()));
            if (photoId.HasValue)
                queryParams.Add(new KeyValuePair<string, string>("id", photoId.ToString()));

            return $"?{String.Join('&', queryParams.Select(x => $"{x.Key}={x.Value}"))}";
        }
    }
}
