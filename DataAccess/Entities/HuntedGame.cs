namespace DataAccess.Entities
{
    public class HuntedGame
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int? GameClass { get; set; }
        public double GameWeight { get; set; }
    }
}
