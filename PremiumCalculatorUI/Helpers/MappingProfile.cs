using AutoMapper;
using PremiumCalcModels;
using PremiumCalculatorUI.ViewModels;

namespace PremiumCalculatorUI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MemberViewModel, MemberModel>().ForMember(p => p.Occupation, opt => opt.MapFrom(c => c.Occupation));
        }

        
    }
}