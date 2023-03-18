using Api.Domain.Entities;
using Api.Domain.models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<UserModel, UserEntity>()
                .ReverseMap();

            CreateMap<UfModel, UfEntity>()
                .ReverseMap();

            CreateMap<MunicipioModel, MunicipioEntity>()
                .ReverseMap();

            CreateMap<CepModel, CepEntity>()
                .ReverseMap();
        }
    }
}