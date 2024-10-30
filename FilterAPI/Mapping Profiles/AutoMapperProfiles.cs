using AutoMapper;
using FilterAPI.Models.Domain;
using FilterAPI.Models.Responses;

namespace FilterAPI.Mapping_Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PaginatedFilteredResponse, Product>().ReverseMap();
        }
    }
}
