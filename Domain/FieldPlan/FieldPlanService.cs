using System;
using DataAccess.Dao.FieldPlan;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.FieldPlan.Models;
using Domain.FieldPlan.ViewModels;

namespace Domain.FieldPlan
{
    public class FieldPlanService : IFieldPlanService
    {
        private readonly IFieldPlanDao _fieldPlanDao;
        private readonly IMarketingYearDao _marketingYearDao;

        public FieldPlanService() : this(new FieldPlanDao(), new MarketingYearDao())
        {
        }

        public FieldPlanService(IFieldPlanDao fieldPlanDao, IMarketingYearDao marketingYearDao)
        {
            _fieldPlanDao = fieldPlanDao;
            _marketingYearDao = marketingYearDao;
        }

        public FieldPlanViewModel GetFieldPlanViewModel(int marketingYearId)
        {
            FieldPlanDto fieldPlanDto = _fieldPlanDao.GetByMarketingYear(marketingYearId);

            DataAccess.Entities.MarketingYear marketingYear = _marketingYearDao.GetById(marketingYearId);

            FiledPlanModel filedPlanModel = null;
            if (fieldPlanDto != null)
            {
                filedPlanModel = new FiledPlanModel
                {
                    Hectare = fieldPlanDto.Hectare
                };
            }

            var fieldPlanViewModel = new FieldPlanViewModel
            {
                FiledPlanModel = filedPlanModel,
                MarketingYearId = marketingYear.Id,
                MarketingYearStart = marketingYear.Start,
                MarketingYearEnd = marketingYear.End
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
