using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.EmploymentPlan;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.EmploymentPlan.ViewModels;

namespace Domain.EmploymentPlan
{
    public class EmploymentPlanService : IEmploymentPlanService
    {
        private readonly IEmploymentPlanDao _employmentPlanDao;
        private readonly IMarketingYearDao _marketingYearDao;
        public EmploymentPlanService() : this(new EmploymentPlanDao(), new MarketingYearDao())
        {
        }

        public EmploymentPlanService(IEmploymentPlanDao employmentPlanDao, IMarketingYearDao marketingYearDao)
        {
            _employmentPlanDao = employmentPlanDao;
            _marketingYearDao = marketingYearDao;
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

            DataAccess.Entities.MarketingYear marketingYear = _marketingYearDao.GetById(marketingYearId);

            var employmentPlanBaseViewModel = new EmploymentPlanBaseViewModel
            {
                EmploymentPlanViewModels = employmentPlanViewModels,
                MarketingYearId = marketingYearId,
                MarketingYearStart = marketingYear.Start,
                MarketingYearEnd = marketingYear.End
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
