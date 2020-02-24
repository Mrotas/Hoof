using DataAccess.Dto;

namespace DataAccess.Dao.AnnualPlan
{
    public interface IAnnualPlanStatusDao
    {
        AnnualPlanStatusDto GetByMarketingYearId(int marketingYearId);
        void Update(AnnualPlanStatusDto annualPlanStatusDto);
        void Insert(AnnualPlanStatusDto annualPlanStatusDto);
    }
}