using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using DataAccess.Dao.FodderPlan;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.AnnualPlanStatus.Models;
using Domain.FodderPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Domain.FodderPlan
{
    public class FodderPlanService : IFodderPlanService
    {
        private readonly IFodderPlanDao _fodderPlanDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public FodderPlanService() : this(new FodderPlanDao(), new MarketingYearService(), new AnnualPlanStatusService())
        {
        }

        public FodderPlanService(IFodderPlanDao fodderPlanDao, IMarketingYearService marketingYearService, IAnnualPlanStatusService annualPlanStatusService)
        {
            _fodderPlanDao = fodderPlanDao;
            _annualPlanStatusService = annualPlanStatusService;
            _marketingYearService = marketingYearService;
        }

        public FodderPlanBaseViewModel GetFodderPlanViewModel(int marketingYearId)
        {
            IList<FodderPlanDto> fodderPlanDtos = _fodderPlanDao.GetByMarketingYear(marketingYearId);

            List<FodderPlanViewModel> fodderPlanViewModels =
            (
                from fodderPlan in fodderPlanDtos
                where fodderPlan.MarketingYearId == marketingYearId
                select new FodderPlanViewModel
                {
                    Id = fodderPlan.Id,
                    Type = fodderPlan.Type,
                    TypeName = TypeName.GetFodderTypeName(fodderPlan.Type),
                    Ton = fodderPlan.Ton
                }
            ).ToList();

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);
            AnnualPlanStatusModel annualPlanStatusModel = _annualPlanStatusService.GetByMarketingYearId(marketingYearId);

            var fodderPlanViewBaseModel = new FodderPlanBaseViewModel
            {
                FodderPlanViewModels = fodderPlanViewModels,
                MarketingYearModel = marketingYearModel,
                AnnualPlanStatusModel = annualPlanStatusModel
            };

            return fodderPlanViewBaseModel;
        }

        public void AddFodderPlan(FodderPlanViewModel model, int marketingYearId)
        {
            if (model.Type == 0)
            {
                throw new Exception("Nie można dodać planu gospodarczego karmy");
            }

            IList<FodderPlanDto> existingEquipmentPlanDtos = _fodderPlanDao.GetByMarketingYear(marketingYearId);
            if (existingEquipmentPlanDtos.Any(x => x.Type == model.Type))
            {
                throw new Exception($"Plan gospodarczy karmy {TypeName.GetFodderTypeName(model.Type)} już istnieje! Proszę użyć opcji edycji istniejącego już planu.");
            }

            var dto = new FodderPlanDto
            {
                Type = model.Type,
                Ton = model.Ton,
                MarketingYearId = marketingYearId
            };

            _fodderPlanDao.Insert(dto);
        }

        public void UpdateFodderPlan(FodderPlanViewModel model, int marketingYearId)
        {
            if (model.Type <= 0)
            {
                throw new Exception("Nie można edytować planu gospodarczego karmy");
            }

            var dto = new FodderPlanDto
            {
                Type = model.Type,
                Ton = model.Ton,
                MarketingYearId = marketingYearId
            };

            _fodderPlanDao.Update(dto);
        }

        public void DeleteFodderPlan(int fodderType, int marketingYearId)
        {
            if (fodderType <= 0)
            {
                throw new Exception("Nie można usunąć planu gospodarczego karmy");
            }

            _fodderPlanDao.Delete(fodderType, marketingYearId);
        }
    }
}
