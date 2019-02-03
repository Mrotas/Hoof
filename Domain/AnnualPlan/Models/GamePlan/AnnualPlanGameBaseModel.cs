namespace Domain.AnnualPlan.Models.GamePlan
{
    public abstract class AnnualPlanGameBaseModel
    {
        public int PreviousHuntPlanCulls { get; set; }
        public int PreviousHuntPlanCatches { get; set; }
        public int PreviousHuntPlanExecutionCulls { get; set; }
        public int PreviousHuntPlanExecutionCatches { get; set; }
        public int PreviousHuntPlanExecutionLosses { get; set; }
        public int PreviousHuntPlanExecutionSanitaryLosses { get; set; }
        public int PreviousHuntPlanExecutionTotal => PreviousHuntPlanExecutionCulls + PreviousHuntPlanExecutionCatches + PreviousHuntPlanExecutionLosses;
        public int GameCountBefore10March { get; set; }
        public int GameCountBeforeHuntingSeason { get; set; }
        public int CurrentHuntPlanCulls { get; set; }
        public int CurrentHuntPlanCatches { get; set; }
    }
}
