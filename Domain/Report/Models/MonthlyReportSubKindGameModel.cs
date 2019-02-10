using System.Collections.Generic;

namespace Domain.Report.Models
{
    public class MonthlyReportSubKindGameModel : MonthlyReportGameBaseModel
    {
        public MonthlyReportSubKindGameModel()
        {
            MonthlyReportClassGameModels = new List<MonthlyReportClassGameModel>();
        }

        public int? SubKind { get; set; }
        public string SubKindName { get; set; }
        public IList<MonthlyReportClassGameModel> MonthlyReportClassGameModels { get; set; }
    }
}
