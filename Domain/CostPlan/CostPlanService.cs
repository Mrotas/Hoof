using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.CostPlan;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.AnnualPlanStatus.Models;
using Domain.CostPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Domain.CostPlan
{
    public class CostPlanService : ICostPlanService
    {
        private readonly ICostPlanDao _costPlanDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public CostPlanService() : this(new CostPlanDao(), new MarketingYearService(), new AnnualPlanStatusService())
        {
        }

        public CostPlanService(ICostPlanDao costPlanDao, IMarketingYearService marketingYearService, IAnnualPlanStatusService annualPlanStatusService)
        {
            _costPlanDao = costPlanDao;
            _marketingYearService = marketingYearService;
            _annualPlanStatusService = annualPlanStatusService;
        }

        public CostPlanBaseViewModel GetCostPlanViewModel(int marketingYearId)
        {
            IList<CostPlanDto> costPlanDtos = _costPlanDao.GetByMarketingYear(marketingYearId);

            List<CostPlanViewModel> costPlanViewModels =
            (
                from costPlan in costPlanDtos
                where costPlan.MarketingYearId == marketingYearId
                select new CostPlanViewModel
                {
                    Id = costPlan.Id,
                    Type = costPlan.Type,
                    TypeName = GetCostTypeName(costPlan.Type),
                    Cost = costPlan.Cost
                }
            ).ToList();

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);
            AnnualPlanStatusModel annualPlanStatusModel = _annualPlanStatusService.GetByMarketingYearId(marketingYearId);

            var costPlanViewBaseModel = new CostPlanBaseViewModel
            {
                CostPlanViewModels = costPlanViewModels,
                MarketingYearModel = marketingYearModel,
                AnnualPlanStatusModel = annualPlanStatusModel
            };

            return costPlanViewBaseModel;
        }

        public void AddCostPlan(CostPlanViewModel model, int marketingYearId)
        {
            if (model.Type == 0)
            {
                throw new Exception("Nie można dodać planu kosztów koła łowieckiego");
            }

            IList<CostPlanDto> existingEquipmentPlanDtos = _costPlanDao.GetByMarketingYear(marketingYearId);
            if (existingEquipmentPlanDtos.Any(x => x.Type == model.Type))
            {
                throw new Exception($"Plan {GetCostTypeName(model.Type)} już istnieje! Proszę użyć opcji edycji istniejącego już planu.");
            }

            var dto = new CostPlanDto
            {
                Type = model.Type,
                Cost = model.Cost,
                MarketingYearId = marketingYearId
            };

            _costPlanDao.Insert(dto);
        }

        public void UpdateCostPlan(CostPlanViewModel model, int marketingYearId)
        {
            if (model.Type <= 0)
            {
                throw new Exception("Nie można edytować planu kosztów koła łowieckiego");
            }

            var dto = new CostPlanDto
            {
                Type = model.Type,
                Cost = model.Cost,
                MarketingYearId = marketingYearId
            };

            _costPlanDao.Update(dto);
        }

        public void DeleteCostPlan(int costType, int marketingYearId)
        {
            if (costType <= 0)
            {
                throw new Exception("Nie można usunąć planu kosztów koła łowieckiego");
            }

            _costPlanDao.Delete(costType, marketingYearId);
        }

        private string GetCostTypeName(int costType)
        {
            switch (costType)
            {
                case 1: return "Koszt";
                case 2: return "Przychód";
                default: throw new NotImplementedException();
            }
        }
    }
}
