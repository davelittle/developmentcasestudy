using System;

namespace CardPreQualificationTool.Models
{
    // Represents details of a request to be logged to a database or file
    public class LogEntry
    {
        public Applicant Applicant { get; set; }

        public Decision Decision { get; set; }

        public DateTime Timestamp { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal AnnualIncome { get; set; }

        public string CardType { get; set; }

        public decimal? InterestRate { get; set; }

        public string RejectionReason { get; set; }

        public LogEntry(Applicant applicant, Decision decision)
        {
            applicant.UpdateLogEntry(this);
            decision.UpdateLogEntry(this);
        }
    }
}
