using Domain.Fodder.ViewModels;

namespace Domain.Fodder
{
    public interface IFodderService
    {
        FodderBaseViewModel GetFodderViewModel(int marketingYearId);
        void AddFodder(FodderViewModel model, int marketingYearId);
        void UpdateFodder(FodderViewModel model, int marketingYearId);
        void DeleteFodder(int id);
    }
}