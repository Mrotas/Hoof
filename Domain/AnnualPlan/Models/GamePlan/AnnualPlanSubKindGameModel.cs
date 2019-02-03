using System.Collections.Generic;

namespace Domain.AnnualPlan.Models.GamePlan
{
    public class AnnualPlanSubKindGameModel : AnnualPlanGameBaseModel
    {
        public AnnualPlanSubKindGameModel()
        {
            AnnualPlanClassGameModels = new List<AnnualPlanClassGameModel>();
        }

        public int? SubKind { get; set; }
        public string SubKindName { get; set; }
        public IList<AnnualPlanClassGameModel> AnnualPlanClassGameModels { get; set; }
    }
}
