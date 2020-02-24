using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.MarketingYear.Models;

namespace Domain.MarketingYear
{
    public class MarketingYearService : IMarketingYearService
    {
        private readonly IMarketingYearDao _marketingYearDao;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public MarketingYearService() : this (new MarketingYearDao(), new AnnualPlanStatusService())
        {
        }

        public MarketingYearService(IMarketingYearDao marketingYearDao, IAnnualPlanStatusService annualPlanStatusService)
        {
            _marketingYearDao = marketingYearDao;
            _annualPlanStatusService = annualPlanStatusService;
        }

        public IList<MarketingYearModel> GetAll()
        {
            IList<MarketingYearDto> allMarketingYears = _marketingYearDao.GetAll();
            var marketingYearModels = allMarketingYears.Select(x => new MarketingYearModel
            {
                Id = x.Id,
                Start = x.Start,
                End = x.End
            }).ToList();

            return marketingYearModels;
        }

        public int GetMarketingYearByDate(DateTime date)
        {
            IList<MarketingYearDto> allMarketingYears = _marketingYearDao.GetAll();
            MarketingYearDto marketingYearDto = allMarketingYears.FirstOrDefault(x => x.Start <= date && x.End >= date);

            return marketingYearDto.Id;
        }

        public MarketingYearModel GetCurrentMarketingYearModel()
        {
            MarketingYearDto currentMarketingYearDto = _marketingYearDao.GetCurrent();

            return new MarketingYearModel
            {
                Id = currentMarketingYearDto.Id,
                Start = currentMarketingYearDto.Start,
                End = currentMarketingYearDto.End
            };
        }

        public MarketingYearModel GetMarketingYearModel(int marketingYearId)
        {
            MarketingYearDto marketingYearDto = _marketingYearDao.GetById(marketingYearId);

            return new MarketingYearModel
            {
                Id = marketingYearDto.Id,
                Start = marketingYearDto.Start,
                End = marketingYearDto.End
            };
        }

        public bool IsDateInMarketingYear(DateTime date, int marketingYearId)
        {
            MarketingYearDto marketingYear = _marketingYearDao.GetById(marketingYearId);

            if (marketingYear == null)
            {
                return false;
            }

            return marketingYear.Start <= date && marketingYear.End >= date;
        }

        public int Create()
        {
            IList<MarketingYearDto> allMarketingYears = _marketingYearDao.GetAll();
            MarketingYearDto lastMarketingYear = allMarketingYears.Last();

            int newMarketingYearEndDate = lastMarketingYear.End.Year + 1;
            var newMarketingYear = new MarketingYearDto
            {
                Start = new DateTime(lastMarketingYear.End.Year, lastMarketingYear.Start.Month, lastMarketingYear.Start.Day),
                End = new DateTime(newMarketingYearEndDate, lastMarketingYear.End.Month, lastMarketingYear.End.Day)
            };

            int newMarketingYearId = _marketingYearDao.Insert(newMarketingYear);

            var annualPlanStatusDto = new AnnualPlanStatusDto
            {
                MarketingYearId = newMarketingYearId,
                Status = (int) Common.Enums.AnnualPlanStatus.ReadyToPlan,
                Description = TypeName.GetAnnualPlanStatusName((int)Common.Enums.AnnualPlanStatus.ReadyToPlan),
                TimeStamp = DateTime.Now
            };

            _annualPlanStatusService.Create(annualPlanStatusDto);

            return newMarketingYearId;
        }
    }
}
