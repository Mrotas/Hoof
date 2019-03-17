using Domain.Pasture.ViewModels;

namespace Domain.Pasture
{
    public interface IPastureService
    {
        PastureBaseViewModel GetPastureViewModel(int marketingYearId);
        void AddPasture(PastureViewModel model, int marketingYearId);
        void UpdatePasture(PastureViewModel model, int marketingYearId);
        void DeletePasture(int id);
    }
}