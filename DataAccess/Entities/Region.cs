using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Region")]
    public class Region
    {
        public int Id { get; set; }
        public int City { get; set; }
        public int Circuit { get; set; }
        public int District { get; set; }
    }
}
