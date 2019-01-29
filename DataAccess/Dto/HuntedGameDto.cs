namespace DataAccess.Dto
{
    public class HuntedGameDto
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int? GameClass { get; set; }
        public double GameWeight { get; set; }
    }
}
