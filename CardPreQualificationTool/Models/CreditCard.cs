using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPreQualificationTool.Models
{
    public class CreditCard
    {
        public string CardType { get; set; }

        public string ImageURL { get; set; }

        public decimal InterestRate { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"Card type: {CardType}, Interest rate: {InterestRate}%";
        }
    }
}
