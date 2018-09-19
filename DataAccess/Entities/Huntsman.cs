using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    //[Table("Huntsman")]
    public class Huntsman
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Rank { get; set; }
    }
}
