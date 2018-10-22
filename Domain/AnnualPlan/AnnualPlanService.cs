using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.AnnualPlan.Cost;
using DataAccess.Dao.AnnualPlan.Employee;
using DataAccess.Dao.AnnualPlan.Field;
using DataAccess.Dao.AnnualPlan.Fodder;
using DataAccess.Dao.AnnualPlan.Game;
using DataAccess.Dao.AnnualPlan.HuntEquipment;
using DataAccess.Dao.AnnualPlan.TrunkFood;
using DataAccess.Entities.AnnualPlan;
using Domain.AnnualPlan.Models;
using Domain.AnnualPlan.ViewModels;

namespace Domain.AnnualPlan
{
    public class AnnualPlanService : IAnnualPlanService
    {
        private readonly IEmployeePlanDao _employeePlanDao;
        private readonly IHuntEquipmentPlanDao _huntEquipmentPlanDao;
        private readonly ITrunkFoodPlanDao _trunkFoodPlanDao;
        private readonly IFieldPlanDao _fieldPlanDao;
        private readonly IFodderPlanDao _fodderPlanDao;
        private readonly ICostPlanDao _costPlanDao;
        private readonly IGamePlanDao _gamePlanDao;

        public AnnualPlanService() : this(new EmployeePlanDao(), new HuntEquipmentPlanDao(), new TrunkFoodPlanDao(), new FieldPlanDao(), new FodderPlanDao(), new CostPlanDao(), new GamePlanDao())
        {
            
        }

        public AnnualPlanService(IEmployeePlanDao employeePlanDao, 
            IHuntEquipmentPlanDao huntEquipmentPlanDao, 
            ITrunkFoodPlanDao trunkFoodPlanDao, 
            IFieldPlanDao fieldPlanDao, 
            IFodderPlanDao fodderPlanDao, 
            ICostPlanDao costPlanDao, 
            IGamePlanDao gamePlanDao)
        {
            _employeePlanDao = employeePlanDao;
            _huntEquipmentPlanDao = huntEquipmentPlanDao;
            _trunkFoodPlanDao = trunkFoodPlanDao;
            _fieldPlanDao = fieldPlanDao;
            _fodderPlanDao = fodderPlanDao;
            _costPlanDao = costPlanDao;
            _gamePlanDao = gamePlanDao;
        }

        public AnnualPlanViewModel GetAnnualPlanViewModel()
        {
            var annualPlanViewModel = new AnnualPlanViewModel();

            annualPlanViewModel.CurrentAnnualPlanModel = GetAnnualPlanModel(DateTime.Now.Year);

            annualPlanViewModel.LastYearAnnualPlanModel = GetAnnualPlanModel(DateTime.Now.Year - 1);
            
            return annualPlanViewModel;
        }
        
        private AnnualPlanModel GetAnnualPlanModel(int year)
        {
            var annualPlanModel = new AnnualPlanModel();

            annualPlanModel.EmployeePlanModels = GetEmployeePlanModels(year);

            annualPlanModel.HuntEquipmentPlanModels = GetHuntEquipmentPlanModels(year);

            annualPlanModel.TrunkFoodPlanModel = GetTrunkFoodPlanModel(year);

            annualPlanModel.BarrierPlanModel = GetBarrierPlanModel(year);

            annualPlanModel.FieldPlanModel = GetFieldPlanModel(year);

            annualPlanModel.FodderPlanModels = GetFodderPlanModels(year);

            annualPlanModel.DamagedFieldPlanModel = GetDamagedFieldPlanModel(year);

            annualPlanModel.CostPlanModels = GetCostPlanModels(year);

            annualPlanModel.GamePlanModels = GetGamePlanModels(year);

            return annualPlanModel;
        }

        private List<EmployeePlanModel> GetEmployeePlanModels(int year)
        {
            List<EmployeePlan> employeePlan = _employeePlanDao.GetEmployeePlan(year);

            List<EmployeePlanModel> employeePlanModels = employeePlan.Select(x => new EmployeePlanModel
            {
                Type = x.Unit,
                Count = x.Count,
                Year = x.Year
            }).ToList();
            
            return employeePlanModels;
        }

        private List<HuntEquipmentPlanModel> GetHuntEquipmentPlanModels(int year)
        {
            List<HuntEquipmentPlan> huntEquipmentPlan = _huntEquipmentPlanDao.GetHuntEquipmentPlan(year);

            var huntEquipmentPlanModels = huntEquipmentPlan.Select(x => new HuntEquipmentPlanModel
            {
                Type = x.Type,
                Count = x.Count,
                Unit = x.Unit,
                Year = x.Year
            }).ToList();

            return huntEquipmentPlanModels;
        }

        private TrunkFoodPlanModel GetTrunkFoodPlanModel(int year)
        {
            List<TrunkFoodPlan> trunkFoodPlan = _trunkFoodPlanDao.GetTrunkFoodPlan(year);

            TrunkFoodPlanModel trunkFoodPlanModel = trunkFoodPlan.Select(x => new TrunkFoodPlanModel
            {
                Unit = x.Unit,
                Count = x.Count,
                Year = x.Year
            }).Single();

            return trunkFoodPlanModel;
        }

        private BarrierPlanModel GetBarrierPlanModel(int year)
        {
           return new BarrierPlanModel();
        }

        private FieldPlanModel GetFieldPlanModel(int year)
        {
            List<FieldPlan> fieldPlan = _fieldPlanDao.GetFieldPlan(year);

            FieldPlanModel fieldPlanModel = fieldPlan.Select(x => new FieldPlanModel
            {
                Unit = x.Unit,
                Count = x.Count,
                Year = x.Year
            }).Single();

            return fieldPlanModel;
        }

        private List<FodderPlanModel> GetFodderPlanModels(int year)
        {
            List<FodderPlan> fodderPlan = _fodderPlanDao.GetFodderPlan(year);

            List<FodderPlanModel> fodderPlanModels = fodderPlan.Select(x => new FodderPlanModel
            {
                Type = x.Type,
                Count = x.Count,
                Unit = x.Unit,
                Year = x.Year
            }).ToList();

            return fodderPlanModels;
        }

        private DamagedFieldPlanModel GetDamagedFieldPlanModel(int year)
        {
            return new DamagedFieldPlanModel();
        }

        private List<CostPlanModel> GetCostPlanModels(int year)
        {
            List<CostPlan> costPlan = _costPlanDao.GetCostPlan(year);

            List<CostPlanModel> costPlanModels = costPlan.Select(x => new CostPlanModel
            {
                Type = x.Type,
                Cost = x.Cost,
                Year = x.Year
            }).ToList();

            return costPlanModels;
        }

        private List<GamePlanModel> GetGamePlanModels(int year)
        {
            List<GamePlan> gamePlan = _gamePlanDao.GetGamePlan(year);

            List<GamePlanModel> gamePlanModels = gamePlan.Select(x => new GamePlanModel
            {
                Type = x.Type,
                SubType = x.SubType,
                Class = x.Class,
                Cull = x.Cull,
                Catch = x.Catch,
                Loss = 0,
                Year = x.Year
            }).ToList();

            return gamePlanModels;
        }
    }
}
