using AutoMapper;
using BusinessLogicLayer.DTOs.GameDtos;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer;

public class MyMapper : Profile
{
    public MyMapper()
    {
        CreateMap<GameDto, Game>()
            .ReverseMap();
        CreateMap<AddGameDto, Game>()
            .ReverseMap();
        CreateMap<UpdateGameDto, Game>()
            .ReverseMap();
    }
}
