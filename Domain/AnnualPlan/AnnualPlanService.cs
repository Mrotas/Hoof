using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Extensions;
using DataAccess.Dao.CarcassRevenue;
using DataAccess.Dao.CostPlan;
using DataAccess.Dao.DeerLicker;
using DataAccess.Dao.EmploymentPlan;
using DataAccess.Dao.Expense;
using DataAccess.Dao.FieldPlan;
using DataAccess.Dao.Fodder;
using DataAccess.Dao.FodderPlan;
using DataAccess.Dao.HuntEquipmentPlan;
using DataAccess.Dao.Pasture;
using DataAccess.Dao.Pulpit;
using DataAccess.Dao.TrunkFoodPlan;
using DataAccess.Dao.WateringPlace;
using DataAccess.Dto;
using Domain.AnnualPlan.Models;
using Domain.AnnualPlan.Models.Cost;
using Domain.AnnualPlan.Models.Employment;
using Domain.AnnualPlan.Models.Fodder;
using Domain.AnnualPlan.Models.HuntEquipment;
using Domain.AnnualPlan.ViewModels;
using Domain.GamePlan;
using Domain.MarketingYear;

namespace Domain.AnnualPlan
{
    public class AnnualPlanService : IAnnualPlanService
    {
        public int CurrentMarketingYearId { get; set; }
        public int PreviousMarketingYearId { get; set; }

        private readonly IMarketingYearService _marketingYearService;
        private readonly IEmploymentPlanDao _employeePlanDao;
        private readonly IHuntEquipmentPlanDao _huntEquipmentPlanDao;
        private readonly IPastureDao _pastureDao;
        private readonly IDeerLickerDao _deerLickerDao;
        private readonly IPulpitDao _pulpitDao;
        private readonly IWateringPlaceDao _wateringPlaceDao;
        private readonly ITrunkFoodPlanDao _trunkFoodPlanDao;
        private readonly IFieldPlanDao _fieldPlanDao;
        private readonly IFodderPlanDao _fodderPlanDao;
        private readonly IFodderDao _fodderDao;
        private readonly ICostPlanDao _costPlanDao;
        private readonly IExpenseDao _expenseDao;
        private readonly ICarcassRevenueDao _carcassRevenueDao;
        private readonly IGamePlanService _gamePlanService;

        public AnnualPlanService() : this(new EmploymentPlanDao(), 
            new HuntEquipmentPlanDao(),
            new PastureDao(),
            new DeerLickerDao(),
            new PulpitDao(),
            new WateringPlaceDao(),
            new TrunkFoodPlanDao(), 
            new FieldPlanDao(),
            new FodderPlanDao(),
            new FodderDao(), 
            new CostPlanDao(), 
            new ExpenseDao(),
            new CarcassRevenueDao(),
            new GamePlanService(), 
            new MarketingYearService())
        {
        }

        public AnnualPlanService(IEmploymentPlanDao employeePlanDao, 
            IHuntEquipmentPlanDao huntEquipmentPlanDao,
            IPastureDao pastureDao,
            IDeerLickerDao deerLickerDao,
            IPulpitDao pulpitDao,
            IWateringPlaceDao wateringPlaceDao,
            ITrunkFoodPlanDao trunkFoodPlanDao, 
            IFieldPlanDao fieldPlanDao, 
            IFodderPlanDao fodderPlanDao,
            IFodderDao fodderDao,
            ICostPlanDao costPlanDao,
            IExpenseDao expenseDao,
            ICarcassRevenueDao carcassRevenueDao,
            IGamePlanService gamePlanService,
            IMarketingYearService marketingYearService)
        {
            _employeePlanDao = employeePlanDao;
            _huntEquipmentPlanDao = huntEquipmentPlanDao;
            _pastureDao = pastureDao;
            _deerLickerDao = deerLickerDao;
            _pulpitDao = pulpitDao;
            _wateringPlaceDao = wateringPlaceDao;
            _trunkFoodPlanDao = trunkFoodPlanDao;
            _fieldPlanDao = fieldPlanDao;
            _fodderPlanDao = fodderPlanDao;
            _fodderDao = fodderDao;
            _costPlanDao = costPlanDao;
            _expenseDao = expenseDao;
            _carcassRevenueDao = carcassRevenueDao;
            _gamePlanService = gamePlanService;
            _marketingYearService = marketingYearService;
        }

        public AnnualPlanViewModel GetAnnualPlanViewModel(int marketingYearId)
        {
            CurrentMarketingYearId = marketingYearId;
            PreviousMarketingYearId = marketingYearId - 1;

            var annualPlanViewModel = new AnnualPlanViewModel();

            annualPlanViewModel.MarketingYearModel = _marketingYearService.GetMarketingYearModel(CurrentMarketingYearId);

            annualPlanViewModel.AnnualPlanModel = GetAnnualPlanModel();
            
            annualPlanViewModel.BigGamePlanModel = _gamePlanService.GetGameAnnualPlanModel(GameType.Big, CurrentMarketingYearId);
            
            annualPlanViewModel.SmallGamePlanModel = _gamePlanService.GetGameAnnualPlanModel(GameType.Small, CurrentMarketingYearId);
           
            return annualPlanViewModel;
        }
        
        private AnnualPlanModel GetAnnualPlanModel()
        {
            var annualPlanModel = new AnnualPlanModel();

            annualPlanModel.EmployeePlanModel = GetEmploymentPlanModel();

            annualPlanModel.HuntEquipmentPlanModel = GetHuntEquipmentPlanModel();

            annualPlanModel.TrunkFoodPlanModel = GetTrunkFoodPlanModel();

            annualPlanModel.BarrierPlanModel = GetBarrierPlanModel();

            annualPlanModel.FieldPlanModel = GetFieldPlanModel();

            annualPlanModel.FodderPlanModel = GetFodderPlanModel();

            annualPlanModel.DamagedFieldPlanModel = GetDamagedFieldPlanModel();

            annualPlanModel.CostPlanModel = GetCostPlanModel();

            return annualPlanModel;
        }

        private AnnualPlanEmploymentModel GetEmploymentPlanModel()
        {
            IList<EmploymentPlanDto> previousMarketingYearEmployeePlans = _employeePlanDao.GetByMarketingYear(PreviousMarketingYearId);
            IList<EmploymentPlanDto> currentMarketingYearEmployeePlans = _employeePlanDao.GetByMarketingYear(CurrentMarketingYearId);

            var employmentPlanModel = new AnnualPlanEmploymentModel();

            employmentPlanModel.FullTimeEmployees = GetAnnualPlanEmploymentModel(EmploymentType.FullTime, previousMarketingYearEmployeePlans, currentMarketingYearEmployeePlans);
            
            employmentPlanModel.PartTimeEmployees = GetAnnualPlanEmploymentModel(EmploymentType.PartTime, previousMarketingYearEmployeePlans, currentMarketingYearEmployeePlans);
            
            return employmentPlanModel;
        }

        private AnnualPlanEmploymentTypeModel GetAnnualPlanEmploymentModel(EmploymentType employmentType, IList<EmploymentPlanDto> previousMarketingYearEmployeePlans, IList<EmploymentPlanDto> currentMarketingYearEmployeePlans)
        {
            var annualPlanEmploymentTypeModel = new AnnualPlanEmploymentTypeModel
            {
                EmploymentType = employmentType,
                EmploymentTypeName = TypeName.GetEmploymentTypeName((int) employmentType),
                PreviousPlan = previousMarketingYearEmployeePlans.FirstOrDefault(x => x.EmploymentType == (int) employmentType)?.Count ?? 0,
                Execution = 0, //TODO: Add Employment utility
                CurrentState = 0,
                FutureState = currentMarketingYearEmployeePlans.FirstOrDefault(x => x.EmploymentType == (int)employmentType)?.Count ?? 0
            };

            return annualPlanEmploymentTypeModel;
        }

        private AnnualPlanHuntEquipmentModel GetHuntEquipmentPlanModel()
        {
            IList<HuntEquipmentPlanDto> previousMarketingYearHuntEquipmentPlans = _huntEquipmentPlanDao.GetByMarketingYear(PreviousMarketingYearId);
            IList<HuntEquipmentPlanDto> currentMarketingYearHuntEquipmentPlans = _huntEquipmentPlanDao.GetByMarketingYear(CurrentMarketingYearId);

           var annualPlanHuntEquipmentModel = new AnnualPlanHuntEquipmentModel
           {
               Aviaries = GetAnnualPlanHuntEquipmentTypeModel(HuntEquipmentType.Aviary, previousMarketingYearHuntEquipmentPlans, currentMarketingYearHuntEquipmentPlans),
               DeerLickers = GetAnnualPlanHuntEquipmentTypeModel(HuntEquipmentType.DeerLicker, previousMarketingYearHuntEquipmentPlans, currentMarketingYearHuntEquipmentPlans),
               Farms = GetAnnualPlanHuntEquipmentTypeModel(HuntEquipmentType.Farm, previousMarketingYearHuntEquipmentPlans, currentMarketingYearHuntEquipmentPlans),
               Pastures = GetAnnualPlanHuntEquipmentTypeModel(HuntEquipmentType.Pasture, previousMarketingYearHuntEquipmentPlans, currentMarketingYearHuntEquipmentPlans),
               Pulpits = GetAnnualPlanHuntEquipmentTypeModel(HuntEquipmentType.Pulpit, previousMarketingYearHuntEquipmentPlans, currentMarketingYearHuntEquipmentPlans),
               WateringPlaces = GetAnnualPlanHuntEquipmentTypeModel(HuntEquipmentType.WateringPlace, previousMarketingYearHuntEquipmentPlans, currentMarketingYearHuntEquipmentPlans)
           };

            return annualPlanHuntEquipmentModel;
        }

        private AnnualPlanHuntEquipmentTypeModel GetAnnualPlanHuntEquipmentTypeModel(HuntEquipmentType huntEquipmentType, IList<HuntEquipmentPlanDto> previousMarketingYearHuntEquipmentPlans, IList<HuntEquipmentPlanDto> currentMarketingYearHuntEquipmentPlans)
        {
            var annualPlanHuntEquipmentTypeModel = new AnnualPlanHuntEquipmentTypeModel
            {
                HuntEquipmentType = huntEquipmentType,
                HuntEquipmentTypeName = TypeName.GetHuntEquipmentTypeName((int) huntEquipmentType),
                PreviousPlan = previousMarketingYearHuntEquipmentPlans.FirstOrDefault(x => x.Type == (int) huntEquipmentType)?.Count ?? 0
            };

            switch (annualPlanHuntEquipmentTypeModel.HuntEquipmentType)
            {
                case HuntEquipmentType.Pasture:
                    IList<PastureDto> currentStatePastures = _pastureDao.GetActiveByMarketingYear(CurrentMarketingYearId);
                    IList<PastureDto> previousMarketingYearPasturesState = _pastureDao.GetActiveByMarketingYear(PreviousMarketingYearId);
                    annualPlanHuntEquipmentTypeModel.CurrentState = currentStatePastures.Count;
                    annualPlanHuntEquipmentTypeModel.Execution = currentStatePastures.Count - previousMarketingYearPasturesState.Count;
                    annualPlanHuntEquipmentTypeModel.FutureState = currentStatePastures.Count + currentMarketingYearHuntEquipmentPlans.FirstOrDefault(x => x.Type == (int)huntEquipmentType)?.Count ?? 0;
                    break;
                case HuntEquipmentType.Pulpit:
                    IList<PulpitDto> currentStatePulpits = _pulpitDao.GetActiveByMarketingYear(CurrentMarketingYearId);
                    IList<PulpitDto> previousMarketingYearPulpitsState = _pulpitDao.GetActiveByMarketingYear(PreviousMarketingYearId);
                    annualPlanHuntEquipmentTypeModel.CurrentState = currentStatePulpits.Count;
                    annualPlanHuntEquipmentTypeModel.Execution = currentStatePulpits.Count - previousMarketingYearPulpitsState.Count;
                    annualPlanHuntEquipmentTypeModel.FutureState = currentStatePulpits.Count + currentMarketingYearHuntEquipmentPlans.FirstOrDefault(x => x.Type == (int)huntEquipmentType)?.Count ?? 0;
                    break;
                case HuntEquipmentType.DeerLicker:
                    IList<DeerLickerDto> currentStateDeerLickers = _deerLickerDao.GetByMarketingYear(CurrentMarketingYearId);
                    IList<DeerLickerDto> previousMarketingYearDeerLickersState = _deerLickerDao.GetByMarketingYear(PreviousMarketingYearId);
                    annualPlanHuntEquipmentTypeModel.CurrentState = currentStateDeerLickers.Sum(x => x.Count);
                    annualPlanHuntEquipmentTypeModel.Execution = currentStateDeerLickers.Sum(x => x.Count) - previousMarketingYearDeerLickersState.Sum(x => x.Count);
                    annualPlanHuntEquipmentTypeModel.FutureState = currentStateDeerLickers.Sum(x => x.Count) + currentMarketingYearHuntEquipmentPlans.FirstOrDefault(x => x.Type == (int)huntEquipmentType)?.Count ?? 0;
                    break;
                case HuntEquipmentType.WateringPlace:
                    IList<WateringPlaceDto> currentStateWateringPlaces = _wateringPlaceDao.GetActiveByMarketingYear(CurrentMarketingYearId);
                    IList<WateringPlaceDto> previousMarketingYearWateringPlacesState = _wateringPlaceDao.GetActiveByMarketingYear(PreviousMarketingYearId);
                    annualPlanHuntEquipmentTypeModel.CurrentState = currentStateWateringPlaces.Count;
                    annualPlanHuntEquipmentTypeModel.Execution = currentStateWateringPlaces.Count - previousMarketingYearWateringPlacesState.Count;
                    annualPlanHuntEquipmentTypeModel.FutureState = currentStateWateringPlaces.Count + currentMarketingYearHuntEquipmentPlans.FirstOrDefault(x => x.Type == (int)huntEquipmentType)?.Count ?? 0;
                    break;
                case HuntEquipmentType.Aviary:
                case HuntEquipmentType.Farm:
                default:
                    break;
            }

            return annualPlanHuntEquipmentTypeModel;
        }

        private TrunkFoodPlanModel GetTrunkFoodPlanModel()
        {
            TrunkFoodPlanDto previousMarketingYearTrunkFoodPlan = _trunkFoodPlanDao.GetByMarketingYear(PreviousMarketingYearId);
            TrunkFoodPlanDto currentMarketingYearTrunkFoodPlan = _trunkFoodPlanDao.GetByMarketingYear(CurrentMarketingYearId);

            var trunkFoodPlanModel = new TrunkFoodPlanModel
            {
                PreviousPlan = previousMarketingYearTrunkFoodPlan?.Hectare ?? 0,
                Execution = 0, //TODO: Add Trunk Food utility
                CurrentState = 0,
                FutureState = currentMarketingYearTrunkFoodPlan?.Hectare ?? 0
            };

            return trunkFoodPlanModel;
        }

        private BarrierPlanModel GetBarrierPlanModel()
        {
           return new BarrierPlanModel();
        }

        private FieldPlanModel GetFieldPlanModel()
        {
            FieldPlanDto previousMarketingYearFieldPlan = _fieldPlanDao.GetByMarketingYear(PreviousMarketingYearId);
            FieldPlanDto currentMarketingYearFieldPlan = _fieldPlanDao.GetByMarketingYear(CurrentMarketingYearId);

            var fieldPlanModel = new FieldPlanModel
            {
                PreviousPlan = previousMarketingYearFieldPlan?.Hectare ?? 0,
                Execution = 0, //TODO: Add Field utility
                CurrentState = 0,
                FutureState = currentMarketingYearFieldPlan?.Hectare ?? 0
            };

            return fieldPlanModel;
        }

        private AnnualPlanFodderModel GetFodderPlanModel()
        {
            IList<FodderPlanDto> previousMarketingYearFodderPlans = _fodderPlanDao.GetByMarketingYear(PreviousMarketingYearId);
            IList<FodderPlanDto> currentMarketingYearFodderPlans = _fodderPlanDao.GetByMarketingYear(CurrentMarketingYearId);
            IList<FodderDto> fodders = _fodderDao.GetByMarketingYear(PreviousMarketingYearId);

            var annualPlanFodderModel = new AnnualPlanFodderModel
            {
                Dry = GetAnnualPlanFodderTypeModel(FodderType.Dry, previousMarketingYearFodderPlans, currentMarketingYearFodderPlans, fodders),
                Juicy = GetAnnualPlanFodderTypeModel(FodderType.Juicy, previousMarketingYearFodderPlans, currentMarketingYearFodderPlans, fodders),
                Pithy = GetAnnualPlanFodderTypeModel(FodderType.Pithy, previousMarketingYearFodderPlans, currentMarketingYearFodderPlans, fodders),
                Salt = GetAnnualPlanFodderTypeModel(FodderType.Salt, previousMarketingYearFodderPlans, currentMarketingYearFodderPlans, fodders)
            };

            return annualPlanFodderModel;
        }

        private AnnualPlanFodderTypeModel GetAnnualPlanFodderTypeModel(FodderType fodderType,
            IList<FodderPlanDto> previousMarketingYearFodderPlans, IList<FodderPlanDto> currentMarketingYearFodderPlans,
            IList<FodderDto> fodders)
        {
            var annualPlanFodderTypeModel = new AnnualPlanFodderTypeModel
            {
                FodderType = fodderType,
                FodderTypeName = TypeName.GetFodderTypeName((int)fodderType),
                PreviousPlan = previousMarketingYearFodderPlans.FirstOrDefault(x => x.Type == (int)fodderType)?.Ton ?? 0,
                Execution = fodders.Where(x => x.Type == (int)fodderType).Sum(x => x.Kilograms) / 1000.0,
                CurrentState = 0,
                FutureState = currentMarketingYearFodderPlans.FirstOrDefault(x => x.Type == (int)fodderType)?.Ton ?? 0
            };

            return annualPlanFodderTypeModel;
        }

        private DamagedFieldPlanModel GetDamagedFieldPlanModel()
        {
            return new DamagedFieldPlanModel();
        }

        private AnnualPlanCostModel GetCostPlanModel()
        {
            IList<CostPlanDto> previousMarketingYearCostPlans = _costPlanDao.GetByMarketingYear(PreviousMarketingYearId);
            IList<CostPlanDto> currentMarketingYearCostPlans = _costPlanDao.GetByMarketingYear(CurrentMarketingYearId);

            var costPlanModel = new AnnualPlanCostModel
            {
                Cost = GetAnnualPlanCostTypeModel(CostType.Cost, previousMarketingYearCostPlans, currentMarketingYearCostPlans),
                Revenue = GetAnnualPlanCostTypeModel(CostType.Revenue, previousMarketingYearCostPlans, currentMarketingYearCostPlans)
            };

            return costPlanModel;
        }

        private AnnualPlanCostTypeModel GetAnnualPlanCostTypeModel(CostType costType,
            IList<CostPlanDto> previousMarketingYearCostPlans, IList<CostPlanDto> currentMarketingYearCostPlans)
        {
            var annualPlanCostTypeModel = new AnnualPlanCostTypeModel
            {
                CostType = costType,
                CostTypeName = TypeName.GetFodderTypeName((int)costType),
                PreviousPlan = previousMarketingYearCostPlans.FirstOrDefault(x => x.Type == (int)costType)?.Cost / 1000.0 ?? 0,
                CurrentState = 0, // TODO: How to determine it?
                FutureState = currentMarketingYearCostPlans.FirstOrDefault(x => x.Type == (int)costType)?.Cost / 1000.0 ?? 0
            };

            switch (costType)
            {
                case CostType.Cost:
                    IList<ExpenseDto> previousMarketingYearExpenses = _expenseDao.GetByMarketingYear(PreviousMarketingYearId);
                    annualPlanCostTypeModel.Execution = previousMarketingYearExpenses.Sum(x => x.Cost) / 1000.0;
                    break;
                case CostType.Revenue:
                    IList<CarcassRevenueDto> previousMarketingYearCarcassRevenues = _carcassRevenueDao.GetByMarketingYear(PreviousMarketingYearId);
                    annualPlanCostTypeModel.Execution = previousMarketingYearCarcassRevenues.Sum(x => x.Revenue) / 1000.0;
                    break;
                default:
                    break;
            }

            return annualPlanCostTypeModel;
        }
    }
}
