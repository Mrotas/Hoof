using System;

namespace DataAccess.Dto
{
    public class LaborDto
    {
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
