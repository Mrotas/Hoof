using DataAccess.Dto;

namespace DataAccess.Dao.FieldPlan
{
    public interface IFieldPlanDao
    {
        FieldPlanDto GetByMarketingYear(int marketingYearId);
        void Insert(FieldPlanDto dto);
        void Update(FieldPlanDto dto);
        void Delete(int marketingYearId);
    }
}