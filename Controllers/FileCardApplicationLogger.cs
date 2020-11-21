using CardPreQualificationTool.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CardPreQualificationTool.Controllers
{
    public class FileCardApplicationLogger : ICardApplicationLogger
    {
        // Log the details of a customer's application for a credit card.
        // We log to the hard-coded file log.txt. In real life we would make this configurable, e.g. in appsettings.json.
        public void Log(LogEntry logEntry)
        {
            using StreamWriter writer = File.AppendText("log.txt");
            writer.WriteLine($"{DateTime.Now} {logEntry.Applicant} | {logEntry.CreditCard}{logEntry.Rejection}");
        }
    }
}
