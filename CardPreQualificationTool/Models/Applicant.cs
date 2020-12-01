using System;
using System.ComponentModel.DataAnnotations;
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

        private TextInfo _textInfo = CultureInfo.CurrentCulture.TextInfo;

        public void UpdateLogEntry(LogEntry logEntry)
        {
            logEntry.FirstName = _textInfo.ToTitleCase(FirstName);
            logEntry.LastName = _textInfo.ToTitleCase(LastName);
            logEntry.DateOfBirth = DateOfBirth;
            logEntry.AnnualIncome = AnnualIncome;
        }
    }
}
