using PremiumCalcModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PremiumCalculatorWrapper
{
    public interface IPremiumCalcWrapper
    {
        string BaseURL { get; set; }
        string OperationURL { get; set; }

        Task<List<string>> GetOccupations();
        Task<decimal> CalculatePremium(MemberModel objMemberModel);
    }
}
