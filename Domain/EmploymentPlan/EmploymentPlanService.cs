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
