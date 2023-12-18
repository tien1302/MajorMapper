using AutoMapper;
using BAL.DTOs.Feedbacks;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<Feedback, GetFeedback>().ForMember(dept => dept.Name, opts => opts.MapFrom(src => src.Booking.Player.Name)).ReverseMap();
        }
    }
}
