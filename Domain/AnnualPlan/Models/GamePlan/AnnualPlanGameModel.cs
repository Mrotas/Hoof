using System.Collections.Generic;
using Common.Enums;

namespace Domain.AnnualPlan.Models.GamePlan
{
    public class AnnualPlanGameModel
    {
        public AnnualPlanGameModel()
        {
            AnnualPlanKindGameModels = new List<AnnualPlanKindGameModel>();
        }

        public GameType Type { get; set; }
        public IList<AnnualPlanKindGameModel> AnnualPlanKindGameModels { get; set; }
    }
}
