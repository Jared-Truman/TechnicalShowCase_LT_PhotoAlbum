using AutoMapper;
using LT_PhotoAlbum.Data.Models;
using LT_PhotoAlbum.Data.Models.Dtos;

namespace LT_PhotoAlbum.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AlbumPhotoDto, Album>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AlbumId));

            CreateMap<AlbumPhotoDto, Photo>();
        }
    }
}
