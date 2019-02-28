using System;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dao.TrunkFoodPlan;
using DataAccess.Dto;
using Domain.TrunkFoodPlan.Models;
using Domain.TrunkFoodPlan.ViewModels;

namespace Domain.TrunkFoodPlan
{
    public class TrunkFoodPlanService : ITrunkFoodPlanService
    {
        private readonly ITrunkFoodPlanDao _trunkFoodPlanDao;
        private readonly IMarketingYearDao _marketingYearDao;

        public TrunkFoodPlanService() : this(new TrunkFoodPlanDao(), new MarketingYearDao())
        {
        }

        public TrunkFoodPlanService(ITrunkFoodPlanDao trunkFoodPlanDao, IMarketingYearDao marketingYearDao)
        {
            _trunkFoodPlanDao = trunkFoodPlanDao;
            _marketingYearDao = marketingYearDao;
        }

        public TrunkFoodPlanViewModel GetTrunkFoodPlanViewModel(int marketingYearId)
        {
            TrunkFoodPlanDto trunkFoodPlanDto = _trunkFoodPlanDao.GetByMarketingYear(marketingYearId);
            
            DataAccess.Entities.MarketingYear marketingYear = _marketingYearDao.GetById(marketingYearId);

            TrunkFoodPlanModel trunkFoodPlanModel = null;
            if (trunkFoodPlanDto != null)
            {
                trunkFoodPlanModel = new TrunkFoodPlanModel
                {
                    Hectare = trunkFoodPlanDto.Hectare
                };
            }

            var trunkFoodPlanViewModel = new TrunkFoodPlanViewModel
            {
                TrunkFoodPlanModel = trunkFoodPlanModel,
                MarketingYearId = marketingYear.Id,
                MarketingYearStart = marketingYear.Start,
                MarketingYearEnd = marketingYear.End
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
