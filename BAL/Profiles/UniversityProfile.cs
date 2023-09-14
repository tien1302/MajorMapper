using AutoMapper;
using BAL.DTOs.Universities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class UniversityProfile : Profile
    {
        public UniversityProfile() 
        {
            CreateMap<University, GetUniversity>().ForMember(dept => dept.MajorName, opts => opts.MapFrom(src => src.Majors.Select(m => m.Name))).ReverseMap();
        }
    }
}
