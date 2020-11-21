using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPreQualificationTool.Models
{
    public class LogEntry
    {
        public Applicant Applicant { get; set; }

        public CreditCard CreditCard { get; set; }

        public Rejection Rejection { get; set; }
    }
}
