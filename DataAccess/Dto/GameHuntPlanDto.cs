namespace DataAccess.Dto
{
    public class GameHuntPlanDto
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int? Class { get; set; }
        public int? Cull { get; set; }
        public int? Catch { get; set; }
        public int MarketingYearId { get; set; }
    }
}
