using AutoMapper;
using BAL.DTOs.TestResults;
using BAL.DTOs.Tests;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile() 
        {
            CreateMap<Test, GetTest>().ForMember(dept => dept.getTestResults, opts => opts.MapFrom(src => src.TestResults))
                                      .ReverseMap();
                            
        }
    }
}
