using DataAccess.Dto;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao.Huntsman
{
    public class HuntsmanDao : DaoBase, IHuntsmanDao
    {
        public IList<HuntsmanDto> GetAll()
        {
            using (DbContext)
            {
                List<Entities.Huntsman> huntsmans = DbContext.Huntsman.ToList();

                IList<HuntsmanDto> dtos = ToDtos(huntsmans);

                return dtos;
            }
        }

        private IList<HuntsmanDto> ToDtos(IList<Entities.Huntsman> entityList)
        {
            var dtos = new List<HuntsmanDto>();
            foreach (Entities.Huntsman entity in entityList)
            {
                var dto = new HuntsmanDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    LastName = entity.LastName,
                    Role = entity.Role
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
