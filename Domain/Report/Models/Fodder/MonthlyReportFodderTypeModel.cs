using Common.Enums;

namespace Domain.Report.Models.Fodder
{
    public class MonthlyReportFodderTypeModel
    {
        public FodderType FodderType { get; set; }
        public string FodderTypeName { get; set; }
        public double PutOut { get; set; }
    }
}
