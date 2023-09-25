using AutoMapper;
using matchSchedule.Models;
using matchSchedule.ViewModels;

namespace matchSchedule.Context
{
    public class AppMapperProfile: Profile
    {
        public AppMapperProfile()
        {
            CreateMap<Team, TeamViewModel>()
                .ForMember(t=>t.TeamId, p=>p.MapFrom(t=>t.TeamId)).ReverseMap();
        }
    }
}
