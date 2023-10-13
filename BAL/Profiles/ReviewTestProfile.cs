using AutoMapper;
using BAL.DTOs.ReviewTests;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class ReviewTestProfile : Profile
    {
        public ReviewTestProfile()
        {
            CreateMap<ReviewTest, GetReviewTest>().ReverseMap();
        }
    }
}
