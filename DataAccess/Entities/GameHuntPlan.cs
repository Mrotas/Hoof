namespace DataAccess.Entities
{
    public class GameHuntPlan
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int? Class { get; set; }
        public int? Cull { get; set; }
        public int? Catch { get; set; }
        public int Year { get; set; }
    }
}
