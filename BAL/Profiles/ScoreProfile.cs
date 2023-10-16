using AutoMapper;
using BAL.DTOs.Scores;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class ScoreProfile : Profile
    {
        public ScoreProfile()
        {
            CreateMap<Score, GetScore>().ReverseMap();
        }
    }
}
