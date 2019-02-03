using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using DataAccess.Dao.CostPlan;
using DataAccess.Dao.EmploymentPlan;
using DataAccess.Dao.FieldPlan;
using DataAccess.Dao.FodderPlan;
using DataAccess.Dao.HuntEquipmentPlan;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dao.TrunkFoodPlan;
using DataAccess.Dto;
using Domain.AnnualPlan.Models;
using Domain.AnnualPlan.ViewModels;
using Domain.GamePlan;

namespace Domain.AnnualPlan
{
    public class AnnualPlanService : IAnnualPlanService
    {
        private readonly IMarketingYearDao _marketingYearDao;
        private readonly IEmploymentPlanDao _employeePlanDao;
        private readonly IHuntEquipmentPlanDao _huntEquipmentPlanDao;
        private readonly ITrunkFoodPlanDao _trunkFoodPlanDao;
        private readonly IFieldPlanDao _fieldPlanDao;
        private readonly IFodderPlanDao _fodderPlanDao;
        private readonly ICostPlanDao _costPlanDao;
        private readonly IGamePlanService _gamePlanService;

        public AnnualPlanService() : this(new EmploymentPlanDao(), 
            new HuntEquipmentPlanDao(), 
            new TrunkFoodPlanDao(), 
            new FieldPlanDao(),
            new FodderPlanDao(), 
            new CostPlanDao(), 
            new GamePlanService(), 
            new MarketingYearDao())
        {
        }

        public AnnualPlanService(IEmploymentPlanDao employeePlanDao, 
            IHuntEquipmentPlanDao huntEquipmentPlanDao, 
            ITrunkFoodPlanDao trunkFoodPlanDao, 
            IFieldPlanDao fieldPlanDao, 
            IFodderPlanDao fodderPlanDao, 
            ICostPlanDao costPlanDao, 
            IGamePlanService gamePlanService,
            IMarketingYearDao marketingYearDao)
        {
            _employeePlanDao = employeePlanDao;
            _huntEquipmentPlanDao = huntEquipmentPlanDao;
            _trunkFoodPlanDao = trunkFoodPlanDao;
            _fieldPlanDao = fieldPlanDao;
            _fodderPlanDao = fodderPlanDao;
            _costPlanDao = costPlanDao;
            _gamePlanService = gamePlanService;
            _marketingYearDao = marketingYearDao;
        }

        public AnnualPlanViewModel GetAnnualPlanViewModel(int marketingYearId)
        {
            int previousMarketingYearId = marketingYearId - 1;

            var annualPlanViewModel = new AnnualPlanViewModel();

            annualPlanViewModel.CurrentAnnualPlanModel = GetAnnualPlanModel(marketingYearId);

            annualPlanViewModel.PreviousAnnualPlanModel = GetAnnualPlanModel(previousMarketingYearId);

            annualPlanViewModel.BigGamePlanModel = _gamePlanService.GetGameAnnualPlanModel(GameType.Big, marketingYearId);
            annualPlanViewModel.SmallGamePlanModel = _gamePlanService.GetGameAnnualPlanModel(GameType.Small, marketingYearId);
            
            return annualPlanViewModel;
        }
        
        private AnnualPlanModel GetAnnualPlanModel(int marketingYearId)
        {
            var annualPlanModel = new AnnualPlanModel();

            annualPlanModel.EmployeePlanModels = GetEmploymentPlanModels(marketingYearId);

            annualPlanModel.HuntEquipmentPlanModels = GetHuntEquipmentPlanModels(marketingYearId);

            annualPlanModel.TrunkFoodPlanModel = GetTrunkFoodPlanModel(marketingYearId);

            annualPlanModel.BarrierPlanModel = GetBarrierPlanModel(marketingYearId);

            annualPlanModel.FieldPlanModel = GetFieldPlanModel(marketingYearId);

            annualPlanModel.FodderPlanModels = GetFodderPlanModels(marketingYearId);

            annualPlanModel.DamagedFieldPlanModel = GetDamagedFieldPlanModel(marketingYearId);

            annualPlanModel.CostPlanModels = GetCostPlanModels(marketingYearId);

            return annualPlanModel;
        }

        private List<EmploymentPlanModel> GetEmploymentPlanModels(int marketingYearId)
        {
            IList<EmploymentPlanDto> employeePlan = _employeePlanDao.GetEmploymentPlan(marketingYearId);

            List<EmploymentPlanModel> employeePlanModels = employeePlan.Select(x => new EmploymentPlanModel
            {
                EmploymentType = x.EmploymentType,
                Count = x.Count,
                MarketingYearId = x.MarketingYearId
            }).ToList();
            
            return employeePlanModels;
        }

        private List<HuntEquipmentPlanModel> GetHuntEquipmentPlanModels(int marketingYearId)
        {
            IList<HuntEquipmentPlanDto> huntEquipmentPlan = _huntEquipmentPlanDao.GetHuntEquipmentPlan(marketingYearId);

            var huntEquipmentPlanModels = huntEquipmentPlan.Select(x => new HuntEquipmentPlanModel
            {
                Type = x.Type,
                Count = x.Count,
                MarketingYearId = x.MarketingYearId
            }).ToList();

            return huntEquipmentPlanModels;
        }

        private TrunkFoodPlanModel GetTrunkFoodPlanModel(int marketingYearId)
        {
            IList<TrunkFoodPlanDto> trunkFoodPlan = _trunkFoodPlanDao.GetTrunkFoodPlan(marketingYearId);

            TrunkFoodPlanModel trunkFoodPlanModel = trunkFoodPlan.Select(x => new TrunkFoodPlanModel
            {
                Hectare = x.Hectare,
                MarketingYearId = x.MarketingYearId
            }).Single();

            return trunkFoodPlanModel;
        }

        private BarrierPlanModel GetBarrierPlanModel(int marketingYearId)
        {
           return new BarrierPlanModel();
        }

        private FieldPlanModel GetFieldPlanModel(int marketingYearId)
        {
            IList<FieldPlanDto> fieldPlan = _fieldPlanDao.GetFieldPlan(marketingYearId);

            FieldPlanModel fieldPlanModel = fieldPlan.Select(x => new FieldPlanModel
            {
                Hectare = x.Hectare,
                MarketingYearId = x.MarketingYearId
            }).Single();

            return fieldPlanModel;
        }

        private List<FodderPlanModel> GetFodderPlanModels(int marketingYearId)
        {
            IList<FodderPlanDto> fodderPlan = _fodderPlanDao.GetFodderPlan(marketingYearId);

            List<FodderPlanModel> fodderPlanModels = fodderPlan.Select(x => new FodderPlanModel
            {
                Type = x.Type,
                Ton = x.Ton,
                MarketingYearId = x.MarketingYearId
            }).ToList();

            return fodderPlanModels;
        }

        private DamagedFieldPlanModel GetDamagedFieldPlanModel(int marketingYearId)
        {
            return new DamagedFieldPlanModel();
        }

        private List<CostPlanModel> GetCostPlanModels(int marketingYearId)
        {
            IList<CostPlanDto> costPlan = _costPlanDao.GetCostPlan(marketingYearId);

            List<CostPlanModel> costPlanModels = costPlan.Select(x => new CostPlanModel
            {
                Type = x.Type,
                Cost = x.Cost,
                MarketingYearId = x.MarketingYearId
            }).ToList();

            return costPlanModels;
        }
    }
}
