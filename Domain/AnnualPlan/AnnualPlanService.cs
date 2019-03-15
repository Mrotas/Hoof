﻿using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Extensions;
using DataAccess.Dao.CostPlan;
using DataAccess.Dao.EmploymentPlan;
using DataAccess.Dao.FieldPlan;
using DataAccess.Dao.Fodder;
using DataAccess.Dao.FodderPlan;
using DataAccess.Dao.HuntEquipmentPlan;
using DataAccess.Dao.TrunkFoodPlan;
using DataAccess.Dto;
using Domain.AnnualPlan.Models;
using Domain.AnnualPlan.Models.Fodder;
using Domain.AnnualPlan.ViewModels;
using Domain.GamePlan;
using Domain.MarketingYear;

namespace Domain.AnnualPlan
{
    public class AnnualPlanService : IAnnualPlanService
    {
        private readonly IMarketingYearService _marketingYearService;
        private readonly IEmploymentPlanDao _employeePlanDao;
        private readonly IHuntEquipmentPlanDao _huntEquipmentPlanDao;
        private readonly ITrunkFoodPlanDao _trunkFoodPlanDao;
        private readonly IFieldPlanDao _fieldPlanDao;
        private readonly IFodderPlanDao _fodderPlanDao;
        private readonly IFodderDao _fodderDao;
        private readonly ICostPlanDao _costPlanDao;
        private readonly IGamePlanService _gamePlanService;

        public AnnualPlanService() : this(new EmploymentPlanDao(), 
            new HuntEquipmentPlanDao(), 
            new TrunkFoodPlanDao(), 
            new FieldPlanDao(),
            new FodderPlanDao(),
            new FodderDao(), 
            new CostPlanDao(), 
            new GamePlanService(), 
            new MarketingYearService())
        {
        }

        public AnnualPlanService(IEmploymentPlanDao employeePlanDao, 
            IHuntEquipmentPlanDao huntEquipmentPlanDao, 
            ITrunkFoodPlanDao trunkFoodPlanDao, 
            IFieldPlanDao fieldPlanDao, 
            IFodderPlanDao fodderPlanDao,
            IFodderDao fodderDao,
            ICostPlanDao costPlanDao, 
            IGamePlanService gamePlanService,
            IMarketingYearService marketingYearService)
        {
            _employeePlanDao = employeePlanDao;
            _huntEquipmentPlanDao = huntEquipmentPlanDao;
            _trunkFoodPlanDao = trunkFoodPlanDao;
            _fieldPlanDao = fieldPlanDao;
            _fodderPlanDao = fodderPlanDao;
            _fodderDao = fodderDao;
            _costPlanDao = costPlanDao;
            _gamePlanService = gamePlanService;
            _marketingYearService = marketingYearService;
        }

        public AnnualPlanViewModel GetAnnualPlanViewModel(int marketingYearId)
        {
            int previousMarketingYearId = marketingYearId - 1;

            var annualPlanViewModel = new AnnualPlanViewModel();

            annualPlanViewModel.CurrentMarketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);
            annualPlanViewModel.PreviousMarketingYearModel = _marketingYearService.GetMarketingYearModel(previousMarketingYearId);

            annualPlanViewModel.CurrentAnnualPlanModel = GetAnnualPlanModel(marketingYearId);
            annualPlanViewModel.PreviousAnnualPlanModel = GetAnnualPlanModel(previousMarketingYearId);

            annualPlanViewModel.BigGamePlanModel = _gamePlanService.GetGameAnnualPlanModel(GameType.Big, marketingYearId);
            annualPlanViewModel.SmallGamePlanModel = _gamePlanService.GetGameAnnualPlanModel(GameType.Small, marketingYearId);
            
            return annualPlanViewModel;
        }
        
        private AnnualPlanModel GetAnnualPlanModel(int marketingYearId)
        {
            var annualPlanModel = new AnnualPlanModel();

            annualPlanModel.EmployeePlanModel = GetEmploymentPlanModel(marketingYearId);

            annualPlanModel.HuntEquipmentPlanModel = GetHuntEquipmentPlanModel(marketingYearId);

            annualPlanModel.TrunkFoodPlanModel = GetTrunkFoodPlanModel(marketingYearId);

            annualPlanModel.BarrierPlanModel = GetBarrierPlanModel(marketingYearId);

            annualPlanModel.FieldPlanModel = GetFieldPlanModel(marketingYearId);

            annualPlanModel.FodderPlanModel = GetFodderPlanModel(marketingYearId);

            annualPlanModel.DamagedFieldPlanModel = GetDamagedFieldPlanModel(marketingYearId);

            annualPlanModel.CostPlanModel = GetCostPlanModel(marketingYearId);

            return annualPlanModel;
        }

        private EmploymentPlanModel GetEmploymentPlanModel(int marketingYearId)
        {
            IList<EmploymentPlanDto> employeePlans = _employeePlanDao.GetByMarketingYear(marketingYearId);

            var employmentPlanModel = new EmploymentPlanModel
            {
                FullTimeEmployees = employeePlans.FirstOrDefault(x => x.EmploymentType == (int) EmploymentType.FullTime)?.Count ?? 0,
                PartTimeEmployees = employeePlans.FirstOrDefault(x => x.EmploymentType == (int) EmploymentType.PartTime)?.Count ?? 0
            };
            
            return employmentPlanModel;
        }

        private HuntEquipmentPlanModel GetHuntEquipmentPlanModel(int marketingYearId)
        {
            IList<HuntEquipmentPlanDto> huntEquipmentPlans = _huntEquipmentPlanDao.GetByMarketingYear(marketingYearId);

           var huntEquipmentPlanModel = new HuntEquipmentPlanModel
           {
               Aviaries = huntEquipmentPlans.FirstOrDefault(x => x.Type == (int) HuntEquipment.Aviary)?.Count ?? 0,
               DeerLickers = huntEquipmentPlans.FirstOrDefault(x => x.Type == (int) HuntEquipment.DeerLicker)?.Count ?? 0,
               Farms = huntEquipmentPlans.FirstOrDefault(x => x.Type == (int) HuntEquipment.Farm)?.Count ?? 0,
               Pastures = huntEquipmentPlans.FirstOrDefault(x => x.Type == (int) HuntEquipment.Pasture)?.Count ?? 0,
               Pulpits = huntEquipmentPlans.FirstOrDefault(x => x.Type == (int) HuntEquipment.Pulpit)?.Count ?? 0,
               WateringPlaces = huntEquipmentPlans.FirstOrDefault(x => x.Type == (int) HuntEquipment.WateringPlace)?.Count ?? 0
           };

            return huntEquipmentPlanModel;
        }

        private TrunkFoodPlanModel GetTrunkFoodPlanModel(int marketingYearId)
        {
            TrunkFoodPlanDto trunkFoodPlanDto = _trunkFoodPlanDao.GetByMarketingYear(marketingYearId);

            var trunkFoodPlanModel = new TrunkFoodPlanModel
            {
                Hectare = trunkFoodPlanDto?.Hectare ?? 0
            };

            return trunkFoodPlanModel;
        }

        private BarrierPlanModel GetBarrierPlanModel(int marketingYearId)
        {
           return new BarrierPlanModel();
        }

        private FieldPlanModel GetFieldPlanModel(int marketingYearId)
        {
            FieldPlanDto fieldPlan = _fieldPlanDao.GetByMarketingYear(marketingYearId);

            var fieldPlanModel = new FieldPlanModel
            {
                Hectare = fieldPlan?.Hectare ?? 0
            };

            return fieldPlanModel;
        }

        private AnnualPlanFodderModel GetFodderPlanModel(int marketingYearId)
        {
            IList<FodderPlanDto> fodderPlans = _fodderPlanDao.GetByMarketingYear(marketingYearId);
            IList<FodderDto> fodders = _fodderDao.GetByMarketingYear(marketingYearId);

            var annualPlanFodderModel = new AnnualPlanFodderModel
            {
                Dry = GetAnnualPlanFodderTypeModel(FodderType.Dry, fodderPlans, fodders),
                Juicy = GetAnnualPlanFodderTypeModel(FodderType.Juicy, fodderPlans, fodders),
                Pithy = GetAnnualPlanFodderTypeModel(FodderType.Pithy, fodderPlans, fodders),
                Salt = GetAnnualPlanFodderTypeModel(FodderType.Salt, fodderPlans, fodders)
            };

            return annualPlanFodderModel;
        }

        private AnnualPlanFodderTypeModel GetAnnualPlanFodderTypeModel(FodderType fodderType, IList<FodderPlanDto> fodderPlans, IList<FodderDto> fodders)
        {
            var annualPlanFodderTypeModel = new AnnualPlanFodderTypeModel
            {
                FodderType = fodderType,
                FodderTypeName = TypeName.GetFodderTypeName((int)fodderType),
                Plan = fodderPlans.FirstOrDefault(x => x.Type == (int)fodderType)?.Ton ?? 0,
                Execution = fodders.Where(x => x.Type == (int)fodderType).Sum(x => x.Kilograms) / 1000.0
            };

            return annualPlanFodderTypeModel;
        }

        private DamagedFieldPlanModel GetDamagedFieldPlanModel(int marketingYearId)
        {
            return new DamagedFieldPlanModel();
        }

        private CostPlanModel GetCostPlanModel(int marketingYearId)
        {
            IList<CostPlanDto> costPlans = _costPlanDao.GetByMarketingYear(marketingYearId);

            var costPlanModel = new CostPlanModel
            {
                Cost = costPlans.FirstOrDefault(x => x.Type == (int) CostType.Cost)?.Cost ?? 0,
                Revenue = costPlans.FirstOrDefault(x => x.Type == (int) CostType.Revenue)?.Cost ?? 0
            };

            return costPlanModel;
        }
    }
}
