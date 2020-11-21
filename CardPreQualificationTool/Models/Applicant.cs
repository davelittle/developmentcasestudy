using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace CardPreQualificationTool.Models
{
    public class Applicant
    {
        [Display(Name = "First Name")]
        [StringLength(60)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(60)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Annual Income")]
        public decimal AnnualIncome { get; set; }

        static readonly TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

        public override string ToString()
        {
            return $"Name: {textInfo.ToTitleCase(FirstName)} {textInfo.ToTitleCase(LastName)}, Date of birth: {DateOfBirth.ToShortDateString()}, Annual income: £{AnnualIncome}";
        }
    }
}
