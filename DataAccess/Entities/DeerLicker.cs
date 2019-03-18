namespace DataAccess.Entities
{
    public class DeerLicker
    {
        public int Id { get; set; }
        public int Section { get; set; }
        public int District { get; set; }
        public string Forestry { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public int MarketingYearId { get; set; }
    }
}