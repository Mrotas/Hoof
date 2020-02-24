using System;
using DataAccess.Dao.FieldPlan;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.AnnualPlanStatus.Models;
using Domain.FieldPlan.Models;
using Domain.FieldPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Domain.FieldPlan
{
    public class FieldPlanService : IFieldPlanService
    {
        private readonly IFieldPlanDao _fieldPlanDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public FieldPlanService() : this(new FieldPlanDao(), new MarketingYearService(), new AnnualPlanStatusService())
        {
        }

        public FieldPlanService(IFieldPlanDao fieldPlanDao, IMarketingYearService marketingYearService, IAnnualPlanStatusService annualPlanStatusService)
        {
            _fieldPlanDao = fieldPlanDao;
            _marketingYearService = marketingYearService;
            _annualPlanStatusService = annualPlanStatusService;
        }

        public FieldPlanViewModel GetFieldPlanViewModel(int marketingYearId)
        {
            FieldPlanDto fieldPlanDto = _fieldPlanDao.GetByMarketingYear(marketingYearId);

            FiledPlanModel filedPlanModel = null;
            if (fieldPlanDto != null)
            {
                filedPlanModel = new FiledPlanModel
                {
                    Hectare = fieldPlanDto.Hectare
                };
            }

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);
            AnnualPlanStatusModel annualPlanStatusModel = _annualPlanStatusService.GetByMarketingYearId(marketingYearId);

            var fieldPlanViewModel = new FieldPlanViewModel
            {
                FiledPlanModel = filedPlanModel,
                MarketingYearModel = marketingYearModel,
                AnnualPlanStatusModel = annualPlanStatusModel
            };

            return fieldPlanViewModel;
        }

        public void AddFieldPlan(FiledPlanModel model, int marketingYearId)
        {
            FieldPlanDto existingFieldPlanDto = _fieldPlanDao.GetByMarketingYear(marketingYearId);
            if (existingFieldPlanDto != null)
            {
                throw new Exception("Plan gospodarczy powierzchni łąk śródleśnych i przyleśnych już istnieje! Proszę użyć opcji edycji istniejącego już planu.");
            }

            var dto = new FieldPlanDto
            {
                Hectare = model.Hectare,
                MarketingYearId = marketingYearId
            };

            _fieldPlanDao.Insert(dto);
        }

        public void UpdateFieldPlan(FiledPlanModel model, int marketingYearId)
        {
            var dto = new FieldPlanDto
            {
                Hectare = model.Hectare,
                MarketingYearId = marketingYearId
            };

            _fieldPlanDao.Update(dto);
        }

        public void DeleteFieldPlan(int marketingYearId)
        {
            _fieldPlanDao.Delete(marketingYearId);
        }
    }
}
