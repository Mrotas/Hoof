using Domain.DeerLicker.ViewModels;

namespace Domain.DeerLicker
{
    public interface IDeerLickerService
    {
        DeerLickerBaseViewModel GetDeerLickerViewModel(int marketingYearId);
        void AddDeerLicker(DeerLickerViewModel model, int marketingYearId);
        void UpdateDeerLicker(DeerLickerViewModel model, int marketingYearId);
        void DeleteDeerLicker(int id);
    }
}