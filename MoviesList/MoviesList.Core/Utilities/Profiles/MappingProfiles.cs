using AutoMapper;
using MoviesList.Core.DTOs.Request;
using MoviesList.Core.DTOs.Response;
using MoviesList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.Utilities.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateMovieResponseDTO, Movie>().ReverseMap()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.ToLower()));
            CreateMap<Movie, CreateMovieRequestDTO>().ReverseMap()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.ToLower()));
            CreateMap<Actor, CreateActorResponseDTO>()
                .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ActorName, opt => opt.MapFrom(src => src.Name));
            //CreateMap<CreateActorResponseDTO, Actor>().ReverseMap();
            //CreateMap<RateMovieRequest, Rating>().ReverseMap();
            //CreateMap<RateMovieResponse, Rating>().ReverseMap();
            //CreateMap<UpdateActorRequest, Actor>().ReverseMap();
            //CreateMap<UpdateActorResponse, Actor>().ReverseMap();
            //CreateMap<DeleteActorRequest, Actor>().ReverseMap();
            //CreateMap<UpdateMovieRequest, Movie>().ReverseMap();
            //CreateMap<UpdateMovieResponse, Movie>().ReverseMap();
            //CreateMap<DeleteActorResponse, Actor>().ReverseMap();
            //CreateMap<DeleteMovieRequest, Movie>().ReverseMap();
        }

        private string GetLowerCaseFullName(CreateActorRequestDTO actor)
        {
            return $"{actor.FirstName}, {actor.LastName}".ToLower();
        }
    }
}
