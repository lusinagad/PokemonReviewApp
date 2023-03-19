using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<PokemonDto,Pokemon>();
            CreateMap<Category,CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto,Country>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto,Owner>();
           
            CreateMap<Review, ReviewDto>();  // 2 ways mapping 
            CreateMap<ReviewDto,Review>();
           
            CreateMap<Reviewer, ReviewerDto>().ReverseMap();   // this also 2 ways mapping
        }
    }
}
