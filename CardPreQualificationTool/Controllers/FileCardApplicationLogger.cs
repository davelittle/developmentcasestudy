using CardPreQualificationTool.Models;
using System.IO;

namespace CardPreQualificationTool.Controllers
{
    public class FileCardApplicationLogger : ICardApplicationLogger
    {
        private readonly string _filePath;
        private bool _headingsWritten = false;

        public FileCardApplicationLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Log(LogEntry logEntry)
        {
            using StreamWriter writer = File.AppendText(_filePath);

            if (!_headingsWritten && (!File.Exists(_filePath) || new FileInfo(_filePath).Length == 0))
            {
                writer.WriteLine("Time,First Name,Last Name,Date of Birth,Annual Income (£),Card Type,Interest Rate (%),Rejection Reason");
                _headingsWritten = true;
            }

            writer.WriteLine($"{logEntry.Timestamp},{logEntry.FirstName},{logEntry.LastName},{logEntry.DateOfBirth.ToShortDateString()},{logEntry.AnnualIncome:F2},{logEntry.CardType},{logEntry.InterestRate:F1},{logEntry.RejectionReason}");
        }
    }
}
