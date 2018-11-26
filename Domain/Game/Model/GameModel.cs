namespace Domain.Game.Model
{
    public class GameModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int Kind { get; set; }
        public string KindName { get; set; }
        public int? SubKind { get; set; }
        public string SubKindName { get; set; }
    }
}
