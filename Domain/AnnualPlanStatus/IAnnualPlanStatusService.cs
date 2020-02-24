using DataAccess.Dto;
using Domain.AnnualPlanStatus.Models;

namespace Domain.AnnualPlanStatus
{
    public interface IAnnualPlanStatusService
    {
        AnnualPlanStatusModel GetByMarketingYearId(int marketingYearId);
        void ApproveToNextStatus(int marketingYearId);
        void RejectAnnualPlan(int marketingYearId);
        void Create(AnnualPlanStatusDto annualPlanStatusDto);
    }
}