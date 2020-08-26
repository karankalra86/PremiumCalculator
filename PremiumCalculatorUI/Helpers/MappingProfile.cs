using AutoMapper;
using PremiumCalcModels;
using PremiumCalculatorUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PremiumCalculatorUI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MemberViewModel, MemberModel>().ForMember(p => p.Occupation, opt => opt.MapFrom(c => c.SelectedOccupation));
        }

        
    }
}