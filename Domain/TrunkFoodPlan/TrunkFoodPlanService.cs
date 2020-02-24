using System;
using DataAccess.Dao.TrunkFoodPlan;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.AnnualPlanStatus.Models;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;
using Domain.TrunkFoodPlan.Models;
using Domain.TrunkFoodPlan.ViewModels;

namespace Domain.TrunkFoodPlan
{
    public class TrunkFoodPlanService : ITrunkFoodPlanService
    {
        private readonly ITrunkFoodPlanDao _trunkFoodPlanDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public TrunkFoodPlanService() : this(new TrunkFoodPlanDao(), new MarketingYearService(), new AnnualPlanStatusService())
        {
        }

        public TrunkFoodPlanService(ITrunkFoodPlanDao trunkFoodPlanDao, IMarketingYearService marketingYearService, IAnnualPlanStatusService annualPlanStatusService)
        {
            _trunkFoodPlanDao = trunkFoodPlanDao;
            _marketingYearService = marketingYearService;
            _annualPlanStatusService = annualPlanStatusService;
        }

        public TrunkFoodPlanViewModel GetTrunkFoodPlanViewModel(int marketingYearId)
        {
            TrunkFoodPlanDto trunkFoodPlanDto = _trunkFoodPlanDao.GetByMarketingYear(marketingYearId);
            TrunkFoodPlanModel trunkFoodPlanModel = null;
            if (trunkFoodPlanDto != null)
            {
                trunkFoodPlanModel = new TrunkFoodPlanModel
                {
                    Hectare = trunkFoodPlanDto.Hectare
                };
            }

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);
            AnnualPlanStatusModel annualPlanStatusModel = _annualPlanStatusService.GetByMarketingYearId(marketingYearId);

            var trunkFoodPlanViewModel = new TrunkFoodPlanViewModel
            {
                TrunkFoodPlanModel = trunkFoodPlanModel,
                MarketingYearModel = marketingYearModel,
                AnnualPlanStatusModel = annualPlanStatusModel
            };

            return trunkFoodPlanViewModel;
        }

        public void AddTrunkFoodPlan(TrunkFoodPlanModel model, int marketingYearId)
        {
            TrunkFoodPlanDto existingTrunkFoodPlanDto = _trunkFoodPlanDao.GetByMarketingYear(marketingYearId);
            if (existingTrunkFoodPlanDto != null)
            {
                throw new Exception("Plan gospodarczy obszarów stanowiących żer dla zwierzyny już istnieje! Proszę użyć opcji edycji istniejącego już planu.");
            }

            var dto = new TrunkFoodPlanDto
            {
                Hectare = model.Hectare,
                MarketingYearId = marketingYearId
            };

            _trunkFoodPlanDao.Insert(dto);
        }

        public void UpdateTrunkFoodPlan(TrunkFoodPlanModel model, int marketingYearId)
        {
            var dto = new TrunkFoodPlanDto
            {
                Hectare = model.Hectare,
                MarketingYearId = marketingYearId
            };

            _trunkFoodPlanDao.Update(dto);
        }

        public void DeleteTrunkFoodPlan(int marketingYearId)
        {
            _trunkFoodPlanDao.Delete(marketingYearId);
        }
    }
}
