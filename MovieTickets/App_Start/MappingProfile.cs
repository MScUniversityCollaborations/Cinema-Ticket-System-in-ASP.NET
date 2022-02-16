using AutoMapper;
using MovieTickets.Dtos;
using MovieTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTickets.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>();
            Mapper.CreateMap<Genre, GenreDto>();

            // Dto to Domain
            Mapper.CreateMap<MovieDto, Movie>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}