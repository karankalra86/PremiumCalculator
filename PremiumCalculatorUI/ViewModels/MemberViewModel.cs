using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PremiumCalculatorUI.ViewModels
{
    public class MemberViewModel
    {
        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        [Required(ErrorMessage = "Please Enter Age")]
        [Range(1, 100, ErrorMessage = "Please enter in range of 1 to 100")]
        public int Age { get; set; }

        /// <summary>
        /// DateOfBirth
        /// </summary>
        [Required(ErrorMessage = "Please Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime ? DateOfBirth { get; set; }

        /// <summary>
        /// Occupation
        /// </summary>
        public List<string> OccupationList { get; set; }

        /// <summary>
        /// Occupation
        /// </summary>
        [Required(ErrorMessage = "Please select Occupation")]
        public string Occupation { get; set; }

        /// <summary>
        /// Sum Insured
        /// </summary>
        [Required(ErrorMessage = "Please Enter Sum insured")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than 0")]
        [Display(Name = "Sum Insured")]
        public decimal SumInsured { get; set; }

        public decimal Premium { get; set; }
    }
}