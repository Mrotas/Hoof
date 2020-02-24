using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using DataAccess.Dao.Fodder;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.AnnualPlanStatus.Models;
using Domain.Fodder.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Domain.Fodder
{
    public class FodderService : IFodderService
    {
        private readonly IFodderDao _fodderDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public FodderService() : this(new FodderDao(), new MarketingYearService(), new AnnualPlanStatusService())
        {
        }

        public FodderService(IFodderDao fodderDao, IMarketingYearService marketingYearService, IAnnualPlanStatusService annualPlanStatusService)
        {
            _fodderDao = fodderDao;
            _marketingYearService = marketingYearService;
            _annualPlanStatusService = annualPlanStatusService;
        }

        public FodderBaseViewModel GetFodderViewModel(int marketingYearId)
        {
            IList<FodderDto> fodderDtos = _fodderDao.GetByMarketingYear(marketingYearId);

            List<FodderViewModel> fodderViewModels = fodderDtos.Select(x => new FodderViewModel
            {
                Id = x.Id,
                Type = x.Type,
                TypeName = TypeName.GetFodderTypeName(x.Type),
                Kilograms = x.Kilograms,
                Owner = x.Owner,
                Description = x.Description,
                Date = x.Date
            }).ToList();

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);
            AnnualPlanStatusModel annualPlanStatusModel = _annualPlanStatusService.GetByMarketingYearId(marketingYearId);

            var fodderPlanViewBaseModel = new FodderBaseViewModel
            {
                FodderViewModels = fodderViewModels,
                MarketingYearModel = marketingYearModel,
                AnnualPlanStatusModel = annualPlanStatusModel
            };

            return fodderPlanViewBaseModel;
        }

        public void AddFodder(FodderViewModel model, int marketingYearId)
        {
            if (model.Type <= 0)
            {
                throw new Exception("Wystąpił nieznany błąd podczas dodawania karmy.");
            }
            
            var dto = new FodderDto
            {
                Type = model.Type,
                Kilograms = model.Kilograms,
                Owner = model.Owner,
                Description = model.Description,
                Date = model.Date
            };

            _fodderDao.Insert(dto);
        }

        public void UpdateFodder(FodderViewModel model, int marketingYearId)
        {
            if (model.Type <= 0)
            {
                throw new Exception("Wystąpił nieznany błąd podczas dodawania karmy.");
            }

            var dto = new FodderDto
            {
                Id = model.Id,
                Type = model.Type,
                Kilograms = model.Kilograms,
                Owner = model.Owner,
                Description = model.Description,
                Date = model.Date
            };

            _fodderDao.Update(dto);
        }

        public void DeleteFodder(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Wystąpił nieznany błąd podczas dodawania karmy.");
            }

            _fodderDao.Delete(id);
        }
    }
}
