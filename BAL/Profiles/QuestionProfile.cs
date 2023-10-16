using AutoMapper;
using BAL.DTOs.Questions;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile() 
        { 
            CreateMap<Question, GetQuestion>().ReverseMap();
        }
    }
}
