using System.Collections.Generic;
using System.Linq;

namespace PremiumCalculatorData.Repository
{
    public class OccupationRepository : IOccupationRepository
    {

        public readonly Dictionary<string, string> OccupationRatings = new Dictionary<string, string>{
            { "Cleaner", "Light Manual" }, { "Doctor", "Professional" },
            { "Author", "White Collar" }, { "Farmer", "Heavy Manual" },
            { "Mechanic", "Heavy Manual" }, { "Florist", "Light Manual" }};

        public readonly Dictionary<string, decimal> OccupationRatingFactor = new Dictionary<string, decimal>{
            { "Professional", 1.0m }, { "White Collar", 1.25m },
            { "Light Manual", 1.5m }, { "Heavy Manual", 1.75m }};

        public IEnumerable<string> GetOccupations()
        {
            return OccupationRatings.Keys.ToList();
        }

        public decimal GetOccupationRatingFactor(string strOccupation)
        {
            string occRating = string.Empty;
            decimal occRatingFactor = decimal.Zero;

            if (OccupationRatings.ContainsKey(strOccupation))
            {
                occRating = OccupationRatings[strOccupation];

                if (!string.IsNullOrEmpty(occRating))
                    occRatingFactor = OccupationRatingFactor[occRating];
            }
            return occRatingFactor;
        }
    }
}
