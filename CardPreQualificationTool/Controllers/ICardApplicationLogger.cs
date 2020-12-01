using CardPreQualificationTool.Models;

namespace CardPreQualificationTool.Controllers
{
    interface ICardApplicationLogger
    {
        public void Log(LogEntry entry);
    }
}
