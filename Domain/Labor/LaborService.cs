﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Huntsman;
using DataAccess.Dao.Labor;
using DataAccess.Dto;
using Domain.Labor.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Domain.Labor
{
    public class LaborService : ILaborService
    {
        private readonly ILaborDao _laborDao;
        private readonly IHuntsmanDao _huntsmanDao;
        private readonly IMarketingYearService _marketingYearService;

        public LaborService() : this(new LaborDao(),new HuntsmanDao(), new MarketingYearService())
        {
        }

        public LaborService(ILaborDao laborDao, IHuntsmanDao huntsmanDao, IMarketingYearService marketingYearService)
        {
            _laborDao = laborDao;
            _huntsmanDao = huntsmanDao;
            _marketingYearService = marketingYearService;
        }

        public LaborBaseViewModel GetLaborViewModel(int marketingYearId)
        {
            IList<LaborDto> laborDtos = _laborDao.GetByMarketingYear(marketingYearId);
            IList<HuntsmanDto> huntsmanDtos = _huntsmanDao.GetAll();

            List<LaborViewModel> laborViewModels =
            (
                from labor in laborDtos
                join huntsman in huntsmanDtos on labor.HuntsmanId equals huntsman.Id
                select new LaborViewModel
                {
                    Id = labor.Id,
                    HuntsmanId = huntsman.Id,
                    HuntsmanName = $"{huntsman.Name} {huntsman.LastName}",
                    Description = labor.Description,
                    Date = labor.Date
                }
            ).ToList();

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);

            var laborBaseViewModel = new LaborBaseViewModel
            {
                LaborViewModels = laborViewModels,
                MarketingYearModel = marketingYearModel
            };

            return laborBaseViewModel;
        }

        public void AddLabor(LaborViewModel model, int marketingYearId)
        {
            if (model.HuntsmanId <= 0 || String.IsNullOrWhiteSpace(model.Description))
            {
                throw new Exception("Nie można dodać pracy gospodarczej");
            }

            var dto = new LaborDto
            {
                HuntsmanId = model.HuntsmanId,
                Description = model.Description,
                Date = model.Date
            };

            _laborDao.Insert(dto);
        }

        public void UpdateLabor(LaborViewModel model, int marketingYearId)
        {
            if (model.HuntsmanId <= 0 || String.IsNullOrWhiteSpace(model.Description))
            {
                throw new Exception("Nie można edytować pracy gospodarczej");
            }

            var dto = new LaborDto
            {
                Id = model.Id,
                HuntsmanId = model.HuntsmanId,
                Description = model.Description,
                Date = model.Date
            };

            _laborDao.Update(dto);
        }

        public void DeleteLabor(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Nie można usunąć pracy gospodarczej");
            }

            _laborDao.Delete(id);
        }
    }
}
