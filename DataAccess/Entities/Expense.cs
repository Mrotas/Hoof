using System;

namespace DataAccess.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
}
