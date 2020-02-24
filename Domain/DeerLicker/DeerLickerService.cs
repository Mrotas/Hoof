using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.DeerLicker;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.AnnualPlanStatus.Models;
using Domain.DeerLicker.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Domain.DeerLicker
{
    public class DeerLickerService : IDeerLickerService
    {
        private readonly IDeerLickerDao _deerLickerDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public DeerLickerService() : this(new DeerLickerDao(), new MarketingYearService(), new AnnualPlanStatusService())
        {
        }

        public DeerLickerService(IDeerLickerDao deerLickerDao, IMarketingYearService marketingYearService, IAnnualPlanStatusService annualPlanStatusService)
        {
            _deerLickerDao = deerLickerDao;
            _marketingYearService = marketingYearService;
            _annualPlanStatusService = annualPlanStatusService;
        }

        public DeerLickerBaseViewModel GetDeerLickerViewModel(int marketingYearId)
        {
            IList<DeerLickerDto> deerLickerDtos = _deerLickerDao.GetByMarketingYear(marketingYearId);

            List<DeerLickerViewModel> deerLickerViewModels = deerLickerDtos.Select(x => new DeerLickerViewModel
            {
                Id = x.Id,
                Count = x.Count,
                Section = x.Section,
                District = x.District,
                Forestry = x.Forestry,
                Description = x.Description
            }).ToList();

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);
            AnnualPlanStatusModel annualPlanStatusModel = _annualPlanStatusService.GetByMarketingYearId(marketingYearId);

            var pastureBaseViewModel = new DeerLickerBaseViewModel
            {
                DeerLickerViewModels = deerLickerViewModels,
                MarketingYearModel = marketingYearModel,
                AnnualPlanStatusModel = annualPlanStatusModel
            };

            return pastureBaseViewModel;
        }

        public void AddDeerLicker(DeerLickerViewModel model, int marketingYearId)
        {
            if (model.Section <= 0 || model.District <= 0 || String.IsNullOrWhiteSpace(model.Forestry))
            {
                throw new Exception("Wystąpił nieznany błąd podczas dodawania lizawek.");
            }

            var dto = new DeerLickerDto
            {
                Count = model.Count,
                Section = model.Section,
                District = model.District,
                Forestry = model.Forestry,
                Description = model.Description,
                MarketingYearId = marketingYearId
            };

            _deerLickerDao.Insert(dto);
        }

        public void UpdateDeerLicker(DeerLickerViewModel model, int marketingYearId)
        {
            if (model.Section <= 0 || model.District <= 0 || String.IsNullOrWhiteSpace(model.Forestry))
            {
                throw new Exception("Wystąpił nieznany błąd podczas edytowania lizawek.");
            }

            var dto = new DeerLickerDto
            {
                Id = model.Id,
                Count = model.Count,
                Section = model.Section,
                District = model.District,
                Forestry = model.Forestry,
                Description = model.Description
            };

            _deerLickerDao.Update(dto);
        }

        public void DeleteDeerLicker(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Wystąpił nieznany błąd podczas usuwania lizawek.");
            }

            _deerLickerDao.Delete(id);
        }
    }
}
