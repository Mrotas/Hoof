using Domain.Pulpit.ViewModels;

namespace Domain.Pulpit
{
    public interface IPulpitService
    {
        PulpitBaseViewModel GetPulpitViewModel(int marketingYearId);
        void AddPulpit(PulpitViewModel model, int marketingYearId);
        void UpdatePulpit(PulpitViewModel model, int marketingYearId);
        void DeletePulpit(int id);
    }
}