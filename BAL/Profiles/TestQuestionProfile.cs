﻿using AutoMapper;
using BAL.DTOs.TestQuestions;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class TestQuestionProfile : Profile
    {
        public TestQuestionProfile() 
        {
            CreateMap<TestQuestion, GetTestQuestion>().ReverseMap();
        }
    }
}
