using System;

namespace Domain.MarketingYear.Models
{
    public class MarketingYearModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public override string ToString()
        {
            return $"{Start.Year}/{End.Year}";
        }
    }
}
