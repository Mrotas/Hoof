using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.FodderPlan;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.FodderPlan.ViewModels;

namespace Domain.FodderPlan
{
    public class FodderPlanService : IFodderPlanService
    {
        private readonly IFodderPlanDao _fodderPlanDao;
        private readonly IMarketingYearDao _marketingYearDao;

        public FodderPlanService() : this(new FodderPlanDao(), new MarketingYearDao())
        {
        }

        public FodderPlanService(IFodderPlanDao fodderPlanDao, IMarketingYearDao marketingYearDao)
        {
            _fodderPlanDao = fodderPlanDao;
            _marketingYearDao = marketingYearDao;
        }

        public FodderPlanViewBaseModel GetFodderPlanViewModel(int marketingYearId)
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
                    TypeName = GetFodderTypeName(fodderPlan.Type),
                    Ton = fodderPlan.Ton
                }
            ).ToList();

            DataAccess.Entities.MarketingYear marketingYear = _marketingYearDao.GetById(marketingYearId);

            var fodderPlanViewBaseModel = new FodderPlanViewBaseModel
            {
                HuntEquipmentPlanViewModels = fodderPlanViewModels,
                MarketingYearId = marketingYearId,
                MarketingYearStart = marketingYear.Start,
                MarketingYearEnd = marketingYear.End
            };

            return fodderPlanViewBaseModel;
        }

        private string GetFodderTypeName(int fodderPlanType)
        {
            switch (fodderPlanType)
            {
                case 1: return "Objętościowa sucha";
                case 2: return "Objętościowa soczysta";
                case 3: return "Treściwa";
                case 4: return "Sól";
                default: throw new System.NotImplementedException();
            }
        }
    }
}
