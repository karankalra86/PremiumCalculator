using System;

namespace PremiumCalculatorAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class MemberModel
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// DateOfBirth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Occupation
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// Sum Insured
        /// </summary>
        public decimal SumInsured { get; set; }
    }
}