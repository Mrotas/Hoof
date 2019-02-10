using System.Collections.Generic;

namespace Domain.Report.Models
{
    public class MonthlyReportKindGameModel : MonthlyReportGameBaseModel
    {
        public MonthlyReportKindGameModel()
        {
            MonthlyReportSubKindGameModels = new List<MonthlyReportSubKindGameModel>();
        }

        public int Kind { get; set; }
        public string KindName { get; set; }
        public IList<MonthlyReportSubKindGameModel> MonthlyReportSubKindGameModels { get; set; }
    }
}
