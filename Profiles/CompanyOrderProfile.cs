using AutoMapper;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.CompanyOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Profiles
{
    public class CompanyOrderProfile : Profile
    {
        public CompanyOrderProfile()
        {
            CreateMap<CompanyOrder, CompanyOrderDto>()
                .ForMember(dest => dest.TransitDays, opt => opt.MapFrom(src => (src.ArrivalDate - src.PurchaseDate).TotalDays))
                .ForMember(dest => dest.Vendor, opt => opt.MapFrom(src => src.Vendor));

            CreateMap<CompanyOrderForCreationDto, CompanyOrder>();
        }
    }
}
