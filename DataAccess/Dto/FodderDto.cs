using System;

namespace DataAccess.Dto
{
    public class FodderDto
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int Kilograms { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
