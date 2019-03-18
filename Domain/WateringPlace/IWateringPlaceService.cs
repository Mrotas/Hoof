using Domain.WateringPlace.ViewModels;

namespace Domain.WateringPlace
{
    public interface IWateringPlaceService
    {
        WateringPlaceBaseViewModel GetWateringPlaceViewModel(int marketingYearId);
        void AddWateringPlace(WateringPlaceViewModel model, int marketingYearId);
        void UpdateWateringPlace(WateringPlaceViewModel model, int marketingYearId);
        void DeleteWateringPlace(int id);
    }
}