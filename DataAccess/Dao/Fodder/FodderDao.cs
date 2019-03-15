using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.Fodder
{
    public class FodderDao : IFodderDao
    {
        public IList<FodderDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.Fodder> fodder = db.Fodder.Where(x => x.Date > marketingYear.Start && x.Date < marketingYear.End).ToList();

                var dtos = ToDtos(fodder);

                return dtos;
            }
        }

        public IList<FodderDto> GetByDateRange(DateTime dateFrom, DateTime dateTo)
        {
            using (var db = new DbContext())
            {
                List<Entities.Fodder> fodder = db.Fodder.Where(x => x.Date > dateFrom && x.Date < dateTo).ToList();

                var dtos = ToDtos(fodder);

                return dtos;
            }
        }

        public void Insert(FodderDto dto)
        {
            var entity = new Entities.Fodder
            {
                Type = dto.Type,
                Kilograms = dto.Kilograms,
                Owner = dto.Owner,
                Description = dto.Description,
                Date = dto.Date
            };

            using (var db = new DbContext())
            {
                db.Fodder.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(FodderDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.Fodder fodder = db.Fodder.Single(x => x.Id == dto.Id);
                
                fodder.Type = dto.Type;
                fodder.Kilograms = dto.Kilograms;
                fodder.Owner = dto.Owner;
                fodder.Description = dto.Description;
                fodder.Date = dto.Date;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new DbContext())
            {
                Entities.Fodder fodder = db.Fodder.Single(x => x.Id == id);

                db.Fodder.Remove(fodder);
                db.SaveChanges();
            }
        }

        private IList<FodderDto> ToDtos(IList<Entities.Fodder> entityList)
        {
            var dtos = new List<FodderDto>();
            foreach (Entities.Fodder entity in entityList)
            {
                var dto = new FodderDto
                {
                    Id = entity.Id,
                    Type = entity.Type,
                    Kilograms = entity.Kilograms,
                    Owner = entity.Owner,
                    Description = entity.Description,
                    Date = entity.Date
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
