using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.CarcassRevenue
{
    public class CarcassRevenueDao : ICarcassRevenueDao
    {
        public IList<CarcassRevenueDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.CarcassRevenue> carcassRevenues =
                (
                    from carcassRevenue in db.CarcassRevenue
                    join huntedGame in db.HuntedGame on carcassRevenue.HuntedGameId equals huntedGame.Id
                    join hunt in db.Hunt on huntedGame.Id equals hunt.HuntedGameId
                    where hunt.Date >= marketingYear.Start && hunt.Date <= marketingYear.End
                    select carcassRevenue
                ).ToList();

                IList<CarcassRevenueDto> dtos = ToDtos(carcassRevenues);

                return dtos;
            }
        }

        public void Insert(CarcassRevenueDto dto)
        {
            var entity = new Entities.CarcassRevenue
            {
                HuntedGameId = dto.HuntedGameId,
                Revenue = dto.Revenue,
                CarcassWeight = dto.CarcassWeight
            };

            using (var db = new DbContext())
            {
                db.CarcassRevenue.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(CarcassRevenueDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.CarcassRevenue carcassRevenue = db.CarcassRevenue.Single(x => x.Id == dto.Id);

                carcassRevenue.HuntedGameId = dto.HuntedGameId;
                carcassRevenue.Revenue = dto.Revenue;
                carcassRevenue.CarcassWeight = dto.CarcassWeight;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new DbContext())
            {
                Entities.CarcassRevenue carcassRevenue = db.CarcassRevenue.Single(x => x.Id == id);

                db.CarcassRevenue.Remove(carcassRevenue);
                db.SaveChanges();
            }
        }

        private IList<CarcassRevenueDto> ToDtos(IList<Entities.CarcassRevenue> entityList)
        {
            var dtos = new List<CarcassRevenueDto>();
            foreach (Entities.CarcassRevenue entity in entityList)
            {
                var dto = new CarcassRevenueDto
                {
                    Id = entity.Id,
                    HuntedGameId = entity.HuntedGameId,
                    Revenue = entity.Revenue,
                    CarcassWeight = entity.CarcassWeight
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
