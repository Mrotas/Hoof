namespace Domain.AnnualPlan.Models
{
    public class CostPlanModel
    {
        private double _cost;
        public double Cost
        {
            get => _cost / 1000;
            set => _cost = value;
        }

        private double _revenue;

        public double Revenue
        {
            get => _revenue / 1000;
            set => _revenue = value;
        }
    }
}
