using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.HuntEquipmentPlan;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.AnnualPlanStatus.Models;
using Domain.HuntEquipmentPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Domain.HuntEquipmentPlan
{
    public class HuntEquipmentPlanService : IHuntEquipmentPlanService
    {
        private readonly IHuntEquipmentPlanDao _huntEquipmentPlanDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public HuntEquipmentPlanService() : this(new HuntEquipmentPlanDao(), new MarketingYearService(), new AnnualPlanStatusService())
        {
        }

        public HuntEquipmentPlanService(IHuntEquipmentPlanDao huntEquipmentPlanDao, IMarketingYearService marketingYearService, IAnnualPlanStatusService annualPlanStatusService)
        {
            _huntEquipmentPlanDao = huntEquipmentPlanDao;
            _marketingYearService = marketingYearService;
            _annualPlanStatusService = annualPlanStatusService;
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

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);
            AnnualPlanStatusModel annualPlanStatusModel = _annualPlanStatusService.GetByMarketingYearId(marketingYearId);

            var huntEquipmentPlanBaseViewModel = new HuntEquipmentPlanBaseViewModel
            {
                HuntEquipmentPlanViewModels = huntEquipmentPlanViewModels,
                MarketingYearModel = marketingYearModel,
                AnnualPlanStatusModel = annualPlanStatusModel
            };

            return huntEquipmentPlanBaseViewModel;
        }

        public void AddHuntEquipmentPlan(HuntEquipmentPlanViewModel model, int marketingYearId)
        {
            if (model.Type == 0)
            {
                throw new Exception("Nie można dodać planu urządzenia łowieckiego");
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
                throw new Exception("Nie można edytować planu urządzenia łowieckiego");
            }
            
            var dto = new HuntEquipmentPlanDto
            {
                Type = model.Type,
                Count = model.Count,
                MarketingYearId = marketingYearId
            };

            _huntEquipmentPlanDao.Update(dto);
        }

        public void DeleteHuntEquipmentPlan(int huntEquipmentType, int marketingYearId)
        {
            if (huntEquipmentType <= 0)
            {
                throw new Exception("Nie można usunąć planu urządzenia łowieckiego");
            }
            
            _huntEquipmentPlanDao.Delete(huntEquipmentType, marketingYearId);
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
