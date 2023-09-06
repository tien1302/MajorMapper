using AutoMapper;
using BAL.DTOs.Accounts;
using BAL.DTOs.Notifications;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, GetNotification>().ReverseMap();
        }
    }
}
