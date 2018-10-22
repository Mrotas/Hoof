namespace DataAccess.Entities.AnnualPlan
{
    public class TrunkFoodPlan
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double? Count { get; set; }
        public int Unit { get; set; }
        public int Year { get; set; }
    }
}
