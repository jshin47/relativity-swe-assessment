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

        // UpdateRequest -> User
        CreateMap<UpdateShowRequestDto, Show>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
    }
}