namespace DataAccess.Entities.AnnualPlan
{
    public class GamePlan
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int? SubType { get; set; }
        public int? Class { get; set; }
        public int? Cull { get; set; }
        public int? Catch { get; set; }
        public int Year { get; set; }
    }
}
