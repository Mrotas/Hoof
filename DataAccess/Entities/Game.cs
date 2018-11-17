namespace DataAccess.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int SubType { get; set; }
        public int? Class { get; set; }
        public double? Weight { get; set; }
    }
}
