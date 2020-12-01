using System;

namespace CardPreQualificationTool.Models
{
    public abstract class Decision
    {
        public DateTime Timestamp { get; set; }

        public Decision()
        {
            Timestamp = DateTime.Now;
        }

        public virtual void UpdateLogEntry(LogEntry logEntry)
        {
            logEntry.Timestamp = Timestamp;
        }
    }
}
