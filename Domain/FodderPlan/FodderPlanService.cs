using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.FodderPlan;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.FodderPlan.ViewModels;

namespace Domain.FodderPlan
{
    public class FodderPlanService : IFodderPlanService
    {
        private readonly IFodderPlanDao _fodderPlanDao;
        private readonly IMarketingYearDao _marketingYearDao;

        public FodderPlanService() : this(new FodderPlanDao(), new MarketingYearDao())
        {
        }

        public FodderPlanService(IFodderPlanDao fodderPlanDao, IMarketingYearDao marketingYearDao)
        {
            _fodderPlanDao = fodderPlanDao;
            _marketingYearDao = marketingYearDao;
        }

        public FodderPlanBaseViewModel GetFodderPlanViewModel(int marketingYearId)
        {
            IList<FodderPlanDto> fodderPlanDtos = _fodderPlanDao.GetByMarketingYear(marketingYearId);

            List<FodderPlanViewModel> fodderPlanViewModels =
            (
                from fodderPlan in fodderPlanDtos
                where fodderPlan.MarketingYearId == marketingYearId
                select new FodderPlanViewModel
                {
                    Id = fodderPlan.Id,
                    Type = fodderPlan.Type,
                    TypeName = GetFodderTypeName(fodderPlan.Type),
                    Ton = fodderPlan.Ton
                }
            ).ToList();

            DataAccess.Entities.MarketingYear marketingYear = _marketingYearDao.GetById(marketingYearId);

            var fodderPlanViewBaseModel = new FodderPlanBaseViewModel
            {
                FodderPlanViewModels = fodderPlanViewModels,
                MarketingYearId = marketingYearId,
                MarketingYearStart = marketingYear.Start,
                MarketingYearEnd = marketingYear.End
            };

            return fodderPlanViewBaseModel;
        }

        public void AddFodderPlan(FodderPlanViewModel model, int marketingYearId)
        {
            if (model.Type == 0)
            {
                throw new Exception("Nie można dodać planu gospodarczego karmy");
            }

            IList<FodderPlanDto> existingEquipmentPlanDtos = _fodderPlanDao.GetByMarketingYear(marketingYearId);
            if (existingEquipmentPlanDtos.Any(x => x.Type == model.Type))
            {
                throw new Exception($"Plan gospodarczy karmy {GetFodderTypeName(model.Type)} już istnieje! Proszę użyć opcji edycji istniejącego już planu.");
            }

            var dto = new FodderPlanDto
            {
                Type = model.Type,
                Ton = model.Ton,
                MarketingYearId = marketingYearId
            };

            _fodderPlanDao.Insert(dto);
        }

        public void UpdateFodderPlan(FodderPlanViewModel model, int marketingYearId)
        {
            if (model.Type <= 0)
            {
                throw new Exception("Nie można dodać planu gospodarczego karmy");
            }

            var dto = new FodderPlanDto
            {
                Type = model.Type,
                Ton = model.Ton,
                MarketingYearId = marketingYearId
            };

            _fodderPlanDao.Update(dto);
        }

        public void DeleteFodderPlan(int fodderType, int marketingYearId)
        {
            if (fodderType <= 0)
            {
                throw new Exception("Nie można dodać planu gospodarczego karmy");
            }

            _fodderPlanDao.Delete(fodderType, marketingYearId);
        }

        private string GetFodderTypeName(int fodderType)
        {
            switch (fodderType)
            {
                case 1: return "Objętościowa sucha";
                case 2: return "Objętościowa soczysta";
                case 3: return "Treściwa";
                case 4: return "Sól";
                default: throw new NotImplementedException();
            }
        }
    }
}
