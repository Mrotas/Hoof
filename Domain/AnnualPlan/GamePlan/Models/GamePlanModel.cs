using Domain.Game.Model;

namespace Domain.AnnualPlan.GamePlan.Models
{
    public class GamePlanModel
    {
        public GameModel GameModel { get; set; }
        public int? Class { get; set; }
        public string ClassName => !Class.HasValue ? "" : Class.Value == 1 ? "selekcyjne" : "łowne";
        public GameHuntPlanModel PreviousGameHuntPlan { get; set; }
        public GameExecutionModel PreviousGamePlanExecution { get; set; }
        public EstimatedGameCountModel EstimatedGameCount { get; set; }
        public GameSettlementPlanModel GameSettlementPlan { get; set; }
        public GameHuntPlanModel CurrentGameHuntPlan { get; set; }
    }
}
