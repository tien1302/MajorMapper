using AutoMapper;
using BAL.DTOs.Methods;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class MethodProfile : Profile
    {
        public MethodProfile() 
        {
            CreateMap<Method, GetMethod>().ReverseMap();
        }
    }
}
