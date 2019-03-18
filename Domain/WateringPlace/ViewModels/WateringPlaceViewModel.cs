using System;

namespace Domain.WateringPlace.ViewModels
{
    public class WateringPlaceViewModel
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
