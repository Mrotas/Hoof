using DataAccess.Dto;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;

namespace DataAccess.Dao.Huntsman
{
    public class HuntsmanDao : IHuntsmanDao
    {
        public IList<HuntsmanDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.Huntsman> huntsmans = db.Huntsman.ToList();

                IList<HuntsmanDto> dtos = ToDtos(huntsmans);

                return dtos;
            }
        }

        public HuntsmanDto GetById(int huntsmanId)
        {
            using (var db = new DbContext())
            {
                Entities.Huntsman huntsman = db.Huntsman.Find(huntsmanId);

                HuntsmanDto dto = ToDto(huntsman);

                return dto;
            }
        }

        public int Insert(HuntsmanDto dto)
        {
            var entity = new Entities.Huntsman
            {
                Id = dto.Id,
                Name = dto.Name,
                LastName = dto.LastName,
                Role = dto.Role
            };

            using (var db = new DbContext())
            {
                Entities.Huntsman huntsman = db.Huntsman.Add(entity);
                db.SaveChanges();
                return huntsman.Id;
            }
        }

        private HuntsmanDto ToDto(Entities.Huntsman entity)
        {
            var dto = new HuntsmanDto
            {
                Id = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                Role = entity.Role
            };

            return dto;
        }

        private IList<HuntsmanDto> ToDtos(IList<Entities.Huntsman> entityList)
        {
            var dtos = new List<HuntsmanDto>();
            foreach (Entities.Huntsman entity in entityList)
            {
                HuntsmanDto dto = ToDto(entity);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
