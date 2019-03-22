using System;

namespace DataAccess.Entities
{
    public class Labor
    {
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
