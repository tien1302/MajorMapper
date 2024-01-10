using AutoMapper;
using BAL.DTOs.Majors;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class MajorProfile : Profile
    {
        public MajorProfile()
        {
            CreateMap<Major, GetMajor>().ForMember(dept => dept.PersonalityTypeName, opts => opts.MapFrom(src => src.PersonalityTypes.Select(m => m.Name)))
                                        .ForMember(dept => dept.PersonalityTypeId, opts => opts.MapFrom(src => src.PersonalityTypes.Select(m => m.Id)))
                                        .ReverseMap();
        }
    }
}
