using System;

namespace DataAccess.Entities
{
    public class WateringPlace
    {
        public int Id { get; set; }
        public int Section { get; set; }
        public int District { get; set; }
        public string Forestry { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? RemovedDate { get; set; }
    }
}