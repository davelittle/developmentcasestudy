namespace CardPreQualificationTool.Models
{
    // Represents a decision that a customer is not eligible for a credit card
    public class Rejection : Decision
    {
        public string Reason { get; set; }

        public Rejection(string reason) : base()
        {
            Reason = reason;
        }

        public override void UpdateLogEntry(LogEntry logEntry)
        {
            base.UpdateLogEntry(logEntry);
            logEntry.RejectionReason = Reason;
        }
    }
}
