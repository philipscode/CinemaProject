using AutoMapper;
using Rambler.WebApi.Cinema.Dto;
using Rambler.WebApi.Cinema.Models;

namespace Rambler.WebApi.Cinema.Profiles
{
    /// <summary>
    /// Настройки для AutoMapper
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Session, SessionDto>();
            CreateMap<Film, FilmDto>();
            CreateMap<Hall, HallDto>();
            CreateMap<SessionType, SessionTypeDto>();
        }
    }
}