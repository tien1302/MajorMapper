using AutoMapper;
using BAL.DTOs.Majors;
using BAL.DTOs.PersonalityTypes;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class PersonalityTypeProfile : Profile
    {
        public PersonalityTypeProfile() 
        {
            CreateMap<PersonalityType, GetPersonalityType>().ReverseMap();
        }
    }
}
