using AutoMapper;
using matchSchedule.Models;
using matchSchedule.ModelsDTO;

namespace matchSchedule.Context
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<Team, NewTeamDTO>()
                .ForMember(t => t.TeamId, m => m.MapFrom(t => t.Id))
                .ReverseMap();

            CreateMap<Match, NewMatchDTO>()
               .ForMember(m => m.MatchId, map => map.MapFrom(m => m.MatchId))
               .ReverseMap();

            CreateMap<Player, NewPlayerDTO>()
                .ForMember(p => p.PlayerId, m => m.MapFrom(p => p.PlayerId))
                .ReverseMap();

            CreateMap<Tournament, NewTournamentDTO>()
                .ForMember(t => t.TournamentId, m => m.MapFrom(t => t.Id))
                .ReverseMap();


        }
    }
}
