namespace CardPreQualificationTool.Models
{
    public class CreditCard
    {
        public string CardType { get; set; }

        public string ImageURL { get; set; }

        public decimal InterestRate { get; set; }

        public string Description { get; set; }

        public void UpdateLogEntry(LogEntry logEntry)
        {
            logEntry.CardType = CardType;
            logEntry.InterestRate = InterestRate;
        }
    }
}
