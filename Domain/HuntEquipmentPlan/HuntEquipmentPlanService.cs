using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.HuntEquipmentPlan;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.HuntEquipmentPlan.ViewModels;

namespace Domain.HuntEquipmentPlan
{
    public class HuntEquipmentPlanService : IHuntEquipmentPlanService
    {
        private readonly IHuntEquipmentPlanDao _huntEquipmentPlanDao;
        private readonly IMarketingYearDao _marketingYearDao;

        public HuntEquipmentPlanService() : this(new HuntEquipmentPlanDao(), new MarketingYearDao())
        {
        }

        public HuntEquipmentPlanService(IHuntEquipmentPlanDao huntEquipmentPlanDao, IMarketingYearDao marketingYearDao)
        {
            _huntEquipmentPlanDao = huntEquipmentPlanDao;
            _marketingYearDao = marketingYearDao;
        }

        public HuntEquipmentPlanBaseViewModel GetHuntEquipmentPlanViewModel(int marketingYearId)
        {
            IList<HuntEquipmentPlanDto> huntEquipmentPlanDtos = _huntEquipmentPlanDao.GetByMarketingYear(marketingYearId);

            List<HuntEquipmentPlanViewModel> huntEquipmentPlanViewModels = 
            (
                from huntEquipmentPlan in huntEquipmentPlanDtos
                where huntEquipmentPlan.MarketingYearId == marketingYearId
                select new HuntEquipmentPlanViewModel
                {
                    Id = huntEquipmentPlan.Id,
                    Type = huntEquipmentPlan.Type,
                    TypeName = GetHuntEquipmentTypeName(huntEquipmentPlan.Type),
                    Count = huntEquipmentPlan.Count
                }
            ).ToList();

            DataAccess.Entities.MarketingYear marketingYear = _marketingYearDao.GetById(marketingYearId);

            var huntEquipmentPlanBaseViewModel = new HuntEquipmentPlanBaseViewModel
            {
                HuntEquipmentPlanViewModels = huntEquipmentPlanViewModels,
                MarketingYearId = marketingYearId,
                MarketingYearStart = marketingYear.Start,
                MarketingYearEnd = marketingYear.End
            };

            return huntEquipmentPlanBaseViewModel;
        }

        public void AddHuntEquipmentPlan(HuntEquipmentPlanViewModel model, int marketingYearId)
        {
            if (model.Type == 0)
            {
                throw new Exception($"Nie można dodać planu urządzenia łowieckiego");
            }

            IList<HuntEquipmentPlanDto> existingEquipmentPlanDtos = _huntEquipmentPlanDao.GetByMarketingYear(marketingYearId);
            if (existingEquipmentPlanDtos.Any(x => x.Type == model.Type))
            {
                throw new Exception($"Plan urządzenia łowieckiego {GetHuntEquipmentTypeName(model.Type)} już istnieje! Proszę użyć opcji edycji istniejącego już planu.");
            }

            var dto = new HuntEquipmentPlanDto
            {
                Type = model.Type,
                Count = model.Count,
                MarketingYearId = marketingYearId
            };

            _huntEquipmentPlanDao.Insert(dto);
        }

        public void UpdateHuntEquipmentPlan(HuntEquipmentPlanViewModel model, int marketingYearId)
        {
            if (model.Type <= 0)
            {
                throw new Exception($"Nie można edytować planu urządzenia łowieckiego");
            }
            
            var dto = new HuntEquipmentPlanDto
            {
                Type = model.Type,
                Count = model.Count,
                MarketingYearId = marketingYearId
            };

            _huntEquipmentPlanDao.Update(dto);
        }

        private string GetHuntEquipmentTypeName(int huntEquipmentType)
        {
            switch (huntEquipmentType)
            {
                case 1: return "Paśniki";
                case 2: return "Lizawki";
                case 3: return "Ambony";
                case 4: return "Woliery";
                case 5: return "Zagrody";
                case 6: return "Wodopoje";
                default: throw new NotImplementedException();
            }
        }
    }
}
