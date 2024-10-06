namespace WebApi.Helpers;

using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Shows;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Show, ShowDto>()
            .ForMember(src => src.Categories, opt => opt.MapFrom(dest => dest.ShowCategories.Select(y => y.Name)));
        CreateMap<CreateShowRequestDto, Show>();
        
        
        CreateMap<ShowCsvRowDto, CreateShowRequestDto>();
        
        CreateMap<UpdateShowRequestDto, Show>();
    }
}