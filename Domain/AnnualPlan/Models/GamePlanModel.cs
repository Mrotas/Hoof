namespace Domain.AnnualPlan.Models
{
    public class GamePlanModel
    {
        public int Type { get; set; }
        public int? SubType { get; set; }
        public int? Class { get; set; }
        public int? Cull { get; set; }
        public int? Catch { get; set; }
        public int? Loss { get; set; }
        public int Year { get; set; }
    }
}
