using Domain.Labor.ViewModels;

namespace Domain.Labor
{
    public interface ILaborService
    {
        LaborBaseViewModel GetLaborViewModel(int marketingYearId);
        void AddLabor(LaborViewModel model, int marketingYearId);
        void UpdateLabor(LaborViewModel model, int marketingYearId);
        void DeleteLabor(int id);
    }
}