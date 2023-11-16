using AutoMapper;
using matchSchedule.Models;
using matchSchedule.ModelsDTO;
using matchSchedule.ViewModels;

namespace matchSchedule.Context
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<Team, TeamViewModel>()
                .ForMember(t => t.TeamId, m => m.MapFrom(t => t.Id))
                .ReverseMap();

            CreateMap<Match, NewMatchDTO>()
               .ForMember(m => m.MatchId, map => map.MapFrom(m => m.MatchId))
               .ReverseMap();

            CreateMap<Player, PlayerViewModel>()
                .ForMember(p => p.PlayerId, m => m.MapFrom(p => p.PlayerId))
                .ReverseMap();

            CreateMap<Tournament, TournamentViewModel>()
                .ForMember(t => t.TournamentId, m => m.MapFrom(t => t.Id))
                .ReverseMap();

            CreateMap<Match, MatchViewModel>()
                .ForMember(m => m.MatchId, mf => mf.MapFrom(m => m.MatchId))
                .ForMember(m => m.AwayTeam, mf => mf.MapFrom(m => m.AwayTeam))
                .ForMember(m => m.HomeTeam, mf => mf.MapFrom(m => m.HomeTeam))
                .ForMember(m => m.HomeTeamId, mf => mf.MapFrom(m => m.HomeTeamId))
                .ForMember(m => m.AwayTeamId, mf => mf.MapFrom(m => m.AwayTeamId))
                .ForMember(m => m.Tournament, mf => mf.MapFrom(m => m.Tournament))
                .ReverseMap();
        }
    }
}
