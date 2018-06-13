using AngularBasic.Data.Entities;
using AngularBasic.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularBasic.Data
{
    public class AngularBasicMappingProfile : Profile
    {
        public AngularBasicMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.Id, ex => ex.MapFrom(o => o.Id)).ReverseMap();

            CreateMap<OrderItem, OrderViewModel>()
                .ReverseMap();

        }
    }
}
