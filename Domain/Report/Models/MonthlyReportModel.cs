namespace Domain.Report.Models
{
    public class MonthlyReportModel
    {
        public string GameKindName { get; set; }
        public string GameSubKindName { get; set; }
        public string GameClass { get; set; }
        public int? Cull { get; set; }
        public int? Catch { get; set; }
        public int? Loss { get; set; }
        public int? Planned { get; set; }
    }
}
