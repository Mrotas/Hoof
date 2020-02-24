using System.Linq;
using DataAccess.Context;
using DataAccess.Dao.AnnualPlan;
using DataAccess.Dto;

namespace DataAccess.Dao.AnnualPlanStatus
{
    public class AnnualPlanStatusDao : IAnnualPlanStatusDao
    {
        public AnnualPlanStatusDto GetByMarketingYearId(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.AnnualPlanStatus annualPlan = db.AnnualPlanStatus.FirstOrDefault(x => x.MarketingYearId == marketingYearId);

                return annualPlan == null ? null : ToDto(annualPlan);
            }
        }

        public void Update(AnnualPlanStatusDto annualPlanStatusDto)
        {
            using (var db = new DbContext())
            {
                Entities.AnnualPlanStatus annualPlanStatus = db.AnnualPlanStatus.Single(x => x.Id == annualPlanStatusDto.Id);
                
                annualPlanStatus.Status = annualPlanStatusDto.Status;
                annualPlanStatus.Description = annualPlanStatusDto.Description;
                annualPlanStatus.TimeStamp = annualPlanStatusDto.TimeStamp;

                db.SaveChanges();
            }
        }
        
        public void Insert(AnnualPlanStatusDto annualPlanStatusDto)
        {
            var entity = new Entities.AnnualPlanStatus
            {
                MarketingYearId = annualPlanStatusDto.MarketingYearId,
                Status = annualPlanStatusDto.Status,
                Description = annualPlanStatusDto.Description,
                TimeStamp = annualPlanStatusDto.TimeStamp
            };

            using (var db = new DbContext())
            {
                db.AnnualPlanStatus.Add(entity);
                db.SaveChanges();
            }
        }

        private AnnualPlanStatusDto ToDto(Entities.AnnualPlanStatus annualPlan)
        {
            var dto = new AnnualPlanStatusDto
            {
                Id = annualPlan.Id,
                MarketingYearId = annualPlan.MarketingYearId,
                Status = annualPlan.Status,
                Description = annualPlan.Description,
                TimeStamp = annualPlan.TimeStamp
            };

            return dto;
        }
    }
}
