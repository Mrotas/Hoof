namespace DataAccess.Entities.AnnualPlan
{
    public class EstimatedGameBeforeHuntPeriodPlan
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int? SubType { get; set; }
        public int? Class { get; set; }
        public int? Count { get; set; }
        public int Year { get; set; }
    }
}
