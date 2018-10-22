using System.ComponentModel;

namespace Domain.AnnualPlan.Models
{
    public class HuntEquipmentPlanModel
    {
        public int? Type { get; set; }
        public int? Count { get; set; }
        public int Unit { get; set; }
        public int Year { get; set; }
    }
    public enum HuntEquipment
    {
        [Description("Paśniki")]
        Pastures = 1,

        [Description("Lizawki")]
        DeerLickers = 2,

        [Description("Ambony")]
        Pulpits = 3,

        [Description("Woliery")]
        Aviaries = 4,

        [Description("Zagrody")]
        Farms = 5,

        [Description("Wodopoje")]
        WateringPlaces = 6
    }
}
