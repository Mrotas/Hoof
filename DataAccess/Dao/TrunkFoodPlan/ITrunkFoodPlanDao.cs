using DataAccess.Dto;

namespace DataAccess.Dao.TrunkFoodPlan
{
    public interface ITrunkFoodPlanDao
    {
        TrunkFoodPlanDto GetByMarketingYear(int marketingYearId);
        void Insert(TrunkFoodPlanDto dto);
        void Update(TrunkFoodPlanDto dto);
        void Delete(int marketingYearId);
    }
}