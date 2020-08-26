using System.Collections.Generic;

namespace PremiumCalculatorData.Repository
{
    public interface IOccupationRepository
    {
        IEnumerable<string> GetOccupations();
        decimal GetOccupationRatingFactor(string strOccupation);
    }
}
