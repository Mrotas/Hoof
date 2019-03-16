using System;

namespace DataAccess.Dto
{
    public class PulpitDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int Section { get; set; }
        public string District { get; set; }
        public string Forestry { get; set; }
        public bool HasRoof { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? RemovedDate { get; set; }
    }
}
