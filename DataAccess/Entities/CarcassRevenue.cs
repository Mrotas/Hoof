namespace DataAccess.Entities
{
    public class CarcassRevenue
    {
        public int Id { get; set; }
        public int HuntedGameId { get; set; }
        public double Revenue { get; set; }
        public double CarcassWeight { get; set; }
    }
}
