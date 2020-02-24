using System;
using Common;
using Common.Extensions;
using DataAccess.Dao.AnnualPlan;
using DataAccess.Dao.AnnualPlanStatus;
using DataAccess.Dto;
using Domain.AnnualPlanStatus.Models;

namespace Domain.AnnualPlanStatus
{
    public class AnnualPlanStatusService : IAnnualPlanStatusService
    {
        private readonly IAnnualPlanStatusDao _annualPlanStatusDao;

        public AnnualPlanStatusService() : this(new AnnualPlanStatusDao())
        {
        }

        public AnnualPlanStatusService(IAnnualPlanStatusDao annualPlanStatusDao)
        {
            _annualPlanStatusDao = annualPlanStatusDao;
        }

        public AnnualPlanStatusModel GetByMarketingYearId(int marketingYearId)
        {
            AnnualPlanStatusDto annualPlanStatusDto = _annualPlanStatusDao.GetByMarketingYearId(marketingYearId);

            var annualPlanStatusModel = new AnnualPlanStatusModel
            {
                Id = annualPlanStatusDto.Id,
                MarketingYearId = annualPlanStatusDto.MarketingYearId,
                Status = annualPlanStatusDto.Status,
                Description = annualPlanStatusDto.Description,
                TimeStamp = annualPlanStatusDto.TimeStamp
            };

            return annualPlanStatusModel;
        }

        public void ApproveToNextStatus(int marketingYearId)
        {
            AnnualPlanStatusDto annualPlanStatusDto = _annualPlanStatusDao.GetByMarketingYearId(marketingYearId);

            if (annualPlanStatusDto.Status == (int)Common.Enums.AnnualPlanStatus.ToCorrect)
            {
                annualPlanStatusDto.Status = (int) Common.Enums.AnnualPlanStatus.ReadyToApprove;
            }
            else
            {
                annualPlanStatusDto.Status++;
            }
            annualPlanStatusDto.Description = TypeName.GetAnnualPlanStatusName(annualPlanStatusDto.Status);
            annualPlanStatusDto.TimeStamp = DateTime.Now;

            _annualPlanStatusDao.Update(annualPlanStatusDto);
        }

        public void RejectAnnualPlan(int marketingYearId)
        {
            AnnualPlanStatusDto annualPlanStatusDto = _annualPlanStatusDao.GetByMarketingYearId(marketingYearId);

            annualPlanStatusDto.Status = (int) Common.Enums.AnnualPlanStatus.ToCorrect;
            annualPlanStatusDto.Description = Text.ToCorrect;
            annualPlanStatusDto.TimeStamp = DateTime.Now;

            _annualPlanStatusDao.Update(annualPlanStatusDto);
        }

        public void Create(AnnualPlanStatusDto annualPlanStatusDto)
        {
            _annualPlanStatusDao.Insert(annualPlanStatusDto);
        }
    }
}
