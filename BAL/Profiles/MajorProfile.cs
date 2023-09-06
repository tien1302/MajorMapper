using AutoMapper;
using BAL.DTOs.Majors;
using BAL.DTOs.Universities;
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
            CreateMap<Major, GetMajor>().ReverseMap();
        }
    }
}
