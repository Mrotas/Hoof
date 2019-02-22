using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.CostPlan;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.CostPlan.ViewModels;

namespace Domain.CostPlan
{
    public class CostPlanService : ICostPlanService
    {
        private readonly ICostPlanDao _costPlanDao;
        private readonly IMarketingYearDao _marketingYearDao;

        public CostPlanService() : this(new CostPlanDao(), new MarketingYearDao())
        {
        }

        public CostPlanService(ICostPlanDao costPlanDao, IMarketingYearDao marketingYearDao)
        {
            _costPlanDao = costPlanDao;
            _marketingYearDao = marketingYearDao;
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

            DataAccess.Entities.MarketingYear marketingYear = _marketingYearDao.GetById(marketingYearId);

            var costPlanViewBaseModel = new CostPlanBaseViewModel
            {
                CostPlanViewModels = costPlanViewModels,
                MarketingYearId = marketingYearId,
                MarketingYearStart = marketingYear.Start,
                MarketingYearEnd = marketingYear.End
            };

            return costPlanViewBaseModel;
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
