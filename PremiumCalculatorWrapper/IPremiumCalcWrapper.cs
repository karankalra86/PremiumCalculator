using PremiumCalcModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PremiumCalculatorWrapper
{
    public interface IPremiumCalcWrapper
    {
        Task<List<string>> GetOccupations();
        Task<decimal> CalculatePremium(MemberModel objMemberModel);
    }
}
