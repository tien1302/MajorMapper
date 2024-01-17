using AutoMapper;
using BAL.DTOs.TestResults;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class TestResultProfile : Profile
    {
        public TestResultProfile()
        {
            CreateMap<TestResult, GetTestResult>().ForMember(dept => dept.getScores, opts => opts.MapFrom(src => src.Scores)).ReverseMap();
        }
    }
}
