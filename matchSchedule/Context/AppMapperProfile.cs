using AutoMapper;
using matchSchedule.Models;
using matchSchedule.ViewModels;

namespace matchSchedule.Context
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<Team, TeamViewModel>()
                .ForMember(t => t.TeamId, m => m.MapFrom(t => t.TeamId)).ReverseMap();

            CreateMap<Player, PlayerViewModel>()
                .ForMember(p => p.PlayerId, m => m.MapFrom(p => p.PlayerId))
                .ReverseMap();

            CreateMap<Tournament, TournamentViewModel>()
                .ForMember(t => t.TournamentId, m => m.MapFrom(t => t.Id)).ReverseMap();

            CreateMap<Match, MatchViewModel>()
                .ForMember(m => m.MatchId, mf => mf.MapFrom(m => m.MatchId)).ReverseMap();
        }
    }
}
