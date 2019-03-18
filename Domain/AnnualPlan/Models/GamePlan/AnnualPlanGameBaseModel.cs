using System;
using Common.Enums;

namespace Domain.AnnualPlan.Models.GamePlan
{
    public abstract class AnnualPlanGameBaseModel
    {
        public GameType Type { get; set; }
        public int PreviousHuntPlanCulls { get; set; }
        public int PreviousHuntPlanCatches { get; set; }
        public int PreviousHuntPlanExecutionCulls { get; set; }
        public int PreviousHuntPlanExecutionCatches { get; set; }
        public int PreviousHuntPlanExecutionLosses { get; set; }
        public int PreviousHuntPlanExecutionSanitaryLosses { get; set; }
        public int PreviousHuntPlanExecutionTotal => PreviousHuntPlanExecutionCulls + PreviousHuntPlanExecutionCatches + PreviousHuntPlanExecutionLosses;
        public int GameCountBefore10March { get; set; }
        public int GameCountBeforeHuntingSeason => (int) (GameCountBefore10March * 1.25);
        public int CurrentHuntPlanCulls { get; set; }
        public int CurrentHuntPlanCullsMin
        {
            get
            {
                if (Type == GameType.Big)
                {
                    return (int)Math.Round(CurrentHuntPlanCulls * 0.9);
                }

                return (int)Math.Round(CurrentHuntPlanCulls * 0.85);
            }
        }

        public int CurrentHuntPlanCullsMax
        {
            get
            {
                if (Type == GameType.Big)
                {
                    return (int)Math.Round(CurrentHuntPlanCulls * 1.1);
                }

                return (int)Math.Round(CurrentHuntPlanCulls * 1.15);
            }
        }

        public int CurrentHuntPlanCatches { get; set; }
        public int CurrentHuntPlanCatchesMin
        {
            get
            {
                if (Type == GameType.Big)
                {
                    return (int)Math.Round(CurrentHuntPlanCatches * 0.9);
                }

                return (int)Math.Round(CurrentHuntPlanCatches * 0.85);
            }
        }

        public int CurrentHuntPlanCatchesMax
        {
            get
            {
                if (Type == GameType.Big)
                {
                    return (int)Math.Round(CurrentHuntPlanCatches * 1.1);
                }

                return (int)Math.Round(CurrentHuntPlanCatches * 1.15);
            }
        }
    }
}
