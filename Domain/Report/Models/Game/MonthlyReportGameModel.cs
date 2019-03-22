using System.Collections.Generic;

namespace Domain.Report.Models.Game
{
    public class MonthlyReportGameModel
    {
        public MonthlyReportGameModel()
        {
            MonthlyReportKindGameModels = new List<MonthlyReportKindGameModel>();
        }

        public IList<MonthlyReportKindGameModel> MonthlyReportKindGameModels { get; set; }
    }
}
