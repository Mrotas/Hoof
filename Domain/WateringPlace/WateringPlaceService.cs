using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.WateringPlace;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.AnnualPlanStatus.Models;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;
using Domain.WateringPlace.ViewModels;

namespace Domain.WateringPlace
{
    public class WateringPlaceService : IWateringPlaceService
    {
        private readonly IWateringPlaceDao _wateringPlaceDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public WateringPlaceService() : this(new WateringPlaceDao(), new MarketingYearService(), new AnnualPlanStatusService())
        {
        }

        public WateringPlaceService(IWateringPlaceDao wateringPlaceDao, IMarketingYearService marketingYearService, IAnnualPlanStatusService annualPlanStatusService)
        {
            _wateringPlaceDao = wateringPlaceDao;
            _marketingYearService = marketingYearService;
            _annualPlanStatusService = annualPlanStatusService;
        }

        public WateringPlaceBaseViewModel GetWateringPlaceViewModel(int marketingYearId)
        {
            IList<WateringPlaceDto> wateringPlaceDtos = _wateringPlaceDao.GetByMarketingYear(marketingYearId);

            List<WateringPlaceViewModel> wateringPlaceViewModels = wateringPlaceDtos.Select(x => new WateringPlaceViewModel
            {
                Id = x.Id,
                Section = x.Section,
                District = x.District,
                Forestry = x.Forestry,
                Description = x.Description,
                CreatedDate = x.CreatedDate,
                RemovedDate = x.RemovedDate
            }).ToList();

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);
            AnnualPlanStatusModel annualPlanStatusModel = _annualPlanStatusService.GetByMarketingYearId(marketingYearId);

            var wateringPlaceBaseViewModel = new WateringPlaceBaseViewModel
            {
                WateringPlaceViewModels = wateringPlaceViewModels,
                MarketingYearModel = marketingYearModel,
                AnnualPlanStatusModel = annualPlanStatusModel
            };

            return wateringPlaceBaseViewModel;
        }

        public void AddWateringPlace(WateringPlaceViewModel model, int marketingYearId)
        {
            if (model.Section <= 0 || model.District <= 0 || String.IsNullOrWhiteSpace(model.Forestry))
            {
                throw new Exception("Wystąpił nieznany błąd podczas dodawania wodopoju.");
            }

            var dto = new WateringPlaceDto
            {
                Section = model.Section,
                District = model.District,
                Forestry = model.Forestry,
                Description = model.Description,
                CreatedDate = model.CreatedDate,
                RemovedDate = model.RemovedDate
            };

            _wateringPlaceDao.Insert(dto);
        }

        public void UpdateWateringPlace(WateringPlaceViewModel model, int marketingYearId)
        {
            if (model.Section <= 0 || model.District <= 0 || String.IsNullOrWhiteSpace(model.Forestry))
            {
                throw new Exception("Wystąpił nieznany błąd podczas edytowania wodopoju.");
            }

            var dto = new WateringPlaceDto
            {
                Id = model.Id,
                Section = model.Section,
                District = model.District,
                Forestry = model.Forestry,
                Description = model.Description,
                CreatedDate = model.CreatedDate,
                RemovedDate = model.RemovedDate
            };

            _wateringPlaceDao.Update(dto);
        }

        public void DeleteWateringPlace(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Wystąpił nieznany błąd podczas usuwania wodopoju.");
            }

            _wateringPlaceDao.Delete(id);
        }
    }
}
