using Domain.CarcassRevenue.ViewModels;

namespace Domain.CarcassRevenue
{
    public interface ICarcassRevenueService
    {
        CarcassRevenueBaseViewModel GetCarcassRevenueViewModel(int marketingYearId);
        void AddCarcassRevenue(CarcassRevenueViewModel model, int marketingYearId);
        void UpdateCarcassRevenue(CarcassRevenueViewModel model, int marketingYearId);
        void DeleteCarcassRevenue(int id);
    }
}