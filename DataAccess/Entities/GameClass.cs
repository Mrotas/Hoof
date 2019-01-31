using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class GameClass
    {
        [Key]
        public int Id { get; set; }
        public string ClassName { get; set; }
    }
}
