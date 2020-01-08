using AutoMapper;
using Betting_Aggregator.Api.Dtos;

namespace Betting_Aggregator.API.Dtos
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Dependent, Business.Models.Dependent>();
            CreateMap<Business.Models.Dependent, Dependent>();

            CreateMap<LeagueDTO, Business.Models.League>();
            CreateMap<Business.Models.League, LeagueDTO>();
        }
    }
}
