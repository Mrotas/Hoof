using Domain.Game.Model;

namespace Domain.AnnualPlan.Models.GamePlan
{
    public class GamePlanModel
    {
        public GameModel GameModel { get; set; }
        public int? Class { get; set; }
        public string ClassName { get; set; }
        public GameHuntPlanModel PreviousGameHuntPlan { get; set; }
        public GameExecutionModel PreviousGamePlanExecution { get; set; }
        public GameCountModel GameCountModel { get; set; }
        public GameSettlementPlanModel GameSettlementPlan { get; set; }
        public GameHuntPlanModel CurrentGameHuntPlan { get; set; }
    }
}
