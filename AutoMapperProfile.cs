using api.Dtos;
using api.Models;
using AutoMapper;

namespace api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Client, ClientReadDto>();
        CreateMap<ClientWriteDto, Client>();
    }
}