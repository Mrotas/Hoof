using System.Collections.Generic;

namespace Domain.AnnualPlan.Models.GamePlan
{
    public class AnnualPlanGameModel
    {
        public AnnualPlanGameModel()
        {
            AnnualPlanKindGameModels = new List<AnnualPlanKindGameModel>();
        }

        public IList<AnnualPlanKindGameModel> AnnualPlanKindGameModels { get; set; }
    }
}
