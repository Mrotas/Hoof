using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Pasture;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.AnnualPlanStatus.Models;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;
using Domain.Pasture.ViewModels;

namespace Domain.Pasture
{
    public class PastureService : IPastureService
    {
        private readonly IPastureDao _pastureDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public PastureService() : this(new PastureDao(), new MarketingYearService(), new AnnualPlanStatusService())
        {
        }

        public PastureService(IPastureDao pastureDao, IMarketingYearService marketingYearService, IAnnualPlanStatusService annualPlanStatusService)
        {
            _pastureDao = pastureDao;
            _marketingYearService = marketingYearService;
            _annualPlanStatusService = annualPlanStatusService;
        }

        public PastureBaseViewModel GetPastureViewModel(int marketingYearId)
        {
            IList<PastureDto> pastureDtos = _pastureDao.GetByMarketingYear(marketingYearId);

            List<PastureViewModel> pastureViewModels = pastureDtos.Select(x => new PastureViewModel
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

            var pastureBaseViewModel = new PastureBaseViewModel
            {
                PastureViewModels = pastureViewModels,
                MarketingYearModel = marketingYearModel,
                AnnualPlanStatusModel = annualPlanStatusModel
            };

            return pastureBaseViewModel;
        }

        public void AddPasture(PastureViewModel model, int marketingYearId)
        {
            if (model.Section <= 0 || model.District <= 0 || String.IsNullOrWhiteSpace(model.Forestry))
            {
                throw new Exception("Wystąpił nieznany błąd podczas dodawania paśnika.");
            }

            var dto = new PastureDto
            {
                Section = model.Section,
                District = model.District,
                Forestry = model.Forestry,
                Description = model.Description,
                CreatedDate = model.CreatedDate,
                RemovedDate = model.RemovedDate
            };

            _pastureDao.Insert(dto);
        }

        public void UpdatePasture(PastureViewModel model, int marketingYearId)
        {
            if (model.Section <= 0 || model.District <= 0 || String.IsNullOrWhiteSpace(model.Forestry))
            {
                throw new Exception("Wystąpił nieznany błąd podczas edytowania paśnika.");
            }

            var dto = new PastureDto
            {
                Id = model.Id,
                Section = model.Section,
                District = model.District,
                Forestry = model.Forestry,
                Description = model.Description,
                CreatedDate = model.CreatedDate,
                RemovedDate = model.RemovedDate
            };

            _pastureDao.Update(dto);
        }

        public void DeletePasture(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Wystąpił nieznany błąd podczas usuwania paśnika.");
            }

            _pastureDao.Delete(id);
        }
    }
}
