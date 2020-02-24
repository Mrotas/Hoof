using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.EmploymentPlan;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.AnnualPlanStatus.Models;
using Domain.EmploymentPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Domain.EmploymentPlan
{
    public class EmploymentPlanService : IEmploymentPlanService
    {
        private readonly IEmploymentPlanDao _employmentPlanDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public EmploymentPlanService() : this(new EmploymentPlanDao(), new MarketingYearService(), new AnnualPlanStatusService())
        {
        }

        public EmploymentPlanService(IEmploymentPlanDao employmentPlanDao, IMarketingYearService marketingYearService, IAnnualPlanStatusService annualPlanStatusService)
        {
            _employmentPlanDao = employmentPlanDao;
            _marketingYearService = marketingYearService;
            _annualPlanStatusService = annualPlanStatusService;
        }

        public EmploymentPlanBaseViewModel GetEmploymentPlanViewModel(int marketingYearId)
        {
            IList<EmploymentPlanDto> employmentPlanDtos = _employmentPlanDao.GetByMarketingYear(marketingYearId);

            List<EmploymentPlanViewModel> employmentPlanViewModels =
            (
                from employmentPlan in employmentPlanDtos
                where employmentPlan.MarketingYearId == marketingYearId
                select new EmploymentPlanViewModel
                {
                    Id = employmentPlan.Id,
                    EmploymentType = employmentPlan.EmploymentType,
                    EmploymentTypeName = GetEmploymentTypeName(employmentPlan.EmploymentType),
                    Count = employmentPlan.Count
                }
            ).ToList();

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);
            AnnualPlanStatusModel annualPlanStatusModel = _annualPlanStatusService.GetByMarketingYearId(marketingYearId);

            var employmentPlanBaseViewModel = new EmploymentPlanBaseViewModel
            {
                EmploymentPlanViewModels = employmentPlanViewModels,
                MarketingYearModel = marketingYearModel,
                AnnualPlanStatusModel = annualPlanStatusModel
            };

            return employmentPlanBaseViewModel;
        }

        public void AddEmploymentPlan(EmploymentPlanViewModel model, int marketingYearId)
        {
            if (model.EmploymentType == 0)
            {
                throw new Exception("Nie można dodać planu gospodarczego zatrudnienia");
            }

            IList<EmploymentPlanDto> existingEquipmentPlanDtos = _employmentPlanDao.GetByMarketingYear(marketingYearId);
            if (existingEquipmentPlanDtos.Any(x => x.EmploymentType == model.EmploymentType))
            {
                throw new Exception($"Plan zatrudnienia {GetEmploymentTypeName(model.EmploymentType)} już istnieje! Proszę użyć opcji edycji istniejącego już planu.");
            }

            var dto = new EmploymentPlanDto
            {
                EmploymentType = model.EmploymentType,
                Count = model.Count,
                MarketingYearId = marketingYearId
            };

            _employmentPlanDao.Insert(dto);
        }

        public void UpdateEmploymentPlan(EmploymentPlanViewModel model, int marketingYearId)
        {
            if (model.EmploymentType <= 0)
            {
                throw new Exception("Nie można edytować planu gospodarczego zatrudnienia");
            }

            var dto = new EmploymentPlanDto
            {
                EmploymentType = model.EmploymentType,
                Count = model.Count,
                MarketingYearId = marketingYearId
            };

            _employmentPlanDao.Update(dto);
        }

        public void DeleteEmploymentPlan(int employmentType, int marketingYearId)
        {
            if (employmentType <= 0)
            {
                throw new Exception("Nie można usunąć planu gospodarczego zatrudnienia");
            }

            _employmentPlanDao.Delete(employmentType, marketingYearId);
        }

        private string GetEmploymentTypeName(int employmentType)
        {
            switch (employmentType)
            {
                case 1: return "Umowa o pracę";
                case 2: return "Inne";
                default: throw new NotImplementedException();
            }
        }
    }
}
