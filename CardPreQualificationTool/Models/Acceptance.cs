namespace CardPreQualificationTool.Models
{
    // Represents a decision to invite a user to apply for a credit card
    public class Acceptance : Decision
    {
        public CreditCard CreditCard { get; set; }

        public Acceptance(CreditCard creditCard) : base()
        {
            CreditCard = creditCard;
        }

        public override void UpdateLogEntry(LogEntry logEntry)
        {
            base.UpdateLogEntry(logEntry);
            CreditCard.UpdateLogEntry(logEntry);
        }
    }
}
