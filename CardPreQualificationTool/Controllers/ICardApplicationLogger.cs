using CardPreQualificationTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPreQualificationTool.Controllers
{
    interface ICardApplicationLogger
    {
        public void Log(LogEntry entry);
    }
}
