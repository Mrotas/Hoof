namespace DataAccess.Entities
{
    public class GameCountBeforeHuntingSeasonPlan
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int? Count { get; set; }
        public int Year { get; set; }
    }
}
