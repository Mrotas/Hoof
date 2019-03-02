using Domain.FieldPlan.Models;
using Domain.FieldPlan.ViewModels;

namespace Domain.FieldPlan
{
    public interface IFieldPlanService
    {
        FieldPlanViewModel GetFieldPlanViewModel(int marketingYearId);
        void AddFieldPlan(FiledPlanModel model, int marketingYearId);
        void UpdateFieldPlan(FiledPlanModel model, int marketingYearId);
        void DeleteFieldPlan(int marketingYearId);
    }
}