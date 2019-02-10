using System;

namespace Domain.Hunt.Models
{
    public class HuntCreateModel
    {
        public DateTime Date { get; set; }
        public string City { get; set; }
        public int Circuit { get; set; }
        public int District { get; set; }
        public int? GameType { get; set; }
        public int? GameKind { get; set; }
        public int? GameSubKind { get; set; }
        public int? GameClass { get; set; }
        public double GameWeight { get; set; }
        public int Shots { get; set; }
    }
}
