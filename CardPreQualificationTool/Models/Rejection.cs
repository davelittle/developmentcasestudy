using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPreQualificationTool.Models
{
    public class Rejection
    {
        public string Reason { get; set; }

        public override string ToString()
        {
            return $"Rejected: {Reason}";
        }
    }
}
