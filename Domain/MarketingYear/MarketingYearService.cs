using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.MarketingYear.Models;

namespace Domain.MarketingYear
{
    public class MarketingYearService : IMarketingYearService
    {
        private IList<MarketingYearDto> _marketingYearList;

        private IList<MarketingYearDto> MarketingYearList
        {
            get
            {
                if (_marketingYearList == null)
                {
                    IList<MarketingYearDto> marketingYearDtos = _marketingYearDao.GetAll();
                    _marketingYearList = marketingYearDtos;
                }

                return _marketingYearList;
            }
        }

        private readonly IMarketingYearDao _marketingYearDao;

        public MarketingYearService() : this (new MarketingYearDao())
        {
        }

        public MarketingYearService(IMarketingYearDao marketingYearDao)
        {
            _marketingYearDao = marketingYearDao;
        }

        public int GetMarketingYearByDate(DateTime date)
        {
            MarketingYearDto marketingYearDto = MarketingYearList.FirstOrDefault(x => x.Start <= date && x.End >= date);

            return marketingYearDto.Id;
        }

        public MarketingYearModel GetMarketingYearModel(int marketingYearId)
        {
            MarketingYearDto marketingYearDto = MarketingYearList.Single(x => x.Id == marketingYearId);

            return new MarketingYearModel
            {
                Id = marketingYearDto.Id,
                Start = marketingYearDto.Start,
                End = marketingYearDto.End
            };
        }

        public bool IsDateInMarketingYear(DateTime date, int marketingYearId)
        {
            MarketingYearDto marketingYear = MarketingYearList.FirstOrDefault(x => x.Id == marketingYearId);

            if (marketingYear == null)
            {
                return false;
            }

            return marketingYear.Start <= date && marketingYear.End >= date;
        }
    }
}
