using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Pulpit;
using DataAccess.Dto;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;
using Domain.Pulpit.ViewModels;

namespace Domain.Pulpit
{
    public class PulpitService : IPulpitService
    {
        private readonly IPulpitDao _pulpitDao;
        private readonly IMarketingYearService _marketingYearService;

        public PulpitService() : this(new PulpitDao(), new MarketingYearService())
        {
        }

        public PulpitService(IPulpitDao pulpitDao, IMarketingYearService marketingYearService)
        {
            _pulpitDao = pulpitDao;
            _marketingYearService = marketingYearService;
        }

        public PulpitBaseViewModel GetPulpitViewModel(int marketingYearId)
        {
            IList<PulpitDto> pulpitDtos = _pulpitDao.GetByMarketingYear(marketingYearId);

            List<PulpitViewModel> pulpitViewModels = pulpitDtos.Select(x => new PulpitViewModel
            {
                Id = x.Id,
                Number = x.Number,
                Section = x.Section,
                District = x.District,
                Forestry = x.Forestry,
                HasRoof = x.HasRoof,
                Description = x.Description,
                CreatedDate = x.CreatedDate,
                RemovedDate = x.RemovedDate
            }).ToList();

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);

            var pulpitBaseViewModel = new PulpitBaseViewModel
            {
                PulpitViewModels = pulpitViewModels,
                MarketingYearModel = marketingYearModel
            };

            return pulpitBaseViewModel;
        }

        public void AddPulpit(PulpitViewModel model, int marketingYearId)
        {
            if (String.IsNullOrWhiteSpace(model.Number) || model.Section <= 0 || String.IsNullOrWhiteSpace(model.District) ||
                String.IsNullOrWhiteSpace(model.Forestry))
            {
                throw new Exception("Wystąpił nieznany błąd podczas dodawania ambony.");
            }

            var dto = new PulpitDto
            {
                Number = model.Number,
                Section = model.Section,
                District = model.District,
                Forestry = model.Forestry,
                HasRoof = model.HasRoof,
                Description = model.Description,
                CreatedDate = model.CreatedDate,
                RemovedDate = model.RemovedDate
            };

            _pulpitDao.Insert(dto);
        }

        public void UpdatePulpit(PulpitViewModel model, int marketingYearId)
        {
            if (String.IsNullOrWhiteSpace(model.Number) || model.Section <= 0 || String.IsNullOrWhiteSpace(model.District) ||
                String.IsNullOrWhiteSpace(model.Forestry))
            {
                throw new Exception("Wystąpił nieznany błąd podczas edycji ambony.");
            }

            var dto = new PulpitDto
            {
                Id = model.Id,
                Number = model.Number,
                Section = model.Section,
                District = model.District,
                Forestry = model.Forestry,
                HasRoof = model.HasRoof,
                Description = model.Description,
                CreatedDate = model.CreatedDate,
                RemovedDate = model.RemovedDate
            };

            _pulpitDao.Update(dto);
        }

        public void DeletePulpit(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Wystąpił nieznany błąd podczas usuwania ambony.");
            }

            _pulpitDao.Delete(id);
        }
    }
}
