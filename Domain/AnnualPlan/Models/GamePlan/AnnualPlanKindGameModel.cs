using System.Collections.Generic;

namespace Domain.AnnualPlan.Models.GamePlan
{
    public class AnnualPlanKindGameModel : AnnualPlanGameBaseModel
    {
        public AnnualPlanKindGameModel()
        {
            AnnualPlanSubKindGameModels = new List<AnnualPlanSubKindGameModel>();    
        }

        public int Kind { get; set; }
        public string KindName { get; set; }
        public IList<AnnualPlanSubKindGameModel> AnnualPlanSubKindGameModels { get; set; }
    }
}
