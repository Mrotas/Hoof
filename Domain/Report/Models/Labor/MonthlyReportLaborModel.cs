using System.Collections.Generic;
using Domain.Labor.Models;

namespace Domain.Report.Models.Labor
{
    public class MonthlyReportLaborModel
    {
        public IList<LaborModel> LaborModels { get; set; }
    }
}
